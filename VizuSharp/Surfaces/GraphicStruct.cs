using System;

namespace VizuSharp.Surfaces
{
	public struct PointD
	{
		public double X;
		public double Y;

		public PointD (double x, double y)
		{
			X = x;
			Y = y;
		}

		public static PointD operator +(PointD p1, PointD p2) 
		{
			return new PointD(p1.X + p2.X, p1.Y + p2.Y);
		}

		public static PointD operator -(PointD p1, PointD p2) 
		{
			return new PointD(p1.X - p2.X, p1.Y - p2.Y);
		}
	}
}

