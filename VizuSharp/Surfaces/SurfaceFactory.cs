using System;
using VizuSharp.Visualisations;

namespace VizuSharp.Surfaces
{
	public enum SurfaceType {
		Cairo
	}

	public static class SurfaceFactory
	{
		public static ISurface Create<T> (DataPlot<T> chart, SurfaceType type)
		{
			return new CairoSurface (chart.Width, chart.Height);
		}
	}
}

