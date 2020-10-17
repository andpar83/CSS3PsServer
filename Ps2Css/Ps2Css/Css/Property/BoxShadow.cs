using System;
using System.Collections.Generic;

namespace Ps2Css.Css.Property
{
	public class BoxShadow : IProperty
	{
		internal readonly IEnumerable<ShadowValue> value;
		public BoxShadow(IEnumerable<ShadowValue> value)
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
			var value = string.Join(", ", this.value.ConvertAll(x => x.ToString()).ToArray());

			if(value == string.Empty) return null;

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
					return string.Format("{1}box-shadow: {0};", value, prefix);
				case Type.scss:
					return string.Format("@include box-shadow({0});", value);
				case Type.sass:
					return string.Format("+box-shadow({0})", value);
				default:
					throw new NotSupportedException();
			}
		}
	}
}
