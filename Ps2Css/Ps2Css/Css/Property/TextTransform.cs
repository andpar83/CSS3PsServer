using System;

namespace Ps2Css.Css.Property
{
	public class TextTransform : IProperty
	{
		private readonly Common.Text.TextTransform value;
		internal TextTransform(Common.Text.TextTransform value)
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
				case Common.Text.TextTransform.Inherit:
					return null;
				case Common.Text.TextTransform.None:
					value = "none";
					break;
				case Common.Text.TextTransform.Uppercase:
					value = "uppercase";
					break;
				default:
					throw new NotImplementedException();
			}
			if(value == null) return null;

			switch(type)
			{
				case Type.css:
				case Type.scss:
					return string.Format("text-transform: {0};", value);
				case Type.sass:
					return string.Format("text-transform: {0}", value);
				default:
					throw new NotSupportedException();
			}
		}
	}
}
