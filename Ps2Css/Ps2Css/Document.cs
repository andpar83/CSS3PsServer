using System.Collections.Generic;
using Ps2Css.Css;

namespace Ps2Css
{
	public class Document
	{
		private readonly ICollection<Style> styles = new List<Style>();
		public ICollection<Style> Styles
		{
			get
			{
				return this.styles;
			}
		}
	}
}
