using ColorPop.App.Models;
using ColorPop.AsmProcessor;
using ColorPop.Common;
using ColorPop.Processor;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace ColorPop.App;

public partial class MainWindow : Form
{
	private const int _averageCount = 5;
	private readonly BindingList<ColorViewModel> _colors;
	private readonly BindingSource _bindingSource;
	private readonly ErrorProvider _threadsErrorProvider;
	private readonly ErrorProvider _thresholdErrorProvider;

	public MainWindow()
	{
		_colors = new BindingList<ColorViewModel>();
		_bindingSource = new BindingSource(_colors, null);
		_threadsErrorProvider = new ErrorProvider(this);
		_thresholdErrorProvider = new ErrorProvider(this);

		InitializeComponent();
		InitializeGridView();
		SetInitialValues();
	}

	private void InitializeGridView()
	{
		dgvColors.AutoGenerateColumns = false;
		dgvColors.Columns[$"col{nameof(ColorViewModel.Red)}"].DataPropertyName = nameof(ColorViewModel.Red);
		dgvColors.Columns[$"col{nameof(ColorViewModel.Green)}"].DataPropertyName = nameof(ColorViewModel.Green);
		dgvColors.Columns[$"col{nameof(ColorViewModel.Blue)}"].DataPropertyName = nameof(ColorViewModel.Blue);
		dgvColors.Columns[$"col{nameof(ColorViewModel.ColorPreview)}"].DataPropertyName = nameof(ColorViewModel.ColorPreview);
		dgvColors.DataSource = _bindingSource;
	}

	private void SetInitialValues()
	{
		tbThreads.Text = Environment.ProcessorCount.ToString();
	}

	private void BtnBrowse_Click(object sender, EventArgs e)
	{
		using var dialog = new OpenFileDialog()
		{
			Filter = "Image Files (*.png, *.jpg, *.jpeg, *.bmp)|*.png;*.jpg;*.jpeg;*.bmp",
			Title = "Select an image"
		};

		DialogResult result = dialog.ShowDialog(this);
		tbImageLocation.Text = dialog.FileName;

		if (result != DialogResult.OK || !ValidateFile())
		{
			return;
		}

		pbMain.Image = new Bitmap(tbImageLocation.Text);
		pbMain.Cursor = Cursors.Cross;
	}

	private async void BtnProcess_Click(object sender, EventArgs e)
	{
		await ProcessAsync();
	}

