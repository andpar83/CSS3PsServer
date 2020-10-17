using System;
using System.Collections.Generic;
using System.Drawing;
using Ps2Css.Css;
using Ps2Css.Css.Property;
using System.Text;

namespace Ps2Css.Common
{
	internal class Color : Ps2Css.Color
	{
		/// <param name="opacity">Opacity [0..100]</param>
		internal Color(Record data, double opacity) : base(
			data != null? data["Rd  "]: 0.0,
			data != null? data["Grn "]: 0.0,
			data != null? data["Bl  "]: 0.0,
			opacity
		)
		{
		}
		internal Color(Record data) : this(
			data["Clr "],
			data["Opct"]
		)
		{
		}
	}
	internal class Offset : Ps2Css.Offset
	{
		internal Offset(Record data) : base(
			data["lagl"],
			data["Dstn"]
		)
		{
		}
	}

	internal class Overlay
	{
		internal static Color Parse(Record data)
		{
			if(data["SoFi"] != null && (bool)data["SoFi"]["enab"])
			{
				return new Color(data["SoFi"]);
			}
			return null;
		}
	}

	internal class Shadows
	{
		internal static IProperty Parse(int globalAngle, Color overlay, Record data)
		{
			var value = new List<Ps2Css.ShadowValue>();

			{
				var section = data["OrGl"];
				if(section != null && (bool)section["enab"])
				{
					var angle = (double)section["lagl"];

					value.Add(new Ps2Css.ShadowValue(false, overlay, new Color(section), new Ps2Css.Offset(angle, section["Dstn"]), (double)section["blur"], (double)section["Ckmt"]));
				}
			}
			{
				var section = data["DrSh"];
				if(section != null && (bool)section["enab"])
				{
					var angle = section["uglg"] != null && (bool)section["uglg"] ? globalAngle : (double)section["lagl"];

					value.Add(new Ps2Css.ShadowValue(false, overlay, new Color(section), new Ps2Css.Offset(angle, section["Dstn"]), (double)section["blur"], (double)section["Ckmt"]));
				}
			}
			{
				var section = data["IrSh"];
				if(section != null && (bool)section["enab"])
				{
					var angle = section["uglg"] != null && (bool)section["uglg"] ? globalAngle : (double)section["lagl"];

					value.Add(new Ps2Css.ShadowValue(true, overlay, new Color(section), new Ps2Css.Offset(angle, section["Dstn"]), (double)section["blur"], (double)section["Ckmt"]));
				}
			}
			{
				var section = data["IrGl"];
				if(section != null && (bool)section["enab"])
				{
					var angle = (double)section["lagl"];

					value.Add(new Ps2Css.ShadowValue(true, overlay, new Color(section), new Ps2Css.Offset(angle, section["Dstn"]), (double)section["blur"], (double)section["Ckmt"]));
				}
			}

			return value.Count > 0 ? new BoxShadow(value) : null;
		}
	}
	internal class BorderValue : Ps2Css.BorderValue
	{
		private BorderValue(Record data) : base(
			(double)data["Sz  "],
			new Color(data)
		)
		{
		}

		internal static IProperty Parse(Record data)
		{
			if(data["FrFX"] != null && (bool)data["FrFX"]["enab"])
			{
				return new Border(new BorderValue(data["FrFX"]));
			}
			return null;
		}
	}
	internal class GradientValue : Ps2Css.LinearGradientValue
	{
		private new class Transition : Ps2Css.LinearGradientValue.Transition
		{
			public Transition(Record data) : base(
				new Color(data),
				Math.Round((int)data["Lctn"] / 4096m, 2)
			)
			{
			}
		}

		internal GradientValue(Color overlay, Record data) : base(
			overlay,
			((List<Record>)data["Grad"]["Clrs"]).ConvertAll(x => new Transition(x)).ToArray(),
			((List<Record>)data["Grad"]["Trns"]).ConvertAll(x => new Transition(x)).ToArray(),
			data["Opct"],
			data["Angl"],
			data["Rvrs"]
		)
		{
		}

