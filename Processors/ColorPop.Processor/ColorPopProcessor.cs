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

	// Kolejnosc pikseli "odwrotna" niz w asm :)
	protected override void ProcessChunk(int startIndex, int endIndex)
	{
		for (int index = startIndex; index + 4 <= endIndex; index += 4)
		{
			var pixel = new Pixel(
				_bitmapData[index + 2],
				_bitmapData[index + 1],
				_bitmapData[index]
			);

			ProcessPixel(pixel);

			_bitmapData[index + 2] = pixel.Red;
			_bitmapData[index + 1] = pixel.Green;
			_bitmapData[index] = pixel.Blue;
		}
	}

	private void ProcessPixel(Pixel pixel)
	{
		if (_colors.Any(x => AreColorsMatching(pixel, x)))
		{
			return;
		}

		byte luminance = (byte)((_redFactor * pixel.Red >> 8) + (_greenFactor * pixel.Green >> 8) + (_blueFactor * pixel.Blue >> 8));

		pixel.Red = luminance;
		pixel.Green = luminance;
		pixel.Blue = luminance;
	}

	private bool AreColorsMatching(Pixel originalPixel, Color targetColor)
	{
		double a = Math.Pow(originalPixel.Red - targetColor.R, 2);
		double b = Math.Pow(originalPixel.Green - targetColor.G, 2);
		double c = Math.Pow(originalPixel.Blue - targetColor.B, 2);

		double difference = a + b + c;

		return (int)difference <= (int)Math.Pow(_threshold, 2);
	}
}
