using ColorPop.Common;
using System.Drawing;
using System.Runtime.InteropServices;

namespace ColorPop.AsmProcessor;

public class ColorPopAsmProcessor : ColorPopProcessorBase
{
	public ColorPopAsmProcessor(byte[] originalBitmapData, IEnumerable<Color> colors, int threshold, int threadCount)
		: base(originalBitmapData, colors, threshold, threadCount)
	{
	}

	[DllImport(@"ColorPopAsm.dll")]
	private static extern void ProcessChunk(int startIndex, int endIndex, int threshold, byte[] targetColors, int targetColorsLength, byte[] bitmapData);

	protected override void ProcessChunk(int startIndex, int endIndex)
	{
		byte[] targetColors = _colors.SelectMany(c => new byte[] { c.B, c.G, c.R, 255 }).ToArray();
		ProcessChunk(startIndex, endIndex, _threshold, targetColors, targetColors.Length, _bitmapData);
	}

	public static string GetAddress(byte[] array)
	{
		return Marshal.UnsafeAddrOfPinnedArrayElement(array, 0).ToString();
	}
}