		internal static IProperty Parse(Color overlay, Record data)
		{
			var value = new List<Ps2Css.LinearGradientValue>();

			if(data["GrFl"] != null && (bool)data["GrFl"]["enab"] && (string)data["GrFl"]["Grad"]["GrdF"] == "customStops")
			{
				value.Add(new GradientValue(overlay, data["GrFl"]));
			}

			return new BackgroundImage(value);
		}
	}
	internal class BackgroundColorValue : Ps2Css.Color
	{
		private BackgroundColorValue(Record data, double opacity) : base(
			data["Rd  "],
			data["Grn "],
			data["Bl  "],
			opacity
		)
		{
		}

		internal static IProperty Parse(Record data)
		{
			if(data["Adjs"] != null)
			{
				var opacity = 100.0 * (int)data["fillOpacity"] / 255;
				foreach(var item in (List<Record>)data["Adjs"])
				{
					if(item["Clr "] != null)
					{
						var value = new BackgroundColorValue(item["Clr "], opacity);

						return new BackgroundColor(value);
					}
				}
			}
			return null;
		}
	}
	internal class Size
	{
		private readonly Rectangle value;
		private Size(int left, int top, int right, int bottom)
		{
			this.value = new Rectangle(left, top, right - left, bottom - top);
		}
		private Size(Record data) : this(
			(int)Math.Round((double)data["Left"]),
			(int)Math.Round((double)data["Top "]),
			(int)Math.Round((double)data["Rght"]),
			(int)Math.Round((double)data["Btom"])
		)
		{
		}

		internal static IProperty[] Parse(Record data)
		{
			if(data["bounds"] != null)
			{
				var value = new Size(data["bounds"]);

				return new IProperty[] {
					new Width(value.value.Width),
					new Height(value.value.Height)
				};
			}
			return null;
		}
	}
	internal class BorderRadiusValue
	{
		internal static IProperty[] Parse(Record data)
		{
			var points = new List<Point>();

			var PthC = data["PthC"];
			if(PthC != null)
			{
				var pathComponents = PthC["pathComponents"];
				if(pathComponents != null)
				{
					var list = (List<Record>)pathComponents;
					if(list.Count > 0)
					{
						var SbpL = (List<Record>)list[0]["SbpL"];
						if(SbpL != null)
						{
							foreach(var subpath in SbpL)
							{
								var Pts = (List<Record>)subpath["Pts "];
								if(Pts != null)
								{
									foreach(var item in Pts)
									{
										if(item["Anch"] != null)
										{
											var Anch = item["Anch"];
											points.Add(new Point((int)Math.Round((double)Anch["Hrzn"]), (int)Math.Round((double)Anch["Vrtc"])));
										}
									}
								}
							}
						}
					}
				}
			}

			if(points.Count > 0)
			{
				var ret = new List<IProperty>();
				#region Bounds
				var maxX = points[0].X;
				var minX = points[0].X;
				var maxY = points[0].Y;
				var minY = points[0].Y;
				for(int i = 1; i < points.Count; i++)
				{
					var point = points[i];
					if(point.X > maxX)
					{
						maxX = point.X;
					}
					if(point.Y > maxY)
					{
						maxY = point.Y;
					}
					if(point.X < minX)
					{
						minX = point.X;
					}
					if(point.Y < minY)
					{
						minY = point.Y;
					}
				}
				#endregion
				ret.Add(new Width(maxX - minX));
				ret.Add(new Height(maxY - minY));
				foreach(var point in points)
				{
					if(point.X != minX && point.X != maxX && point.Y != minY && point.Y != maxY)
					{
						return ret.ToArray();
					}
				}
				var value = new Ps2Css.BorderRadiusValue(
					points.Min(p => p.Y == minY, p => p.X) - minX,
					points.Min(p => p.X == minX, p => p.Y) - minY,
					maxX - points.Max(p => p.Y == minY, p => p.X),
					points.Min(p => p.X == maxX, p => p.Y) - minY,
					maxX - points.Max(p => p.Y == maxY, p => p.X),
					maxY - points.Max(p => p.X == maxX, p => p.Y),
					points.Min(p => p.Y == maxY, p => p.X) - minX,
					maxY - points.Max(p => p.X == minX, p => p.Y)
				);
				if(value.ToString() != "0")
				{
					ret.Add(new BorderRadius(value));
				}
				return ret.ToArray();
			}
			return null;
		}
	}

