using System;
using System.Collections.Generic;

namespace Ps2Css.Css.Property
{
	public class TextDecoration : IProperty
	{
		private readonly Common.Text.TextDecoration value;
		internal TextDecoration(Common.Text.TextDecoration value)
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
			var values = new List<string>();
			if((this.value & Common.Text.TextDecoration.Underline) == Common.Text.TextDecoration.Underline)
			{
				values.Add("underline");
			}
			if((this.value & Common.Text.TextDecoration.Linetrough) == Common.Text.TextDecoration.Linetrough)
			{
				values.Add("line-through");
			}
			var value = string.Join(" ", values.ToArray());

			if(value == string.Empty) return null;

			switch(type)
			{
				case Type.css:
				case Type.scss:
					return string.Format("text-decoration: {0};", value);
				case Type.sass:
					return string.Format("text-decoration: {0}", value);
				default:
					throw new NotSupportedException();
			}
		}
	}
}
