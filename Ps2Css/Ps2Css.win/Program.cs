using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Ps2Css.win
{
	static class Program
	{
		[STAThread]
		static void Main(string[] args)
		{
			var file = args.Length > 0 ? args[0] : @"C:\Users\alexandr\Downloads\test.zip";

			var data = File.ReadAllBytes(file);
			Document doc = null;
			switch(Path.GetExtension(file).ToLowerInvariant())
			{
				case ".ai":
					doc = new Ai.Document(data);
					break;
				case ".asl":
					doc = new Asl.Document(data);
					break;
				case ".png":
					doc = new Png.Document(data);
					break;
				case ".xml":
					doc = new Xml.Document(Encoding.UTF8.GetString(data));
					break;
				case ".zip":
					doc = new Xml.Document(Encoding.UTF8.GetString(Zlib.Decompress(data)));
					break;
			}
			if(doc != null)
			{
				var sb = new StringBuilder();
				foreach(var style in doc.Styles)
				{
					if(style != null)
					{
						sb.Append(style.ToString());
					}
				}
				if(sb.Length > 0)
				{
					Clipboard.SetText(sb.ToString());
				}
			}
		}
	}
}
