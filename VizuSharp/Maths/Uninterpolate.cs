using System;

namespace VizuSharp.Maths
{
	public static class Uninterpolate
	{
		public static Func<double,double> Linear (double start, double end) {
			return y => (y - start) / (end - start);
		}
	}
}

