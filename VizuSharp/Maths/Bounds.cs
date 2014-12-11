using System;

namespace VizuSharp.Maths
{
	public struct Bounds
	{
		public readonly double Lower;
		public readonly double Upper;

		public Bounds (double lower, double upper)
		{
			Lower = lower;
			Upper = upper;
		}
	}
}

