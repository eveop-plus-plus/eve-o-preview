using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EveOPreview.Configuration
{
    public class ThumbnailArea
	{
		public enum Direction
        {
			LeftToRight,
			RightToLeft,
			TopDown,
			BottomUp
        }

		public class PositionOffset
        {
			public int X { get; set; }
			public int Y { get; set; }
			public int Epoch { get; set; }
		}

		public ThumbnailArea()
		{
			this.Dir = Direction.LeftToRight;
		}

		public ThumbnailArea(int x, int y, int width, int height, Direction dir)
		{
			this.X = x;
			this.Y = y;
			this.Width = width;
			this.Height = height;
			this.Dir = dir;
		}

		public int X { get; set; }

		public int Y { get; set; }

		public int Width { get; set; }

		public int Height { get; set; }

		[JsonConverter(typeof(StringEnumConverter))]
		public Direction Dir { get; set; }
	}
}
