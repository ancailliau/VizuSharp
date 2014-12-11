using System;
using Cairo;

namespace VizuSharp.Surfaces
{
	public class CairoSurface : ISurface
	{
		ImageSurface surface;

		Cairo.Context context;

		Pango.Layout pangoLayout;

		int ImageWidth;

		int ImageHeight;

		public CairoSurface (int imageWidth, int imageHeight)
		{
			ImageWidth = imageWidth;
			ImageHeight = imageHeight;

			Init ();
		}

		~CairoSurface ()
		{
			Close ();
		}

		void Init ()
		{
			surface = new ImageSurface (Format.Argb32, ImageWidth, ImageHeight);
			context = new Cairo.Context (surface);
			pangoLayout = Pango.CairoHelper.CreateLayout (context);
		}

		void Close ()
		{
			context.Dispose ();
			surface.Dispose ();
		}

		public void DrawBackground (Color color)
		{
			SaveState ();

			context.SetSourceColor (Convert (color));
			context.Rectangle (0, 0, ImageWidth, ImageHeight);
			context.Fill ();

			RestoreState ();
		}

		public void DrawLine (PointD start, PointD end, double lineWidth = 1, Surfaces.Color color = null)
		{
			if (color == null)
				color = RGBColor.Black;

			var _start = RoundToCrisp (Convert (start), RoundingStrategy.Floor);
			var _end = RoundToCrisp (Convert (end), RoundingStrategy.Floor);

			SaveState ();

			context.SetSourceColor (Convert (color));
			context.LineWidth = lineWidth;
			context.MoveTo (_start);
			context.LineTo (_end);
			context.Stroke ();

			RestoreState ();
		}

		public void DrawCircle (PointD center, double radius, double lineWidth, Color color, bool stroke, bool fill)
		{
			var _center = RoundToCrisp (Convert (center));

			SaveState ();

			context.SetSourceColor (Convert (color));
			context.LineWidth = lineWidth;
			context.MoveTo (_center);
			context.Arc (_center.X, _center.Y, radius, 0, Math.PI * 2);

			if (fill) {
				if (stroke)
					context.FillPreserve ();
				else
					context.Fill ();
			}
			if (stroke)
				context.Stroke ();

			RestoreState ();
		}

		public void DrawRectangle (PointD pointD, double width, double height, Surfaces.Color color)
		{
			SaveState ();

			Console.WriteLine ("pointD={0}, width={1}, height={2}", pointD.X + "," + pointD.Y, width, height);
			

			context.SetSourceColor (Convert (color));
			context.Rectangle (Convert(pointD), width, height);
			context.Fill ();

			RestoreState ();
		}

		public void DisplayText (string text, PointD anchor, TextPosition position, TextAlignment aligment, TextOrientation orientation, Surfaces.Color color)
		{
			SaveState ();

			pangoLayout.Alignment = Pango.Alignment.Center;
			pangoLayout.SetText (text);

			var font = new Pango.FontDescription ();
			font.Size = (int) (12 * Pango.Scale.PangoScale);
			font.Family = "sans";
			pangoLayout.FontDescription = font;

			int textWidth, textHeight;
			pangoLayout.GetPixelSize(out textWidth, out textHeight);

			var _anchor = Convert (anchor);

			if (orientation == TextOrientation.Vertical) {
				throw new NotImplementedException ("Text orientation is not implemented");
			}

			if (position == TextPosition.TopLeft) {
				context.MoveTo (_anchor);
			} else if (position == TextPosition.TopCenter) {
				context.MoveTo (_anchor.X - textWidth / 2.0, _anchor.Y);
			} else if (position == TextPosition.TopRight) {
				context.MoveTo (_anchor.X - textWidth, _anchor.Y);
			} else if (position == TextPosition.CenterLeft) {
				context.MoveTo (_anchor.X, _anchor.Y - textHeight / 2.0);
			} else if (position == TextPosition.CenterCenter) {
				context.MoveTo (_anchor.X - textWidth / 2.0, _anchor.Y - textHeight / 2.0);
			} else if (position == TextPosition.CenterRight) {
				context.MoveTo (_anchor.X - textWidth, _anchor.Y - textHeight / 2.0);
			} else if (position == TextPosition.BottomLeft) {
				context.MoveTo (_anchor.X, _anchor.Y - textHeight);
			} else if (position == TextPosition.BottomCenter) {
				context.MoveTo (_anchor.X - textWidth / 2.0, _anchor.Y - textHeight);
			} else if (position == TextPosition.BottomRight) {
				context.MoveTo (_anchor.X - textWidth, _anchor.Y - textHeight);
			} else {
				throw new NotSupportedException (string.Format ("TextPosition {0} is not supported.", position));
			}

			context.SetSourceColor (Convert (color));
			Pango.CairoHelper.ShowLayout(context, pangoLayout);

			RestoreState ();
		}

		public void WriteToPng (string filename)
		{
			surface.WriteToPng (filename);
		}

		#region Rounding

		enum RoundingStrategy {
			Floor, Ceiling, Round
		}

		Cairo.PointD RoundToCrisp (Cairo.PointD point, RoundingStrategy type = RoundingStrategy.Round)
		{
			Func<double, double> _round;
			if (type == RoundingStrategy.Ceiling)
				_round = x => Math.Ceiling(x) - .5;
			else if (type == RoundingStrategy.Floor)
				_round = x => Math.Floor(x) + .5;
			else
				_round = Math.Round;

			point.X = _round (point.X);
			point.Y = _round (point.Y);
			return point;
		}

		#endregion

		#region Conversions

		Cairo.Color Convert (Surfaces.Color color)
		{
			if (color is RGBColor) {
				return Convert ((RGBColor)color);
			} else if (color is RGBAColor) {
				return Convert ((RGBAColor)color);
			}

			throw new NotImplementedException (string.Format ("Color {0} is not supported", color.GetType ().Name));
		}

		Cairo.Color Convert (Surfaces.RGBAColor color) {
			return new Cairo.Color (color.R, color.G, color.B, color.A);
		}

		Cairo.Color Convert (Surfaces.RGBColor color) {
			return new Cairo.Color (color.R, color.G, color.B);
		}

		Cairo.PointD Convert (Surfaces.PointD point) {
			return new Cairo.PointD (point.X, point.Y);
		}

		#endregion

		#region State

		void SaveState ()
		{
			context.Save ();
		}

		void RestoreState ()
		{
			context.Restore ();
		}

		#endregion
	}
}

