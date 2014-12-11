using System;
using VizuSharp.Scales;
using VizuSharp.Visualisations;
using VizuSharp.Surfaces;
using System.Linq;
using System.Collections.Generic;

namespace VizuSharp.Axes
{
	public enum AxisPosition {
		Bottom = 0,
		Left = 1,
		Right = 2,
		Top = 3
	}

	public class Axis<T>
	{
		public double InnerSize {
			get;
			set;
		}

		public double OuterSize {
			get;
			set;
		}

		public double Padding {
			get;
			set;
		}

		public IScale Scale {
			get;
			set;
		}

		public Color Color {
			get;
			set;
		}

		public Color TextColor {
			get;
			set;
		}

		public TextOrientation TextOrientation {
			get;
			set;
		}

		public int LineWidth {
			get;
			set;
		}

		public double TextSpacing {
			get;
			set;
		}

		public Func<double, int, string> TickFormat {
			get;
			set;
		}

		public bool AutoScale { 
			get; 
			set;
		}

		public bool Visible {
			get;
			set;
		}

		DataPlot<T> _chart;
		AxisPosition _position;
		Func<T, double> _mapping;

		IEnumerable<double> _ticks;

		public Axis (DataPlot<T> chart, Func<T, double> mapping, AxisPosition position)
		{
			Scale = new LinearScale ();
			InnerSize = 0;
			OuterSize = 6;
			Padding = 20;
			_chart = chart;
			_position = position;
			Color = RGBColor.LightGrey;
			TextColor = RGBColor.DarkGrey;
			LineWidth = 1;
			TextSpacing = 5;
			TickFormat = (t,i) => string.Format ("{0:0.##}", t);
			_ticks = Scale.Ticks (11);
			AutoScale = true;
			Visible = true;
			_mapping = mapping;

		}

		public void SetTicks (IEnumerable<double> ticks)
		{
			_ticks = ticks;
		}

		public void SetDomain (double lower, double upper)
		{
			AutoScale = false;
			Scale.SetDomain(new VizuSharp.Maths.Bounds (lower, upper));
		}

		public void Plot (ISurface surface)
		{
			PointD start;
			PointD end;

			if (_position == AxisPosition.Bottom) {
				start = _chart.GetBottomLeft ();
				if (_chart.Axes[1] != null && _chart.Axes[1].Visible)
					start += new PointD (_chart.Axes[1].Padding, 0);

				end = _chart.GetBottomRight ();

				if (AutoScale) {
					Scale.SetDomain (new VizuSharp.Maths.Bounds (_chart.Data.Min (d => _mapping(d)), _chart.Data.Max (d => _mapping(d))));
				}

				Scale.SetRange (new VizuSharp.Maths.Bounds (start.X, end.X));

				if (Visible) {
					int index = 0;
					foreach (var tick in _ticks) {
						var offset = Scale.Scale (tick);			
						surface.DrawLine (new PointD (offset, start.Y - InnerSize),
							new PointD (offset, start.Y + OuterSize), LineWidth, Color);

						surface.DisplayText (TickFormat(tick, index), new PointD (offset, start.Y + OuterSize + TextSpacing), 
							TextPosition.TopCenter, TextAlignment.Center, TextOrientation, TextColor);
						index++;
					}
				}

			} else if (_position == AxisPosition.Left) {
				start = _chart.GetBottomLeft () - new PointD (0, _chart.Axes[0].Padding);
				end = _chart.GetTopLeft ();

				if (AutoScale) {
					Scale.SetDomain (new VizuSharp.Maths.Bounds (_chart.Data.Min (d => _mapping (d)), _chart.Data.Max (d => _mapping (d))));
				}
				Scale.SetRange (new VizuSharp.Maths.Bounds (start.Y, end.Y));

				if (Visible) {
					int index = 0;
					foreach (var tick in _ticks) {
						var offset = Scale.Scale (tick);			
						surface.DrawLine (new PointD (start.X + InnerSize, offset),
							new PointD (start.X - OuterSize, offset), LineWidth, Color);

						surface.DisplayText (TickFormat(tick, index), new PointD (start.X - OuterSize - TextSpacing, offset), 
							TextPosition.CenterRight, TextAlignment.Right, TextOrientation, TextColor);
					}
					index++;
				}


			} else if (_position == AxisPosition.Right) {
				throw new NotImplementedException ();

			} else if (_position == AxisPosition.Top) {
				throw new NotImplementedException ();

			} else {
				throw new NotSupportedException (string.Format ("AxisPosition {0} is not supported", _position));
			}

			if (Visible)
				surface.DrawLine (start, end, LineWidth, Color);
		}
	}
}

