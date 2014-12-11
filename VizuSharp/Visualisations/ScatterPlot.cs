using System;
using System.Collections.Generic;
using VizuSharp.Surfaces;
using VizuSharp.DataProviders;
using VizuSharp.Axes;

namespace VizuSharp.Visualisations
{
	public class ScatterPlot<T> : DataPlot<T>
	{
		public Func<T, double> PointSize {
			get;
			set;
		}

		public Func<T, Color> PointColor {
			get;
			set;
		}

		public Func<T, double> X { get; set; }
		public Func<T, double> Y { get; set; }

		public ScatterPlot (Func<T, double> x, Func<T, double> y) : base ()
		{
			Init (x, y);
		}

		public ScatterPlot (Func<T, double> x, Func<T, double> y, DataProvider<T> data) : base (data)
		{
			Init (x, y);
		}

		void Init (Func<T, double> x, Func<T, double> y)
		{
			X = x;
			Y = y;
			PointSize = d => 3;
			PointColor = d => RGBColor.Red;

			Axes [0] = new Axis<T> (this, x, AxisPosition.Bottom);
			Axes [1] = new Axis<T> (this, y, AxisPosition.Left);
		}

		public override void Plot (ISurface surface)
		{
			PlotBackground (surface);
			PlotAxes (surface);

			foreach (var d in Data) {
				var x = Axes [0].Scale.Scale (X(d));
				var y = Axes [1].Scale.Scale (Y(d));

				surface.DrawCircle (new PointD (x, y), PointSize(d), 1, PointColor(d), false, true);
			}
		}
	}
}

