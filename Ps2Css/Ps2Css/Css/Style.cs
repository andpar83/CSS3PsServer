using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Ps2Css.Css
{
	public class Style
	{
		private static Vendors[] vendors;
		static Style()
		{
			vendors = (Vendors[])Enum.GetValues(typeof(Vendors));
			Array.Reverse(vendors);
		}

		internal Style()
		{
			this.Text = new List<Span>();
		}

		public List<Span> Text
		{
			get;
			private set;
		}

		public string Name
		{
			get;
			set;
		}

		public string Comment
		{
			get;
			set;
		}

		private List<IProperty> values = new List<IProperty>();

		public bool Contains<T>() where T : IProperty
		{
			foreach(var value in values)
			{
				if(value is T) return true;
			}
			return false;
		}
		public T Get<T>() where T : IProperty
		{
			foreach(var value in values)
			{
				if(value is T) return (T)value;
			}
			return default(T);
		}

		public void Add(IProperty value)
		{
			if(value != null)
			{
				this.values.Add(value);
			}
		}
		public void Add(IEnumerable<IProperty> values)
		{
			if(values != null)
			{
				foreach(var value in values)
				{
					this.Add(value);
				}
			}
		}

		public void Remove(IProperty value)
		{
			if(value != null)
			{
				this.values.Remove(value);
			}
		}

		internal int Count
		{
			get
			{
				return this.values.Count;
			}
		}

		public string[] ToArray(Type type)
		{
			var lines = new List<string>();

			foreach(var property in this.values)
			{
				switch(type)
				{
					case Type.css:
						foreach(var vendor in vendors)
						{
							if((vendor & property.SupportedVendors) == vendor)
							{
								lines.Add(property.ToString(Type.css, vendor));
							}
						}
						break;
					case Type.scss:
						lines.Add(property.ToString(type, default(Vendors)));
						break;
					case Type.sass:
						lines.Add(property.ToString(type, default(Vendors)));
						break;
				}
			}

			return lines.FindAll(x => x != null).ToArray();
		}
		public string ToString(Type type)
		{
			string format = null;
			switch(type)
			{
				case Type.css:
					format = ".{1}{2}{0}{{{0}\t{3}{0}}}{0}";
					break;
				case Type.scss:
					format = "@import \"compass/css3\";{0}.{1}{2}{0}{{{0}\t{3}{0}}}{0}";
					break;
				case Type.sass:
					format = "@import \"compass/css3\"{0}.{1}{2}{0}\t{3}{0}";
					break;
			}

			return string.Format(format, Environment.NewLine, this.Name, this.Comment != null ? string.Format(" /* {0} */", this.Comment) : null, string.Join(Environment.NewLine + "\t", this.ToArray(type)));
		}
		public override string ToString()
		{
			return this.ToString(Type.css);
		}
	}
}
