using System;

namespace Ps2Css.Css
{
	[Flags]
	public enum Vendors
	{
		w3c = 0,
		ms = 1,
		o = 2,
		moz = 4,
		webkit = 8,
		legacy = 16
	}
	[Flags]
	public enum Type
	{
		css = 0,
		scss = 1,
		sass = 2
	}
	public interface IProperty
	{
		Vendors SupportedVendors
		{
			get;
		}
		string ToString(Type type, Vendors vendor);
	}
}
