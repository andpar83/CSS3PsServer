using System;
using System.Collections.Generic;

namespace Ps2Css.Css
{
	public class Span
	{
		public string Text
		{
			get;
			internal set;
		}

		private static Vendors[] vendors;
		static Span()
		{
			vendors = (Vendors[])Enum.GetValues(typeof(Vendors));
			Array.Reverse(vendors);
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
	}
}
