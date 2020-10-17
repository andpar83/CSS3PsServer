using System;

namespace Ps2Css.Css.Property
{
	public class BorderRadius : IProperty
	{
		private readonly BorderRadiusValue value;
		public BorderRadius(BorderRadiusValue value)
		{
			this.value = value;
		}

		public Vendors SupportedVendors
		{
			get
			{
				return Vendors.w3c | Vendors.webkit | Vendors.moz;
			}
		}

		public string ToString(Type type, Vendors vendor)
		{
			if(value == null) return null;

			switch(type)
			{
				case Type.css:
					var prefix = string.Empty;
					switch(vendor)
					{
						case Vendors.w3c:
							break;
						case Vendors.moz:
							prefix = "-moz-";
							break;
						case Vendors.webkit:
							prefix = "-webkit-";
							break;
					}
					return string.Format("{1}border-radius: {0};", value, prefix);
				case Type.scss:
					return string.Format("@include border-radius({0});", value.ToString().Replace("/", ", "));
				case Type.sass:
					return string.Format("+border-radius({0})", value.ToString().Replace("/", ", "));
				default:
					throw new NotSupportedException();
			}
		}
	}
}
