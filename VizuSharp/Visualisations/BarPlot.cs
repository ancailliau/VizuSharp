using System;
using System.Collections.Generic;
using VizuSharp.Surfaces;
using System.Linq;
using VizuSharp.DataProviders;
using VizuSharp.Axes;

namespace VizuSharp.Visualisations
{
	public class BarPlot<T> : DataPlot<T>
	{
		public Func<T, Color> PointColor {
			get;
			set;
		}

		private Func<T, double> _lowerBound;
		private Func<T, double> _upperBound;
		private Func<T, double> _count;
		private Func<T, double> _index;

		public BarPlot (Func<T, double> lowerBound, Func<T, double> upperBound, Func<T, double> count, Func<T, double> index) : base ()
		{
			Init (lowerBound, upperBound, count, index);
		}

		public BarPlot (Func<T, double> lowerBound, Func<T, double> upperBound, Func<T, double> count, Func<T, double> index, DataProvider<T> data) : base (data)
		{
			Init (lowerBound, upperBound, count, index);
		}

		void Init (Func<T, double> lowerBound, Func<T, double> upperBound, Func<T, double> count, Func<T, double> index)
		{
			_index = index;
			_lowerBound = lowerBound;
			_upperBound = upperBound;
			_count = count;

			Axes [0] = new Axis<T> (this, _index, AxisPosition.Bottom);
			Axes [0].SetDomain (Data.Min (_lowerBound), Data.Max (_upperBound));
			Axes [0].SetTicks (Data.SelectMany (x => new [] { _lowerBound (x), _upperBound(x) }).Distinct ());

			Axes [1] = new Axis<T> (this, _count, AxisPosition.Left);
			Axes [1].SetDomain (0, Data.Max (_count));

			PointColor = d => RGBColor.Red;
		}

		public override void Plot (ISurface surface)
		{
			PlotBackground (surface);
			PlotAxes (surface);

			foreach (var data in Data) {
				Console.WriteLine (_count(data));
				Console.WriteLine (Axes[1].Scale.Range.Upper);
			
				var x1 = Axes[0].Scale.Scale (_lowerBound(data));
				var x2 = Axes[0].Scale.Scale (_upperBound(data));
				var y = Axes[1].Scale.Scale (0);
				var height = y - Axes[1].Scale.Scale (_count(data));
				var width = x2 - x1;

				surface.DrawRectangle (new PointD (x1, y), width, -height, RGBColor.Red);
			}
		}

	}
}