	internal class Text
	{
		public enum FontWeight
		{
			Inherit,
			Normal,
			Bold
		}
		public enum FontStyle
		{
			Inherit,
			Regular,
			Italic,
			Oblique
		}
		public enum TextTransform
		{
			Inherit,
			None,
			Uppercase
		}
		[Flags]
		public enum TextDecoration
		{
			Inherit = 0,
			None = 1,
			Underline = 2,
			Linetrough = 4
		}

		public class Style
		{
			private readonly string FontName;
			private readonly UnitPx FontSize;
			private readonly FontWeight FontWeight;
			private readonly FontStyle FontStyle;
			private readonly TextTransform TextTransform;
			private readonly TextDecoration TextDecoration;
			private Color Color;
			public Style(string fontName, UnitPx fontSize, FontWeight fontWeight, FontStyle fontStyle, TextTransform textTransform, TextDecoration textDecoration, Color color)
			{
				this.FontName = fontName;
				this.FontSize = fontSize;
				this.FontWeight = fontWeight;
				this.FontStyle = fontStyle;
				this.TextTransform = textTransform;
				this.TextDecoration = textDecoration == TextDecoration.Inherit? TextDecoration.None: textDecoration;
				this.Color = color;
			}
			public Style(Record data) : this(
				data["FntN"],
				(double)data["Sz  "],
				((string)data["FntS"] ?? string.Empty).Contains("Bold") ? FontWeight.Bold : FontWeight.Normal,
				((string)data["FntS"] ?? string.Empty).Contains("Italic") ? FontStyle.Italic : FontStyle.Regular,
				data["fontCaps"] == "allCaps" ? TextTransform.Uppercase : TextTransform.None,
				((data["Undl"] ?? "underlineOff") != "underlineOff"? TextDecoration.Underline: TextDecoration.Inherit) |
				(data["strikethrough"] == "xHeightStrikethroughOn"? TextDecoration.Linetrough: TextDecoration.Inherit),
				new Color(data["Clr "], 100))
			{
			}

			public bool Equals(Style obj)
			{
				if(obj == null) return false;

				return this.FontName == obj.FontName
					&& this.FontSize == obj.FontSize
					&& this.FontWeight == obj.FontWeight
					&& this.FontStyle == obj.FontStyle
					&& this.TextTransform == obj.TextTransform
					&& this.TextDecoration == obj.TextDecoration
					&& this.Color == obj.Color
				;
			}

			public static Style operator -(Style value1, Style value2)
			{
				var fontName = value1.FontName != value2.FontName ? value1.FontName : null;
				var fontSize = value1.FontSize != value2.FontSize ? value1.FontSize : null;
				var fontWeight = value1.FontWeight != value2.FontWeight ? value1.FontWeight : FontWeight.Inherit;
				var fontStyle = value1.FontStyle != value2.FontStyle ? value1.FontStyle : FontStyle.Inherit;
				var textTransform = value1.TextTransform != value2.TextTransform ? value1.TextTransform : TextTransform.Inherit;
				var textDecoration = value1.TextDecoration != value2.TextDecoration ? value1.TextDecoration : TextDecoration.Inherit;
				var color = value1.Color != value2.Color ? value1.Color : null;

				return new Style(fontName, fontSize, fontWeight, fontStyle, textTransform, textDecoration, color);
			}

