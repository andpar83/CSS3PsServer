using System;
using System.Collections.Generic;

namespace Ps2Css.Css.Property
{
	public class BackgroundImage : IProperty
	{
		private readonly IEnumerable<LinearGradientValue> value;
		public BackgroundImage(IEnumerable<LinearGradientValue> value)
		{
			this.value = value;
		}

		public Vendors SupportedVendors
		{
			get
			{
				return Vendors.webkit | Vendors.moz | Vendors.o | Vendors.ms;
			}
		}

		public string ToString(Type type, Vendors vendor)
		{
			var value = string.Join(", ", this.value.ConvertAll(x => x.ToString()).ToArray());

			if(value == string.Empty) return null;

			switch(type)
			{
				case Type.css:
					var prefix = string.Empty;
					switch(vendor)
					{
						case Vendors.w3c:
							value = value.Replace("(top", "(to bottom").Replace("(bottom", "(to top").Replace("(left", "(to right").Replace("(right", "(to left");
							break;
						case Vendors.ms:
							prefix = "-ms-";
							break;
						case Vendors.o:
							prefix = "-o-";
							break;
						case Vendors.moz:
							prefix = "-moz-";
							break;
						case Vendors.webkit:
							prefix = "-webkit-";
							break;
					}
					return string.Format("background-image: {1}{0};", value, prefix);
				case Type.scss:
					return string.Format("@include background-image({0});", value);
				case Type.sass:
					return string.Format("+background-image({0})", value);
				default:
					throw new NotSupportedException();
			}
		}
	}
}
