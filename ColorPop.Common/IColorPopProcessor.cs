using System.Drawing;

namespace ColorPop.Common;

public interface IColorPopProcessor
{
	long ProcessTimeMilliseconds { get; }

	Task<Bitmap> ProcessAsync();
}
