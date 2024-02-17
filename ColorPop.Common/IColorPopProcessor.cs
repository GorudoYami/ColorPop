using System.Drawing;

namespace ColorPop.Common;

public interface IColorPopProcessor
{
	long ProcessingTimeMicroseconds { get; }

	Task<byte[]> ProcessAsync();
}
