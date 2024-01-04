using System.Diagnostics;

namespace ColorPop.App;

public partial class MainWindow : Form
{
	public MainWindow()
	{
		InitializeComponent();
		SetInitialValues();
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

		dialog.ShowDialog(this);
		tbImageLocation.Text = dialog.FileName;
	}

	private void TbImageLocation_TextChanged(object sender, EventArgs e)
	{
		Debug.WriteLine("text changed");
	}

	private void BtnProcess_Click(object sender, EventArgs e)
	{
		if (!ValidateFile())
		{
			return;
		}

		Process();
	}

	private bool ValidateFile()
	{
		if (!File.Exists(tbImageLocation.Text))
		{
			MessageBox.Show("File does not exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			return false;
		}

		return true;
	}

	private void Process()
	{

	}
}
