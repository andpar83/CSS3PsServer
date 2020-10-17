using System;

namespace Ps2Css.Css.Property
{
	internal class Color : IProperty
	{
		private readonly Ps2Css.Color value;
		public Color(Ps2Css.Color value)
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
			if(value == null) return null;

			switch(type)
			{
				case Type.css:
				case Type.scss:
					return string.Format("color: {0};", value);
				case Type.sass:
					return string.Format("color: {0}", value);
				default:
					throw new NotSupportedException();
			}
		}
	}
}
