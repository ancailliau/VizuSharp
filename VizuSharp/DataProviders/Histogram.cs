using System;
using System.Collections.Generic;
using System.Linq;
using VizuSharp.Maths;

namespace VizuSharp.DataProviders
{
	public class Bin
	{
		public double LowerBound {
			get;
			set;
		}

		public double UpperBound {
			get;
			set;
		}

		public int Count {
			get;
			set;
		}

		public int Index {
			get;
			set;
		}

		public Bin (int index, double lowerBound, double upperBound, int count)
		{
			Index = index;
			LowerBound = lowerBound;
			UpperBound = upperBound;
			Count = count;
		}
		
	}

	public class Histogram<T> : DataProvider<Bin>
	{
		DataProvider<T> _data;

		public int NBin {
			get;
			set;
		}

		public override IEnumerable<Bin> Data {
			get {
				return  _histogram;
			}
		}

		Func<T, double> F;

		Bin[] _histogram;

		public Histogram (int nbin, Func<T, double> f, DataProvider<T> data)
		{
			NBin = nbin;
			F = f;
			_data = data;
			BuildHistogram ();
		}


		void BuildHistogram ()
		{
			var low = _data.Min(d => F(d));
			var high = _data.Max(d => F(d));

			var stops = EnumerableHelpers.LinSpace (low, high, NBin + 1).ToArray ();
//			var width = stops[1] - stops[0];

			var sorteddata = _data.Select (x => F(x)).ToList ();
			sorteddata.Sort ();

			_histogram = new Bin[NBin];
			for (int i = 0; i < NBin; i++) {
				_histogram [i] = new Bin (i, stops[i], stops[i+1], 0);
			}

			var currentBin = 0;
			for (int index = 0; index < sorteddata.Count; index++) {
				while (currentBin + 1 < NBin && sorteddata [index] >= stops [currentBin + 1])
					currentBin++;
				_histogram [currentBin].Count++;
			}
		}
	}
}

