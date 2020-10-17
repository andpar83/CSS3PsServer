using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Ps2Css.Css;
using Ps2Css.Css.Property;

namespace Ps2Css.Ai
{
	public class Document : Ps2Css.Document
	{
		public Document(byte[] data)
		{
			foreach(var chunk in GetStyles(data))
			{
				base.Styles.Add(GetStyle(chunk));
			}
		}

		private static List<string> GetStyles(byte[] data)
		{
			var text = Encoding.ASCII.GetString(data);

			var ret = new List<string>();
			foreach(Match match in Regex.Matches(text, @"(?<=\*u).+?(?=\*U)", RegexOptions.Singleline))
			{
				ret.Add(match.Value);
			}

			return ret;
		}
		private static Style GetStyle(string data)
		{
			var points = new List<Point>();

			var minX = int.MaxValue;
			var minY = int.MaxValue;
			var maxX = int.MinValue;
			var maxY = int.MinValue;
			foreach(Match match in Regex.Matches(data, @"(?<=\r|\n)(?<x>\d+\.\d+)\ (?<y>\d+\.\d+)\ l(?=\r|\n)"))
			{
				var x = (int)Math.Round(decimal.Parse(match.Groups["x"].Value, CultureInfo.InvariantCulture));
				var y = (int)Math.Round(decimal.Parse(match.Groups["y"].Value, CultureInfo.InvariantCulture));
				if(x < minX)
				{
					minX = x;
				}
				if(y < minY)
				{
					minY = y;
				}
				if(x > maxX)
				{
					maxX = x;
				}
				if(y > maxY)
				{
					maxY = y;
				}
				points.Add(new Point(x, y));
			}
			points = points.ConvertAll(point => new Point(point.X - minX, point.Y - minY));

			var width = maxX - minX;
			var height = maxY - minY;
			var radius = points.Find(point => point.Y == 0).X;

			var style = new Style();
			style.Add(new Width(width));
			style.Add(new Height(height));
			style.Add(new BorderRadius(new BorderRadiusValue(radius)));
			return style;
		}
	}
}
