using System;

namespace Ps2Css.Css.Property
{
	public class Border : IProperty
	{
		private readonly BorderValue value;
		public Border(BorderValue value)
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
					return string.Format("border: {0};", value);
				case Type.sass:
					return string.Format("border: {0}", value);
				default:
					throw new NotSupportedException();
			}
		}
	}
}
