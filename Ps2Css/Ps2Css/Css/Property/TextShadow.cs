using System;
using System.Collections.Generic;

namespace Ps2Css.Css.Property
{
	public class TextShadow : IProperty
	{
		private readonly IEnumerable<ShadowValue> value;
		public TextShadow(IEnumerable<ShadowValue> value)
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
			var value = string.Join(", ", this.value.ConvertAll(x => x.ToString()).ToArray());

			if(value == string.Empty) return null;

			switch(type)
			{
				case Type.css:
					return string.Format("text-shadow: {0};", value);
				case Type.scss:
					return string.Format("@include text-shadow({0});", value);
				case Type.sass:
					return string.Format("+text-shadow({0})", value);
				default:
					throw new NotSupportedException();
			}
		}
	}
}
