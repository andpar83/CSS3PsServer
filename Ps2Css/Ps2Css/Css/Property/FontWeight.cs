using System;

namespace Ps2Css.Css.Property
{
	public class FontWeight : IProperty
	{
		private readonly Common.Text.FontWeight value;
		internal FontWeight(Common.Text.FontWeight value)
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
				case Common.Text.FontWeight.Inherit:
					return null;
				case Common.Text.FontWeight.Normal:
					value = "normal";
					break;
				case Common.Text.FontWeight.Bold:
					value = "bold";
					break;
				default:
					throw new NotImplementedException();
			}

			switch(type)
			{
				case Type.css:
				case Type.scss:
					return string.Format("font-weight: {0};", value);
				case Type.sass:
					return string.Format("font-weight: {0}", value);
				default:
					throw new NotSupportedException();
			}
		}
	}
}
