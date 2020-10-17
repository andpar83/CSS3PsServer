using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Ps2Css.Css;
using Ps2Css.Css.Property;

namespace Ps2Css.Asl
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

		internal static List<byte[]> Split(byte[] data, string regex, bool includePrefix)
		{
			var ret = new List<byte[]>();

			var text = Encoding.ASCII.GetString(data);
			var matches = Regex.Matches(text, regex, RegexOptions.Compiled | RegexOptions.Singleline);
			var indexes = new List<int>();
			if(includePrefix)
			{
				indexes.Add(0);
			}
			foreach(Match match in matches)
			{
				indexes.Add(match.Index);
			}
			for(int i = 0; i < indexes.Count; i++)
			{
				var i1 = indexes[i];
				var i2 = i < indexes.Count - 1 ? indexes[i + 1] : data.Length;
				var value = new byte[i2 - i1];
				Array.Copy(data, i1, value, 0, value.Length);
				ret.Add(value);
			}

			return ret;
		}
		private static List<byte[]> GetStyles(byte[] data)
		{
			return Split(data, "\x10\0\0\0\x01\0\0\0\0\0\0null", false);
		}
		private static Dictionary<string, byte[]> GetGroups(byte[] data)
		{
			var ret = new Dictionary<string, byte[]>();

			var list = Split(data, @"[A-Za-z\ #]+(?<=DrSh|IrSh|OrGl|IrGl|GrFl|SoFi|FrFX)Objc"/**/, true);
			foreach(var item in list)
			{
				ret.Add(Regex.Match(Encoding.ASCII.GetString(item), @"^[A-Za-z\ #]+").Value, item);
			}

			return ret;
		}
		internal static T Get<T>(byte[] data, string label)
		{
			object ret = null;

			var text = Encoding.ASCII.GetString(data);
			var index = text.IndexOf(label);
			if(index != -1)
			{
				index += label.Length;

				if(label.EndsWith("TEXT"))
				{
					var length = Get<Int32>(data, index, BitConverter.ToInt32);
					index += sizeof(Int32);
					var value = new char[length];
					for(int i = 0; i < length; i++)
					{
						value[i] = Get<Char>(data, index, BitConverter.ToChar);
						index += sizeof(Char);
					}
					ret = new string(value).TrimEnd('\0');
				}
				else
				if(label.EndsWith("long") || label.EndsWith("VlLs") || label.EndsWith("TrnS"))
				{
					ret = Get<Int32>(data, index, BitConverter.ToInt32);
				}
				else
				if(label.EndsWith("bool"))
				{
					ret = Get<Boolean>(data, index, BitConverter.ToBoolean);
				}
				else
				if(label.EndsWith("enum"))
				{
					ret = Encoding.ASCII.GetString(data, index + 12, 4);
				}
				else
				{
					ret = Get<Double>(data, index, BitConverter.ToDouble);
				}
			}
			return ret is T? (T)ret : default(T);
		}
		private static T Get<T>(byte[] data, int index, BitConverterDelegate<T> convert)
		{
			int size = 1;
			if(typeof(T) == typeof(Double))
			{
				size = sizeof(Double);
			}
			if(typeof(T) == typeof(Int32))
			{
				size = sizeof(Int32);
			}
			if(typeof(T) == typeof(Boolean))
			{
				size = sizeof(Boolean);
			}
			if(typeof(T) == typeof(Char))
			{
				size = sizeof(Char);
			}

			if(index + size - 1 >= data.Length) return default(T);

			var value = new byte[size];
			for(int i = 0; i < size; i++)
			{
				value[i] = data[index + size - i - 1];
			}
			return convert(value, 0);
		}
		private delegate T BitConverterDelegate<T>(byte[] value, int startIndex);

		private static Style GetStyle(byte[] data)
		{
			var groups = GetGroups(data);

			var style = new Style();
			style.Name = Get<string>(groups[string.Empty], "Nm  TEXT");
			var overlay = Overlay.Parse(groups);
			style.Add(ShadowValue.Parse(overlay, groups));
			style.Add(BorderValue.Parse(groups));
			style.Add(GradientValue.Parse(overlay, groups));
			return style;
		}
	}

	internal class Color : Ps2Css.Color
	{
		public Color(byte[] data) : base(
			Document.Get<double>(data, "Rd  doub"),
			Document.Get<double>(data, "Grn doub"),
			Document.Get<double>(data, "Bl  doub"),
			Document.Get<double>(data, "OpctUntF#Prc")
		)
		{
		}
	}
	internal class Offset : Ps2Css.Offset
	{
		public Offset(byte[] data) : base(
			Document.Get<double>(data, "laglUntF#Ang"),
			Document.Get<double>(data, "DstnUntF#Pxl")
		)
		{
		}
	}

	internal class ShadowValue : Ps2Css.ShadowValue
	{
		private ShadowValue(bool inset, Color overlay, byte[] data) : base(
			inset,
			overlay,
			new Color(data),
			new Offset(data),
			Document.Get<double>(data, "blurUntF#Pxl"),
			0
		)
		{
		}

		internal static IProperty Parse(Color overlay, Dictionary<string, byte[]> groups)
		{
			var value = new List<Ps2Css.ShadowValue>();

			if(groups.ContainsKey("DrShObjc"))
			{
				value.Add(new ShadowValue(false, overlay, groups["DrShObjc"]));
			}
			if(groups.ContainsKey("IrShObjc"))
			{
				value.Add(new ShadowValue(true, overlay, groups["IrShObjc"]));
			}
			if(groups.ContainsKey("OrGlObjc"))
			{
				value.Add(new ShadowValue(false, overlay, groups["OrGlObjc"]));
			}
			if(groups.ContainsKey("IrGlObjc"))
			{
				value.Add(new ShadowValue(true, overlay, groups["IrGlObjc"]));
			}

			return new BoxShadow(value);
		}
	}
	internal class BorderValue : Ps2Css.BorderValue
	{
		private BorderValue(byte[] data) : base(
			Document.Get<double>(data, "Sz  UntF#Pxl"),
			new Color(data)
		)
		{
		}

		internal static IProperty Parse(Dictionary<string, byte[]> groups)
		{
			if(groups.ContainsKey("FrFXObjc"))
			{
				return new Border(new BorderValue(groups["FrFXObjc"]));
			}
			return null;
		}
	}
	internal class GradientValue : Ps2Css.LinearGradientValue
	{
		private new class Transition : Ps2Css.LinearGradientValue.Transition
		{
			public Transition(byte[] data) : base(
				new Color(data),
				Math.Round(Document.Get<int>(data, "Lctnlong") / 4096m, 2)
			)
			{
			}
		}

		private GradientValue(Color overlay, byte[] data) : base(
			overlay,
			Document.Split(Document.Split(data, "ClrsVlLs", false)[0], "Clrt", false).ConvertAll(transition => new Transition(transition)).ToArray(),
			Document.Split(Document.Split(data, "TrnsVlLs", false)[0], "TrnS", false).ConvertAll(transition => new Transition(transition)).ToArray(),
			100,
			Document.Get<double>(data, "AnglUntF#Ang"),
			Document.Get<bool>(data, "Rvrsbool")
		)
		{
		}

		internal static IProperty Parse(Color overlay, Dictionary<string, byte[]> groups)
		{
			var value = new List<Ps2Css.LinearGradientValue>();

			if(groups.ContainsKey("GrFlObjc") && Document.Get<string>(groups["GrFlObjc"], "GrdFenum") == "CstS")
			{
				value.Add(new GradientValue(overlay, groups["GrFlObjc"]));
			}

			return new BackgroundImage(value);
		}
	}

	internal class Overlay
	{
		internal static Color Parse(Dictionary<string, byte[]> groups)
		{
			if(groups.ContainsKey("SoFiObjc"))
			{
				return new Color(groups["SoFiObjc"]);
			}
			return null;
		}
	}
}
