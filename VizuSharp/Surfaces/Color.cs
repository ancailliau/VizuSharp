using System;

namespace VizuSharp.Surfaces
{
	public interface Color
	{
	}


	public class RGBAColor : Color 
	{
		public double R {
			get;
			set;
		}

		public double G {
			get;
			set;
		}

		public double B {
			get;
			set;
		}

		public double A {
			get;
			set;
		}

		public RGBAColor (double r, double g, double b, double a)
		{
			this.R = r;
			this.G = g;
			this.B = b;
			this.A = a;
		}

	}

	public class RGBColor : Color 
	{
		public static readonly RGBColor Black = new RGBColor (0, 0, 0);
		public static readonly RGBColor White = new RGBColor (1, 1, 1);
		public static readonly RGBColor Red = new RGBColor (218/255.0,147/255.0,149/255.0);
		public static readonly RGBColor DarkGrey = new RGBColor (.3, .3, .3);
		public static readonly RGBColor LightGrey = new RGBColor (.8, .8, .8);

		public double R {
			get;
			set;
		}

		public double G {
			get;
			set;
		}

		public double B {
			get;
			set;
		}

		public RGBColor (double r, double g, double b)
		{
			this.R = r;
			this.G = g;
			this.B = b;
		}
		
	}
}

