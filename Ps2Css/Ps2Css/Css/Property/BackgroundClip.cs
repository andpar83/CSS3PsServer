using System;

namespace Ps2Css.Css.Property
{
	public class BackgroundClip : IProperty
	{
		public enum Value
		{
			BorderBox,
			PaddingBox,
			ContentBox
		}

		private readonly Value value;
		public BackgroundClip(Value value)
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
			string value = null;
			switch(this.value)
			{
				case Value.BorderBox:
					value = "border-box";
					break;
				case Value.PaddingBox:
					value = "padding-box";
					break;
				case Value.ContentBox:
					value = "content-box";
					break;
			}

			switch(type)
			{
				case Type.css:
					var prefix = string.Empty;
					switch(vendor)
					{
						case Vendors.w3c:
							prefix = "-browser-";
							break;
						case Vendors.webkit:
							prefix = "-webkit-";
							value = value.Replace("-box", string.Empty);
							break;
						case Vendors.moz:
							prefix = "-moz-";
							value = value.Replace("-box", string.Empty);
							break;
					}
					return string.Format("{1}background-clip: {0};", value, prefix);
				case Type.scss:
					return string.Format("@include background-clip({0});", value);
				case Type.sass:
					return string.Format("+background-clip({0})", value);
				default:
					throw new NotSupportedException();
			}
		}
	}
}
