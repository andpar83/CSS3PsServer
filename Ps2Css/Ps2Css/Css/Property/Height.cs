using System;

namespace Ps2Css.Css.Property
{
	public class Height : IProperty
	{
		private readonly UnitPx value;
		public Height(UnitPx value)
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
					return string.Format("height: {0};", value);
				case Type.sass:
					return string.Format("height: {0}", value);
				default:
					throw new NotSupportedException();
			}
		}
	}
}
