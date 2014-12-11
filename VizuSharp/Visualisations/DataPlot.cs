using VizuSharp.Axes;
using System.Collections.Generic;
using VizuSharp.Surfaces;
using System.Linq;
using System;
using VizuSharp.DataProviders;

namespace VizuSharp.Visualisations
{
	public abstract class DataPlot<T>
	{
		public DataProvider<T> Data {
			get;
			protected set;
		}

		public Axis<T>[] Axes {
			get;
			set;
		}

		public int Width {
			get;
			set;
		}

		public int Height {
			get;
			set;
		}

		public int Padding {
			get;
			set;
		}

		public DataPlot () : this (new DataSet<T> ())
		{
		}

		public DataPlot (DataProvider<T> data)
		{
			Data = data;

			Axes = new Axis<T>[4];

			Width = 640;
			Height = 480;
			Padding = 50;
		}

		protected void PlotBackground (ISurface surface)
		{
			surface.DrawBackground (RGBColor.White);
		}

		protected void PlotAxes (ISurface surface)
		{
			foreach (var a in Axes.Where (x => x != null)) {
				a.Plot (surface);
			}
		}

		public PointD GetTopLeft ()
		{
			return new PointD (Padding, Padding);
		}

		public PointD GetTopRight ()
		{
			return new PointD (Width - Padding, Padding);
		}

		public PointD GetBottomRight ()
		{
			return new PointD (Width - Padding, Height - Padding);
		}

		public PointD GetBottomLeft ()
		{
			return new PointD (Padding, Height - Padding);
		}

		public abstract void Plot (ISurface surface);
	}
}

