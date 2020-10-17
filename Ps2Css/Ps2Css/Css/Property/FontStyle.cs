using System;

namespace Ps2Css.Css.Property
{
	public class FontStyle : IProperty
	{
		private readonly Common.Text.FontStyle value;
		internal FontStyle(Common.Text.FontStyle value)
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
			string value = null;
			switch(this.value)
			{
				case Common.Text.FontStyle.Inherit:
					return null;
				case Common.Text.FontStyle.Regular:
					value = "normal";
					break;
				case Common.Text.FontStyle.Italic:
					value = "italic";
					break;
				case Common.Text.FontStyle.Oblique:
					value = "oblique";
					break;
				default:
					throw new NotImplementedException();
			}

			switch(type)
			{
				case Type.css:
				case Type.scss:
					return string.Format("font-style: {0};", value);
				case Type.sass:
					return string.Format("font-style: {0}", value);
				default:
					throw new NotSupportedException();
			}
		}
	}
}
