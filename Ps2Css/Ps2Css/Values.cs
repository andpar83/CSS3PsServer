using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;

namespace Ps2Css
{
	public class Color
	{
		internal readonly int r;
		internal readonly int g;
		internal readonly int b;
		internal readonly decimal a;
		/// <param name="opacity">Opacity [0..100]</param>
		public Color(double r, double g, double b, double opacity)
		{
			this.r = (int)Math.Round(r);
			this.g = (int)Math.Round(g);
			this.b = (int)Math.Round(b);
			this.a = (decimal)((int)Math.Round(opacity)) / 100;
		}

		public override string ToString()
		{
			if(this.a == 1.00m)
			{
				if(r / 0x10 == r % 0x10 && g / 0x10 == g % 0x10 && b / 0x10 == b % 0x10)
				{
					return string.Format(CultureInfo.InvariantCulture, "#{0:x}{1:x}{2:x}", r / 0x10, g / 0x10, b / 0x10);
				}
				else
				{
					return string.Format(CultureInfo.InvariantCulture, "#{0:x2}{1:x2}{2:x2}", r, g, b);
				}
			}
			else
			{
				return string.Format(CultureInfo.InvariantCulture, "rgba({0},{1},{2},{3:#.##;#.##;0})", r, g, b, a);
			}
		}