	private bool ValidateFile()
	{
		if (string.IsNullOrWhiteSpace(tbImageLocation.Text))
		{
			MessageBox.Show("Please select an image", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			return false;
		}
		else if (!File.Exists(tbImageLocation.Text))
		{
			MessageBox.Show("File does not exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			return false;
		}

		return true;
	}

	private async Task ProcessAsync()
	{
		if (!ValidateFile())
		{
			return;
		}

		byte[] bitmapData = GetBitmapData();
		IEnumerable<Color> colors = GetColors();
		int threshold = GetThreshold();
		int threadCount = GetThreadCount();

		var processor = ResolveProcessor(bitmapData, colors, threshold, threadCount);

		byte[] processedBitmapData = await processor.ProcessAsync();
		using var processedBitmap = new Bitmap(tbImageLocation.Text);
		SetBitmapData(processedBitmap, processedBitmapData);
		ShowResult(processedBitmap, processor.ProcessingTimeMicroseconds);
	}

	private byte[] GetBitmapData()
	{
		using var bitmap = new Bitmap(tbImageLocation.Text);
		var rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
		BitmapData bmpData = bitmap.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
		IntPtr ptr = bmpData.Scan0;
		int bytes = Math.Abs(bmpData.Stride) * bitmap.Height;
		byte[] rgbValues = new byte[bytes];
		Marshal.Copy(ptr, rgbValues, 0, bytes);
		bitmap.UnlockBits(bmpData);
		return rgbValues;
	}

	private static void SetBitmapData(Bitmap bitmap, byte[] bitmapData)
	{
		var rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
		BitmapData bmpData = bitmap.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
		IntPtr ptr = bmpData.Scan0;
		int bytes = Math.Abs(bmpData.Stride) * bitmap.Height;
		Marshal.Copy(bitmapData, 0, ptr, bytes);
		bitmap.UnlockBits(bmpData);
	}

	private IEnumerable<Color> GetColors()
	{
		return _colors.ToList().Select(x => x.Color);
	}

	private int GetThreshold()
	{
		return int.Parse(tbThreshold.Text);
	}

	private int GetThreadCount()
	{
		return int.Parse(tbThreads.Text);
	}

	private IColorPopProcessor ResolveProcessor(byte[] bitmapData, IEnumerable<Color> colors, int threshold, int threadCount)
	{
		return rbAssembly.Checked
			? new ColorPopAsmProcessor(bitmapData, colors, threshold, threadCount)
			: new ColorPopProcessor(bitmapData, colors, threshold, threadCount);
	}

	private void ShowResult(Bitmap processedImage, long processTimeMilliseconds)
	{
		using var dialog = new ResultDialog(processedImage, processTimeMilliseconds);
		dialog.ShowDialog(this);
	}

	private void PbMain_MouseClick(object sender, MouseEventArgs e)
	{
		if (pbMain.Image == null)
		{
			return;
		}

		Color color = GetColorFromImage(e.Location);
		_colors.Add(new ColorViewModel(color));
	}

	private Color GetColorFromImage(Point point)
	{
		Size displayedImageSize = GetDisplayedImageSize(out int topOffset, out int leftOffset);
		Size imageSize = pbMain.Image.Size;

		float scaleX = (float)imageSize.Width / displayedImageSize.Width;
		float scaleY = (float)imageSize.Height / displayedImageSize.Height;

		int imageX = (int)((point.X - leftOffset) * scaleX);
		int imageY = (int)((point.Y - topOffset) * scaleY);

		using (var bitmap = new Bitmap(pbMain.Image))
		{
			if (imageX >= 0 && imageX < imageSize.Width && imageY >= 0 && imageY < imageSize.Height)
			{
				return bitmap.GetPixel(imageX, imageY);
			}
		}

		return Color.White;
	}

	private Size GetDisplayedImageSize(out int topOffset, out int leftOffset)
	{
		topOffset = 0;
		leftOffset = 0;

		Size containerSize = pbMain.ClientSize;
		float containerAspectRatio = (float)containerSize.Height / (float)containerSize.Width;
		Size originalImageSize = pbMain.Image.Size;
		float imageAspectRatio = (float)originalImageSize.Height / (float)originalImageSize.Width;

		var result = new Size();
		if (containerAspectRatio > imageAspectRatio)
		{
			result.Width = containerSize.Width;
			result.Height = (int)(imageAspectRatio * containerSize.Width);
			topOffset = (containerSize.Height - result.Height) / 2;
		}
		else
		{
			result.Height = containerSize.Height;
			result.Width = (int)((1.0f / imageAspectRatio) * containerSize.Height);
			leftOffset = (containerSize.Width - result.Width) / 2;
		}

		return result;
	}

	private void RbCustom_CheckedChanged(object sender, EventArgs e)
	{
		if (rbCustom.Checked)
		{
			tbThreads.Enabled = true;
		}
	}

	private void RbAuto_CheckedChanged(object sender, EventArgs e)
	{
		if (rbAuto.Checked)
		{
			tbThreads.Enabled = false;
			tbThreads.Text = Environment.ProcessorCount.ToString();
		}
	}

	private void TbThreshold_Validating(object sender, CancelEventArgs e)
	{
		if (int.TryParse(tbThreshold.Text, out _) == false)
		{
			e.Cancel = true;
			_thresholdErrorProvider.SetError(tbThreshold, "Please enter a valid number");
		}
		else
		{
			_thresholdErrorProvider.Clear();
		}
	}

	private void TbThreads_Validating(object sender, CancelEventArgs e)
	{
		if (int.TryParse(tbThreads.Text, out int threads) == false || threads < 1)
		{
			e.Cancel = true;
			_threadsErrorProvider.SetError(tbThreads, "Please enter a valid number bigger than 0");
		}
		else
		{
			_threadsErrorProvider.Clear();
		}
	}

	private async void BtnAverage_Click(object sender, EventArgs e)
	{
		if (!ValidateFile())
		{
			return;
		}

		IEnumerable<Color> colors = GetColors();
		int threshold = GetThreshold();
		int threadCount = GetThreadCount();

		var times = new List<long>();
		for (int i = 0; i < _averageCount; i++)
		{
			byte[] bitmapData = GetBitmapData();
			var processor = ResolveProcessor(bitmapData, colors, threshold, threadCount);
			await processor.ProcessAsync();
			times.Add(processor.ProcessingTimeMicroseconds);
		}

		long averageTime = (long)times.Average();
		MessageBox.Show($"Average processing time: {averageTime} μs", "Average processing time", MessageBoxButtons.OK, MessageBoxIcon.Information);
	}
}
