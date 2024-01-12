using System.Drawing;

namespace ColorPop.Common;

public interface IColorPopProcessor
{
	long ProcessingTimeMilliseconds { get; }

	Task<byte[]> ProcessAsync();
}