		public bool Equals(Color color)
		{
			if(color == null) return false;

			return this.a == color.a
				&& this.r == color.r
				&& this.g == color.g
				&& this.b == color.b
			;
		}
		public override bool Equals(object obj)
		{
			if(!(obj is Color)) return false;

			return this.Equals(obj as Color);
		}
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		public static bool operator ==(Color value1, Color value2)
		{
			if(object.Equals(value1, value2)) return true;
			if(object.Equals(value1, null)) return false;
			return value1.Equals(value2);
		}
		public static bool operator !=(Color value1, Color value2)
		{
			if(object.Equals(value1, value2)) return false;
			if(object.Equals(value1, null)) return true;
			return !value1.Equals(value2);
		}
	}
	public class UnitPx
	{
		private readonly double value;
		public UnitPx(double value)
		{
			this.value = value;
		}

		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, (int)this == 0 ? "0" : "{0}px", (int)this);
		}
		public override int GetHashCode()
		{
			return value.GetHashCode();
		}
		private const double E = 0.1;
		public bool Equals(UnitPx value)
		{
			return object.Equals(value, null) ? false : Math.Abs(this.value - value.value) < E;
		}
		public override bool Equals(object obj)
		{
			return this.Equals(obj as UnitPx);
		}
		public static bool operator ==(UnitPx value1, UnitPx value2)
		{
			return (object.Equals(value1, null) == object.Equals(value2, null)) ? !object.Equals(value1, null) ? value1.Equals(value2) : true : false;
		}
		public static bool operator !=(UnitPx value1, UnitPx value2)
		{
			return (object.Equals(value1, null) == object.Equals(value2, null)) ? !object.Equals(value1, null) ? !value1.Equals(value2) : false : true;
		}

		public static implicit operator UnitPx(double value)
		{
			return new UnitPx(value);
		}
		public static explicit operator int(UnitPx value)
		{
			return (int)Math.Round(value.value);
		}
	}
	public class Offset
	{
		private readonly UnitPx x;
		private readonly UnitPx y;
		public Offset(double angle, double distance)
		{
			if(double.IsNaN(angle) || double.IsNaN(distance))
			{
				this.x = 0;
				this.y = 0;
			}
			else
			{
				if(distance < 2.0)
				{
					angle = Math.Round(angle / 45) * 45;
				}
				angle = (angle + 180) * Math.PI / 180;
				this.x = +distance * Math.Cos(angle);
				this.y = -distance * Math.Sin(angle);
			}
		}

		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0} {1}", this.x, this.y);
		}
	}
	public class Size
	{
		private readonly Rectangle rectangle;

		public Size(int left, int top, int right, int bottom)
		{
			this.rectangle = new Rectangle(left, top, right - left, bottom - top);
		}

		public UnitPx Width
		{
			get
			{
				return this.rectangle.Width;
			}
		}
		public UnitPx Height
		{
			get
			{
				return this.rectangle.Height;
			}
		}
	}

	public class ShadowValue
	{
		private readonly bool inset;
		private readonly Color color;
		private readonly Offset offset;
		private readonly UnitPx blur;
		private readonly UnitPx size;

		private ShadowValue(bool inset, Color overlay, Color color, Offset offset, UnitPx blur, UnitPx size)
		{
			this.inset = inset;
			this.color = color;
			this.offset = offset;
			this.blur = blur;
			this.size = size;
		}
		public ShadowValue(bool inset, Color overlay, Color color, Offset offset, double blur, double choke) : this(inset, overlay, color, offset, GetBlur(blur, choke), GetSize(blur, choke))
		{
		}

		private static UnitPx GetSize(double blur, double choke)
		{
			return blur * choke / 100;
		}
		private static UnitPx GetBlur(double blur, double choke)
		{
			return blur - blur * choke / 100;
		}

		public bool SkipSize
		{
			set;
			get;
		}
		public override string ToString()
		{
			var ret = new List<string>();

			if(this.inset)
			{
				ret.Add("inset");
			}
			ret.Add(this.offset.ToString());
			if((int)this.blur > 0 || (int)this.size > 0)
			{
				ret.Add(this.blur.ToString());
			}
			if((int)this.size > 0 && !this.SkipSize)
			{
				ret.Add(this.size.ToString());
			}
			ret.Add(this.color.ToString());

			return string.Join(" ", ret.ToArray());
		}
	}
	public class BorderValue
	{
		private readonly UnitPx size;
		private readonly Color color;

		public BorderValue(UnitPx size, Color color)
		{
			this.size = size;
			this.color = color;
		}

		public override string ToString()
		{
			return string.Format("solid {0} {1}", this.size, this.color);
		}
	}
	public class LinearGradientValue
	{
		protected internal class Transition
		{
			public readonly Color color;
			public readonly decimal offset;

			public Transition(Color color, decimal offset)
			{
				this.color = color;
				this.offset = offset;
			}

			public override string ToString()
			{
				return string.Format(this.offset > 0 && this.offset < 1? "{0} {1:0}%": "{0}", this.color, this.offset * 100);
			}
		}

		private class color
		{
			public double r;
			public double g;
			public double b;
			public double a;

			public bool Equals(color color)
			{
				if(!object.Equals(color, null))
				{
					return this.a == color.a && this.r == color.r && this.g == color.g && this.b == color.b;
				}
				return false;
			}

			public static implicit operator color(Color value)
			{
				return new color()
				{
					r = value.r,
					g = value.g,
					b = value.b,
					a = (double)value.a
				};
			}
			public static implicit operator Color(color value)
			{
				return new Color(value.r, value.g, value.b, value.a * 100);
			}

			public color(double r, double g, double b, double a)
			{
				this.r = r;
				this.g = g;
				this.b = b;
				this.a = a;
			}
			public color() : this(0, 0, 0, 0)
			{
			}
			public color(int index, int index1, int index2, color color1, color color2)
			{
				this.r = mix(index, index1, index2, color1.r, color2.r);
				this.g = mix(index, index1, index2, color1.g, color2.g);
				this.b = mix(index, index1, index2, color1.b, color2.b);
			}
			public color(int index, int index1, int index2, double a1, double a2)
			{
				this.a = mix(index, index1, index2, a1, a2);
			}

			private static double mix(int index, int index1, int index2, double value1, double value2)
			{
				if(index1 == index2)
				{
					return value1; // or value2
				}
				else
				{
					return (value2 - value1) * (index - index1) / (index2 - index1) + value1;
				}
			}
		}
		private static int FindMaxLTE(ICollection<int> line, int index)
		{
			var indexes = new int[line.Count];
			line.CopyTo(indexes, 0);
			Array.Sort(indexes);
			var i = Array.FindLastIndex(indexes, x => x <= index);
			return indexes[i == -1? 0: i];
		}
		private static int FindMinGTE(ICollection<int> line, int index)
		{
			var indexes = new int[line.Count];
			line.CopyTo(indexes, 0);
			Array.Sort(indexes);
			var i = Array.FindIndex(indexes, x => x >= index);
			return indexes[i == -1? indexes.Length - 1: i];
		}
		private const int PRECISION = 100;

		private readonly Transition[] ColorStop;
		private readonly int Angle;

		/// <param name="opacity">Opacity [0..100]</param>
		/// <param name="angle">Angle in grad</param>
		protected LinearGradientValue(Color overlay, Transition[] color, Transition[] transparency, double opacity, double angle, bool reverse)
		{
			#region this.ColorStop = ...
			{
				var cLine = new Dictionary<int, List<color>>();
				var tLine = new Dictionary<int, List<color>>();
				var rainbow = new Dictionary<int, color[]>();

				foreach(var item in color)
				{
					var index = (int)Math.Round(item.offset * PRECISION);
					if(!cLine.ContainsKey(index))
					{
						cLine[index] = new List<color>();
					}
					cLine[index].Add(item.color);
					if(!rainbow.ContainsKey(index))
					{
						rainbow[index] = new color[] { new color(), new color() };
					}
				}
				foreach(var item in transparency)
				{
					var index = (int)Math.Round(item.offset * PRECISION);
					if(!tLine.ContainsKey(index))
					{
						tLine[index] = new List<color>();
					}
					tLine[index].Add(item.color);
					if(!rainbow.ContainsKey(index))
					{
						rainbow[index] = new color[] { new color(), new color() };
					}
				}

				foreach(var index in cLine.Keys)
				{
					var index1 = FindMaxLTE(tLine.Keys, index);
					var index2 = FindMinGTE(tLine.Keys, index);
					if(index1 != index2)
					{
						var t1 = tLine[index1][tLine[index1].Count - 1];
						var t2 = tLine[index2][0];
						var tR = new color(index, index1, index2, t1.a, t2.a);
						rainbow[index][0].a = tR.a * opacity / 100;
						rainbow[index][1].a = tR.a * opacity / 100;
					}
					else
					{
						var t1 = tLine[index1][0];
						var t2 = tLine[index2][tLine[index1].Count - 1];
						rainbow[index][0].a = t1.a * opacity / 100;
						rainbow[index][1].a = t2.a * opacity / 100;
					}

					var c1 = cLine[index][0];
					var c2 = cLine[index][cLine[index].Count - 1];
					rainbow[index][0].r = c1.r;
					rainbow[index][0].g = c1.g;
					rainbow[index][0].b = c1.b;
					rainbow[index][1].r = c2.r;
					rainbow[index][1].g = c2.g;
					rainbow[index][1].b = c2.b;
				}
				foreach(var index in tLine.Keys)
				{
					var index1 = FindMaxLTE(cLine.Keys, index);
					var index2 = FindMinGTE(cLine.Keys, index);
					if(index1 != index2)
					{
						var c1 = cLine[index1][cLine[index1].Count - 1];
						var c2 = cLine[index2][0];
						var cR = new color(index, index1, index2, c1, c2);
						rainbow[index][0].r = cR.r;
						rainbow[index][0].g = cR.g;
						rainbow[index][0].b = cR.b;
						rainbow[index][1].r = cR.r;
						rainbow[index][1].g = cR.g;
						rainbow[index][1].b = cR.b;
					}
					else
					{
						var c1 = cLine[index1][0];
						var c2 = cLine[index2][cLine[index1].Count - 1];
						rainbow[index][0].r = c1.r;
						rainbow[index][0].g = c1.g;
						rainbow[index][0].b = c1.b;
						rainbow[index][1].r = c2.r;
						rainbow[index][1].g = c2.g;
						rainbow[index][1].b = c2.b;
					}

					var t1 = tLine[index][0];
					var t2 = tLine[index][tLine[index].Count - 1];
					rainbow[index][0].a = t1.a * opacity / 100;
					rainbow[index][1].a = t2.a * opacity / 100;
				}

				var values = new List<Transition>();
				{
					var keys = new int[rainbow.Keys.Count];
					rainbow.Keys.CopyTo(keys, 0);
					Array.Sort(keys);

					var index1 = (rainbow[keys[0]][1].Equals(rainbow[keys[1]][0])) ? 1 : 0;
					var index2 = (rainbow[keys[keys.Length - 1]][0].Equals(rainbow[keys[keys.Length - 2]][1])) ? keys.Length - 1 : keys.Length;
					for(int i = index1; i < index2; i++)
					{
						var value1 = rainbow[keys[i]][0];
						var value2 = rainbow[keys[i]][1];
						var offset = (decimal)keys[i] / PRECISION;
						if(value1.Equals(value2))
						{
							values.Add(new Transition(value1, offset));
						}
						else
						{
							values.Add(new Transition(value1, offset));
							values.Add(new Transition(value2, offset));
						}
					}
				}
				this.ColorStop = values.ToArray();
			}
			#endregion
			#region Direction = ...
			{
				int value = (int)Math.Round(angle);
				if(reverse) value += 180;
				while(value < 0) value += 360;
				if(value > 180) value -= 360;
				this.Angle = value;
			}
			#endregion
		}

		public override string ToString()
		{
			string direction;
			switch(this.Angle)
			{
				case 0: direction = "left"; break;
				case 90: direction = "bottom"; break;
				case 180: direction = "right"; break;
				case -90: direction = "top"; break;
				default: direction = string.Format("{0}deg", this.Angle); break;
			}

			return string.Format("linear-gradient({0}, {1})", direction, string.Join(", ", Array.ConvertAll(this.ColorStop, x => x.ToString())));
		}
	}
	public class BorderRadiusValue
	{
		private readonly UnitPx tlh;
		private readonly UnitPx tlv;
		private readonly UnitPx trh;
		private readonly UnitPx trv;
		private readonly UnitPx brh;
		private readonly UnitPx brv;
		private readonly UnitPx blh;
		private readonly UnitPx blv;

		public BorderRadiusValue(UnitPx tlh, UnitPx tlv, UnitPx trh, UnitPx trv, UnitPx brh, UnitPx brv, UnitPx blh, UnitPx blv)
		{
			this.tlh = tlh;
			this.tlv = tlv;
			this.trh = trh;
			this.trv = trv;
			this.brh = brh;
			this.brv = brv;
			this.blh = blh;
			this.blv = blv;
		}
		public BorderRadiusValue(UnitPx value) : this(value, value, value, value, value, value, value, value)
		{
		}

		private static string MinimizeGroup(UnitPx tl, UnitPx tr, UnitPx br, UnitPx bl)
		{
			var ret = new Dictionary<string, UnitPx>(4);
			ret.Add("tl", tl);
			ret.Add("tr", tr);
			ret.Add("br", br);
			ret.Add("bl", bl);
			if(bl == tr)
			{
				ret.Remove("bl");
				if(br == tl)
				{
					ret.Remove("br");
					if(tr == tl)
					{
						ret.Remove("tr");
					}
				}
			}
			return string.Join(" ", ret.Values.ConvertAll(x => x.ToString()).ToArray());
		}
		public override string ToString()
		{
			if(tlh == tlv && trh == trv && brh == brv && blh == blv)
			{
				return MinimizeGroup(tlh, trh, brh, blh);
			}
			else
			{
				return string.Format("{0}/{1}", MinimizeGroup(tlh, trh, brh, blh), MinimizeGroup(tlv, trv, brv, blv));
			}
		}
	}
}