			public Style Normalize(bool defaults)
			{
				var fontName = this.FontName;
				var fontSize = this.FontSize;
				var fontWeight = (this.FontWeight == FontWeight.Inherit || this.FontWeight == FontWeight.Normal) ? defaults ? FontWeight.Inherit : FontWeight.Normal : this.FontWeight;
				var fontStyle = (this.FontStyle == FontStyle.Inherit || this.FontStyle == FontStyle.Regular) ? defaults ? FontStyle.Inherit : FontStyle.Regular : this.FontStyle;
				var textTransform = (this.TextTransform == TextTransform.Inherit || this.TextTransform == TextTransform.None) ? defaults ? TextTransform.Inherit : TextTransform.None : this.TextTransform;
				var textDecoration = (this.TextDecoration == TextDecoration.Inherit || this.TextDecoration == TextDecoration.None) ? defaults ? TextDecoration.Inherit : TextDecoration.None : this.TextDecoration;
				var color = this.Color;

				return new Style(fontName, fontSize, fontWeight, fontStyle, textTransform, textDecoration, color);
			}

			public IProperty[] ToArray()
			{
				var ret = new List<IProperty>();

				ret.Add(new Css.Property.FontFamily(this.FontName));
				ret.Add(new FontSize(this.FontSize));
				ret.Add(new Css.Property.FontWeight(this.FontWeight));
				ret.Add(new Css.Property.FontStyle(this.FontStyle));
				ret.Add(new Css.Property.TextTransform(this.TextTransform));
				ret.Add(new Css.Property.TextDecoration(this.TextDecoration));
				ret.Add(new Css.Property.Color(this.Color));

				return ret.ToArray();
			}

			private static T GetCommonValue<T>(IEnumerable<Style> values, Converter<Style, T> converter)
			{
				var stat = new Dictionary<T, decimal>();
				int count = 0;
				foreach(var value in values)
				{
					++count;
					var key = converter(value);
					if(key != null)
					{
						if(!stat.ContainsKey(key))
						{
							stat.Add(key, 0);
						}
						++stat[key];
					}
				}
				foreach(var item in stat)
				{
					if(item.Value / count > .5m)
					{
						return item.Key;
					}
				}
				return default(T);
			}
			public static Style GetCommonStyle(IEnumerable<Style> values)
			{
				var fontName = GetCommonValue(values, x => x.FontName);
				var fontSize = GetCommonValue(values, x => x.FontSize);
				var fontWeight = GetCommonValue(values, x => x.FontWeight);
				var fontStyle = GetCommonValue(values, x => x.FontStyle);
				var textTransform = GetCommonValue(values, x => x.TextTransform);
				var textDecoration = GetCommonValue(values, x => x.TextDecoration);
				var color = GetCommonValue(values, x => x.Color);

				return new Style(fontName, fontSize, fontWeight, fontStyle, textTransform, textDecoration, color);
			}
		}

		public class Block
		{
			public readonly int From;
			public int To;
			public readonly Style Style;
			public Block(int from, int to, Style style)
			{
				this.From = from;
				this.To = to;
				this.Style = style;
			}
			public Block(Record data) : this(data["From"], data["T   "], new Style(data["TxtS"]))
			{
			}

			public static Block[] Parse(Record data)
			{
				var blocks = new List<Block>();
				foreach(var item in (List<Record>)data)
				{
					blocks.Add(new Block(item));
				}
				var ret = new List<Block>();
				if(blocks.Count > 0)
				{
					ret.Add(blocks[0]);
					foreach(var block in blocks)
					{
						var last = ret[ret.Count - 1];
						if(block.Style.Equals(last.Style))
						{
							last.To = block.To;
						}
						else
						{
							ret.Add(block);
						}
					}
				}
				return ret.ToArray();
			}
		}

		public readonly string Value;
		public readonly Block[] Blocks;
		public Text(string value, Block[] blocks)
		{
			this.Value = value;
			this.Blocks = blocks;
		}

		internal static Text Parse(Record data)
		{
			if(data["Txt "] != null)
			{
				data = data["Txt "];

				return new Text(data["Txt "], Block.Parse(data["Txtt"]));
			}
			return null;
		}
	}

