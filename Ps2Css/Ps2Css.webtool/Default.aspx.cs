using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Text.RegularExpressions;
using Ps2Css;
using Ps2Css.Css;

public partial class _Default : Page
{
	private static readonly int InitialDelay;
	static _Default()
	{
		InitialDelay = int.Parse(ConfigurationManager.AppSettings["InitialDelay"]);
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		string guid = Request.QueryString[string.Empty];
		if(guid == null || !Regex.IsMatch(guid, "[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}"))
		{
			Response.StatusCode = 301;
			Response.Status = "301 Moved Permanently";
			Response.AddHeader("Location", SiteUri.ToString());
			Response.End();
		}

		Meta.SQL.IDatabase db = new Meta.SQL.Module().Database;
		Ps2Css.DB.Request.Module module = new Ps2Css.DB.Request.Module(db);
		Ps2Css.DB.Request.Item item = module.GetByID(new Guid(guid));
		if(item != null)
		{
			int diff = (int)Math.Ceiling((DateTime.Now - item.Created).TotalSeconds);
			if(diff < InitialDelay)
			{
				this.wait = InitialDelay - diff;
			}
			else
			{
				this.wait = 0;
				try
				{
					Document doc = new Ps2Css.Xml.Document(Encoding.UTF8.GetString(Zlib.Decompress(item.Data)));
					this.Styles = doc.Styles.ToArray();
				}
				catch(Ps2Css.Xml.CorruptedXmlException ex)
				{
					exception = ex.GetType().ToString();
					base.Log("XML parsing error");
				}
				catch(Exception ex)
				{
					exception = "System.Exception";
					base.Log(ex);
				}
			}
		}
		else
		{
			if(Request.UrlReferrer != null)
			{
				exception = "StyleNotFound";
			}
		}
	}

	protected Style[] Styles = new Style[0];

	protected int Remained = 0;

	protected string exception = null;
	protected int wait = InitialDelay;

	private static Regex reHighlightCSS = new Regex(@"^([^:]+):\ ([^;]+)(;?)$", RegexOptions.Compiled | RegexOptions.Multiline);
	private static Regex reHighlightSCSS = new Regex(@"^@include\ ([^\(]+)\((.+)\);$", RegexOptions.Compiled | RegexOptions.Multiline);
	private static Regex reHighlightSASS = new Regex(@"^\+([^\(]+)\((.+)\)$", RegexOptions.Compiled | RegexOptions.Multiline);
	protected static string Highlight(string value)
	{
		if(value.StartsWith("@include"))
		{
			return reHighlightSCSS.Replace(value, @"@include <span class=""property"">$1</span>(<span class=""value"">$2</span>);");
		}
		if(value.StartsWith("+"))
		{
			return reHighlightSASS.Replace(value, @"+<span class=""property"">$1</span>(<span class=""value"">$2</span>)");
		}
		return reHighlightCSS.Replace(value, @"<span class=""property"">$1</span>: <span class=""value"">$2</span>$3");
	}
}
