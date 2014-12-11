using System;
using NUnit.Framework;
using MathNet.Numerics.Distributions;
using System.Linq;
using VizuSharp.DataProviders;
using VizuSharp.Visualisations;
using VizuSharp.Surfaces;

namespace VizuSharp.Tests
{
	[TestFixture ()]
	public class TestImage
	{
		[Test ()]
		public void TestScatterPlot ()
		{
			var data = new DataSet<Tuple<double, double, double>> ();

			var rnd = new Normal ();
			for (int i = 0; i < 10000; i++) {
				var s1 = rnd.Sample ();
				var s2 = rnd.Sample ();
				data.Add (new Tuple<double, double, double> (s1 * 13.48, s2 * 2, rnd.Sample ()));
			}

			var chart = new ScatterPlot<Tuple<double, double, double>> (x => x.Item1, y => y.Item2, data);
			chart.PointColor = x => new RGBAColor (0, 0, .8, .2);

			var surface = SurfaceFactory.Create (chart, SurfaceType.Cairo);
			chart.Plot (surface);

			surface.WriteToPng ("/Users/acailliau/Desktop/data.png");
		}

		[Test ()]
		public void TestHistogram ()
		{
			var data = new DataSet <double> ();
			var rnd = new Normal ();
			for (int i = 0; i < 100000; i++) {
				var s1 = rnd.Sample ();
				data.Add (s1);
			}

			string.Join (",", data);

			var hist = new DataProviders.Histogram<double> (20, x => x, data);

			var chart = new BarPlot<Bin> (x => x.LowerBound, x => x.UpperBound, x => x.Count, x => x.Index, hist);

			chart.Axes [1].SetTicks (hist.Select (x => (double) x.Count).Distinct ());
			chart.Axes [0].SetTicks (hist.Select (x => (double) (x.UpperBound - x.LowerBound) / 2 + x.LowerBound).Distinct ());
			chart.Axes [0].TickFormat = (tick, index) => string.Format ("{0}", index + 1);

			var surface = SurfaceFactory.Create (chart, SurfaceType.Cairo);
			chart.Plot (surface);

			surface.WriteToPng ("/Users/acailliau/Desktop/data.png");
		}
	}
}

