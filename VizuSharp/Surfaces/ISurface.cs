namespace VizuSharp.Surfaces
{
	public interface ISurface
	{
		void DrawBackground (Color color);
		void DrawLine (PointD start, PointD end, double lineWidth = 1, Color color = null);
		void DrawCircle (PointD pointD, double radius, double lineWidth, Color color, bool stroke, bool fill);
		void DrawRectangle (PointD pointD, double width, double height, Surfaces.Color color);

		void DisplayText (string text, PointD anchor, TextPosition position, TextAlignment aligment, TextOrientation orientation, Surfaces.Color color);

		void WriteToPng (string filename);

	}

	public enum TextAlignment {
		Left, Right, Center
	}

	public enum TextPosition {
		TopLeft, TopCenter, TopRight,
		CenterLeft, CenterCenter, CenterRight,
		BottomLeft, BottomCenter, BottomRight
	}

	public enum TextOrientation {
		Horizontal, Vertical
	}
}

