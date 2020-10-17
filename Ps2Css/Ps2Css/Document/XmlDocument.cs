using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using Ps2Css.Common;

namespace Ps2Css.Xml
{
	public class Document : Ps2Css.Document
	{
		public Document(string data)
		{
			var document = new Common.Document(data, Parse);
			foreach(var style in document.Styles)
			{
				base.Styles.Add(style);
			}
		}

		private static Record[] Parse(string data)
		{
			var doc = new System.Xml.XmlDocument();
			try
			{
				doc.LoadXml(data.TrimStart('\xfeff'));
			}
			catch(Exception ex)
			{
				throw new CorruptedXmlException(ex);
			}
			var root = doc.FirstChild;
			var layers = root["layers"].ChildNodes;
			var ret = new Record[layers.Count];
			for(var i = 0; i < ret.Length; i++)
			{
				var layer = layers[i];
				var record = new Dictionary<string, Record>();
				record.Add("layer", layer["descriptor"] == null ? Record.Empty : Parse(layer["descriptor"]));
				record.Add("path", layer["path"] == null || layer["path"]["descriptor"] == null ? Record.Empty : Parse(layer["path"]["descriptor"]));
				ret[i] = new Record(record);
			}
			return ret;
		}
		private static Record Parse(XmlNode root)
		{
			switch(root.Name)
			{
				case "descriptor":
					var descriptor = new Dictionary<string, Record>();
					foreach(XmlNode node in root.ChildNodes)
					{
						descriptor.Add(string.IsNullOrEmpty(node.Attributes["charid"].Value) ? node.Attributes["stringid"].Value : node.Attributes["charid"].Value, Parse(node));
					}
					return new Record(descriptor);
				case "list":
					var list = new List<Record>();
					foreach(XmlNode node in root.ChildNodes)
					{
						list.Add(Parse(node));
					}
					return new Record(list);
				case "element":
					switch(root.Attributes["type"].Value.Substring("DescValueType.".Length))
					{
						case "OBJECTTYPE":
						case "LISTTYPE":
							return Parse(root.FirstChild);
						case "BOOLEANTYPE":
							return new Record(bool.Parse(root["value"].InnerText));
						case "INTEGERTYPE":
							return new Record(int.Parse(root["value"].InnerText));
						case "DOUBLETYPE":
						case "UNITDOUBLE":
							return new Record(double.Parse(root["value"].InnerText, CultureInfo.InvariantCulture));
						case "REFERENCETYPE":
							return null;
						case "STRINGTYPE":
						case "ENUMERATEDTYPE":
							return new Record(root["value"].InnerText);
						default:
							return null;
					}
				default:
					return null;
			}
		}
	}

	public class CorruptedXmlException : ApplicationException
	{
		public CorruptedXmlException(Exception ex) : base(string.Empty, ex)
		{
		}
	}
}
