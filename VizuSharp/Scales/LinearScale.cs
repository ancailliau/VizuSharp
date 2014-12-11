using System;
using VizuSharp.Maths;
using System.Collections.Generic;

namespace VizuSharp.Scales
{
	public class LinearScale : IScale
	{
		public Bounds Domain {
			get;
			private set;
		}

		public Bounds Range {
			get;
			private set;
		}

		Func<double, double> to01;

		Func<double, double> toRange;

		#region Constructors

		public LinearScale () : this (new Bounds (0, 1), new Bounds (0, 1))
		{
		}

		public LinearScale (Bounds domain) : this (domain, new Bounds (0, 1))
		{
			Domain = domain;
		}

		public LinearScale (Bounds domain, Bounds range)
		{
			SetBounds (domain, range);
		}

		public void SetDomain (Bounds domain)
		{
			Domain = domain;
			to01 = Uninterpolate.Linear (Domain.Lower, Domain.Upper);
		}

		public void SetRange (Bounds range)
		{
			Range = range;
			toRange = Interpolate.Linear (Range.Lower, Range.Upper);
		}

		public void SetBounds (Bounds domain, Bounds range)
		{
			SetDomain (domain);
			SetRange (range);
		}

		#endregion

		#region Implements IScale

		public bool Clamp {
			get;
			set;
		}

		public double Scale (double value)
		{
			var ret = toRange (to01 (value));
			return Clamp ? ret.Clamp (Range.Lower, Range.Upper) : ret;
		}

		public double ScaleRound (double value)
		{
			return System.Math.Round (Scale (value));
		}

		public IEnumerable<double> Ticks (int count)
		{
			var w = (Domain.Upper - Domain.Lower) / (count - 1);
			for (int i = 0; i < count; i++) {
				yield return Domain.Lower + w * i;
			}
		}

		#endregion
	}
}