	internal class Record
	{
		private readonly object @this;
		private Record(object value)
		{
			this.@this = value;
		}
		public Record(Dictionary<string, Record> value) : this((object)value)
		{
		}
		public Record(IList<Record> value) : this((object)value)
		{
		}
		public Record(string value) : this((object)value)
		{
		}
		public Record(bool value) : this((object)value)
		{
		}
		public Record(int value) : this((object)value)
		{
		}
		public Record(double value) : this((object)value)
		{
		}

		public Record this[string name]
		{
			get
			{
				var dic = (Dictionary<string, Record>)@this;
				return dic != null && dic.ContainsKey(name)? dic[name]: null;
			}
		}

		public static implicit operator string(Record value)
		{
			return value == null? null: (string)value.@this;
		}
		public static implicit operator bool(Record value)
		{
			return value == null? default(bool): (bool)value.@this;
		}
		public static implicit operator int(Record value)
		{
			return value == null ? default(int) : (int)value.@this;
		}
		public static implicit operator double(Record value)
		{
			return value == null ? default(double) : (double)value.@this;
		}
		public static implicit operator List<Record>(Record value)
		{
			return value == null ? default(List<Record>) : (List<Record>)value.@this;
		}

		public static readonly Record Empty = new Record((object)null);

		public override string ToString()
		{
			return this.@this.ToString();
		}
	}

	internal class Document : Ps2Css.Document
	{
		public Document(string data, Converter<string, Record[]> parse)
		{
			foreach(var record in parse(data))
			{
				var style = GetStyle(record);
				if(style != null)
				{
					base.Styles.Add(style);
				}
			}
		}

		private static Style GetStyle(Record data)
		{
			var style = new Style();

			var path = data["path"];
			if(path != null)
			{
				style.Add(Common.BorderRadiusValue.Parse(path));
			}
			var layer = data["layer"] ?? data;
			if(layer != null)
			{
				if(layer["Vsbl"] != null && (bool)layer["Vsbl"])
				{
					style.Name = layer["Nm  "];
					style.Add(Common.BackgroundColorValue.Parse(layer));
					int globalAngle = layer["gblA"];
					if((bool)layer["lfxv"])
					{
						var Lefx = layer["Lefx"];

						var overlay = Common.Overlay.Parse(Lefx);
						style.Add(Common.Shadows.Parse(globalAngle, overlay, Lefx));
						style.Add(Common.BorderValue.Parse(Lefx));
						style.Add(Common.GradientValue.Parse(overlay, Lefx));
					}
					if(style.Contains<Border>() && style.Get<Border>().ToString().Contains("rgba") && (style.Contains<BackgroundImage>() || style.Contains<BackgroundColor>()))
					{
						style.Add(new BackgroundClip(BackgroundClip.Value.PaddingBox));
					}
					if(!style.Contains<Width>())
					{
						style.Add(Common.Size.Parse(layer));
					}
					var text = Text.Parse(layer);
					if(text != null)
					{
						var textStyle = Text.Style.GetCommonStyle(text.Blocks.ConvertAll(x => x.Style));
						textStyle = textStyle.Normalize(true);
						style.Add(textStyle.ToArray());
						textStyle = textStyle.Normalize(false);
						for(int i = 0; i < text.Blocks.Length; i++)
						{
							var span = new Span();
							var from = Math.Min(text.Blocks[i].From, text.Value.Length);
							var to = Math.Min(text.Blocks[i].To, text.Value.Length);
							span.Text = text.Value.Substring(from, to - from);
							span.Add((text.Blocks[i].Style - textStyle).ToArray());
							style.Text.Add(span);
						}
						var boxShadow = style.Get<BoxShadow>();
						if(boxShadow != null)
						{
							style.Remove(boxShadow);
							style.Add(new TextShadow(boxShadow.value.ConvertAll(x => { x.SkipSize = true; return x; })));
						}
						var width = style.Get<Width>();
						if(width != null)
						{
							style.Remove(width);
						}
						var height = style.Get<Height>();
						if(height != null)
						{
							style.Remove(height);
						}
					}
				}
			}

			return style.Count > 0 ? style : null;
		}
	}
}
