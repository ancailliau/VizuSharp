using System;

namespace VizuSharp.Maths
{
	public static class Interpolate
	{
		public static Func<double,double> Linear (double start, double end) {
			return x => start * (1 - x) + end * x;
		}
	}
}

