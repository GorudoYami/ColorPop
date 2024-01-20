using System.Diagnostics;
using System.Drawing;

namespace ColorPop.Common;

public abstract class ColorPopProcessorBase : IColorPopProcessor
{
	public long ProcessingTimeMilliseconds { get; set; }

	protected readonly byte[] _bitmapData;
	protected readonly double _redFactor;
	protected readonly double _greenFactor;
	protected readonly double _blueFactor;
	protected IEnumerable<Color> _colors;
	protected int _threshold;
	protected int _threadCount;

	public ColorPopProcessorBase(byte[] originalBitmapData, IEnumerable<Color> colors, int threshold, int threadCount)
	{
		_bitmapData = originalBitmapData;
		_colors = colors;
		_threshold = threshold;
		_threadCount = threadCount;
		_redFactor = 0.299;
		_greenFactor = 0.587;
		_blueFactor = 0.114;
	}

	public async Task<byte[]> ProcessAsync()
	{
		int chunkLength = _bitmapData.Length / _threadCount;
		chunkLength += (4 - chunkLength % 4) % 4;

		var tasks = new List<Task>();

		for (int chunkIndex = 0; chunkIndex < _threadCount; chunkIndex++)
		{
			int startIndex = chunkIndex * chunkLength;
			int endIndex = startIndex + chunkLength;

			if (chunkIndex == _threadCount - 1)
			{
				endIndex = _bitmapData.Length;
			}

			tasks.Add(new Task(() => ProcessChunk(startIndex, endIndex)));
		}

		var stopwatch = Stopwatch.StartNew();
		tasks.ForEach(x => x.Start());
		await Task.WhenAll(tasks);

		stopwatch.Stop();
		ProcessingTimeMilliseconds = stopwatch.ElapsedMilliseconds;

		return _bitmapData;
	}

	protected abstract void ProcessChunk(int startIndex, int endIndex);
}
