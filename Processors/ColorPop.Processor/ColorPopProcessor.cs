using ColorPop.Common;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.Versioning;

namespace ColorPop.Processor;

[SupportedOSPlatform("Windows6.1")]
public class ColorPopProcessor : ColorPopProcessorBase
{
	public ColorPopProcessor(byte[] originalBitmapData, IEnumerable<Color> colors, int threshold, int threadCount)
		: base(originalBitmapData, colors, threshold, threadCount)
	{
	}

	protected override void ProcessChunk(int startIndex, int endIndex)
	{
		for (int index = startIndex; index + 4 < endIndex; index += 4)
		{
			var pixel = new Pixel(
				_bitmapData[index],
				_bitmapData[index + 1],
				_bitmapData[index + 2]
			);

			ProcessPixel(pixel);

			_bitmapData[index] = pixel.Red;
			_bitmapData[index + 1] = pixel.Green;
			_bitmapData[index + 2] = pixel.Blue;
		}
	}

	private void ProcessPixel(Pixel pixel)
	{
		if (_colors.Any(x => AreColorsMatching(pixel, x)))
		{
			return;
		}

		int luminance = (int)(_redFactor * pixel.Red + _greenFactor * pixel.Green + _blueFactor * pixel.Blue);

		pixel.Red = (byte)luminance;
		pixel.Green = (byte)luminance;
		pixel.Blue = (byte)luminance;
	}

	private bool AreColorsMatching(Pixel originalPixel, Color targetColor)
	{
		int deltaR = Math.Abs(originalPixel.Red - targetColor.R);
		int deltaG = Math.Abs(originalPixel.Green - targetColor.G);
		int deltaB = Math.Abs(originalPixel.Blue - targetColor.B);

		return deltaR <= _threshold && deltaG <= _threshold && deltaB <= _threshold;
	}
}
