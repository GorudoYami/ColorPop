using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorPop.Common;
public class Pixel
{
	public byte Red { get; set; }
	public byte Green { get; set; }
	public byte Blue { get; set; }

	public Pixel(byte red, byte green, byte blue)
	{
		Red = red;
		Green = green;
		Blue = blue;
	}
}
