using System.Collections.Generic;
using VizuSharp.Maths;

namespace VizuSharp.Scales
{
	public interface IScale
	{
		Bounds Domain {
			get;
		}

		Bounds Range {
			get;
		}
		bool Clamp { get; set; }

		double Scale (double value);
		double ScaleRound (double value);
		IEnumerable<double> Ticks (int count);

		void SetDomain (Bounds domain);
		void SetRange (Bounds range);
		void SetBounds (Bounds domain, Bounds range);
	}
}

