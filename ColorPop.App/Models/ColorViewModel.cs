using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorPop.App.Models;

public class ColorViewModel : IDisposable
{
	public Color Color { get; private set; }

	public byte Red
	{
		get => Color.R;
		set
		{
			Color = Color.FromArgb(Color.A, value, Color.G, Color.B);
			UpdateColorPreview();
		}
	}
	public byte Green
	{
		get => Color.G;
		set
		{
			Color = Color.FromArgb(Color.A, Color.R, value, Color.B);
			UpdateColorPreview();
		}
	}
	public byte Blue
	{
		get => Color.B;
		set
		{
			Color = Color.FromArgb(Color.A, Color.R, Color.G, value);
			UpdateColorPreview();
		}
	}

	public Bitmap ColorPreview { get; }

	public ColorViewModel()
	{
		Color = Color.White;
		ColorPreview = new Bitmap(8, 8);
		UpdateColorPreview();
	}

	public ColorViewModel(Color color)
	{
		Color = color;
		ColorPreview = new Bitmap(8, 8);
		UpdateColorPreview();
	}

	private void UpdateColorPreview()
	{
		using var graphics = Graphics.FromImage(ColorPreview);
		using var brush = new SolidBrush(Color);
		graphics.FillRectangle(brush, 0, 0, ColorPreview.Width, ColorPreview.Height);
	}

	public void Dispose()
	{
		GC.SuppressFinalize(this);
		ColorPreview.Dispose();
	}
}
