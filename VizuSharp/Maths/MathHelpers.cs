using System;

namespace VizuSharp.Maths
{
	public static class MathHelpers
	{
		public static double Clamp (this double v, double min, double max)
		{
			return Math.Max (Math.Min (v, max), min);
		}
	}
}

