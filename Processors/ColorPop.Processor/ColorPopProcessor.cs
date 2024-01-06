using ColorPop.Common;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.Versioning;

namespace ColorPop.Processor;

[SupportedOSPlatform("Windows6.1")]
public class ColorPopProcessor : IColorPopProcessor
{
	public long ProcessTimeMilliseconds { get; private set; }

	private const double _redFactor = 0.299;
	private const double _greenFactor = 0.587;
	private const double _blueFactor = 0.114;

	private readonly ICollection<Color> _colors;
	private readonly Bitmap _originalImage;
	private readonly Bitmap _processedImage;
	private readonly int _threshold;
	private readonly int _threadCount;

	public ColorPopProcessor(Bitmap originalImage, ICollection<Color> colors, int threshold, int threadCount)
	{
		_originalImage = originalImage;
		_processedImage = new Bitmap(_originalImage.Width, _originalImage.Height);
		_colors = colors;
		_threshold = threshold;
		_threadCount = threadCount;
	}

	public async Task<Bitmap> ProcessAsync()
	{
		int chunkWidth = _originalImage.Width / _threadCount;
		int chunkHeight = _originalImage.Height / _threadCount;

		var tasks = new List<Task>();
		var stopwatch = Stopwatch.StartNew();
		for (int y = 0; y < _threadCount; y++)
		{
			for (int x = 0; x < _threadCount; x++)
			{
				int startX = x * chunkWidth;
				int startY = y * chunkHeight;
				int endX = startX + chunkWidth;
				int endY = startY + chunkHeight;

				tasks.Add(Task.Run(() => ProcessChunk(startX, startY, endX, endY)));
			}
		}

		await Task.WhenAll(tasks);

		stopwatch.Stop();
		ProcessTimeMilliseconds = stopwatch.ElapsedMilliseconds;

		return _processedImage;
	}

	private void ProcessChunk(int startX, int startY, int endX, int endY)
	{
		for (int y = startY; y < endY; y++)
		{
			for (int x = startX; x < endX; x++)
			{
				Color originalColor = _originalImage.GetPixel(x, y);
				Color processedColor = ProcessPixel(originalColor);

				_processedImage.SetPixel(x, y, processedColor);
			}
		}
	}

	private Color ProcessPixel(Color originalColor)
	{
		foreach (Color targetColor in _colors)
		{
			if (AreColorsMatching(originalColor, targetColor))
			{
				return originalColor;
			}
		}

		int luminance = (int)(_redFactor * originalColor.R + _greenFactor * originalColor.G + _blueFactor * originalColor.B);
		return Color.FromArgb(luminance, luminance, luminance);
	}

	private bool AreColorsMatching(Color colorA, Color colorB)
	{
		int deltaR = Math.Abs(colorA.R - colorB.R);
		int deltaG = Math.Abs(colorA.G - colorB.G);
		int deltaB = Math.Abs(colorA.B - colorB.B);

		return (deltaR + deltaG + deltaB <= _threshold);
	}
}
