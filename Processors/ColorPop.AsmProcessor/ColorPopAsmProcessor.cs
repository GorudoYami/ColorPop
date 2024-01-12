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

	[DllImport(@"ColorPopAsm.dll")]
	private static extern void TestProcedure(int a, int b, int c, int d, int e, int f);

	protected override void ProcessChunk(int startIndex, int endIndex)
	{
		byte[] targetColors = _colors.SelectMany(c => new byte[] { c.B, c.G, c.R, 255 }).ToArray();
		ProcessChunk(startIndex, endIndex, _threshold, targetColors, _colors.Count() * 4, _bitmapData);
	}
}
