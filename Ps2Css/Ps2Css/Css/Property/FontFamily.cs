using System;

namespace Ps2Css.Css.Property
{
	public class FontFamily : IProperty
	{
		private readonly string value;
		public FontFamily(string value)
		{
			this.value = value;
		}

		public Vendors SupportedVendors
		{
			get
			{
				return Vendors.w3c;
			}
		}

		public string ToString(Type type, Vendors vendor)
		{
			if(this.value == null) return null;
			var value = string.Format(this.value.Contains(" ")? @"""{0}""": "{0}", this.value);

			switch(type)
			{
				case Type.css:
				case Type.scss:
					return string.Format("font-family: {0};", value);
				case Type.sass:
					return string.Format("font-family: {0}", value);
				default:
					throw new NotSupportedException();
			}
		}
	}
}
