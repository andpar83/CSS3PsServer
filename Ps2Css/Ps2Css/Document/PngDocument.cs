using System.IO;
using Ps2Css.Css;
using Ps2Css.Css.Property;

namespace Ps2Css.Png
{
	public class Document : Ps2Css.Document
	{
		public Document(byte[] data)
		{
			base.Styles.Add(GetStyle(data));
		}

		private static Style GetStyle(byte[] data)
		{
			using(var stream = new MemoryStream(data))
			using(var bitmap = (System.Drawing.Bitmap)System.Drawing.Image.FromStream(stream))
			{
				var style = new Style();
				var color = bitmap.GetPixel(0, 0);
				style.Add(new BackgroundColor(new Ps2Css.Color(color.R, color.G, color.G, (double)color.A / 255)));
				return style;
			}
		}
	}
}
