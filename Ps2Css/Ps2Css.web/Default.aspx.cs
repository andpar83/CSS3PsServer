using System;
using System.Configuration;
using System.Text.RegularExpressions;

public partial class _Default : Meta.Web.UI.Page
{
	private static readonly string ToolUrl = ConfigurationManager.AppSettings["ToolUrl"];

	protected void Page_Load(object sender, EventArgs e)
	{
		string guid = Request.QueryString[string.Empty];
		if(guid != null && Regex.IsMatch(guid, "[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}"))
		{
			Response.StatusCode = 301;
			Response.Status = "301 Moved Permanently";
			Response.AddHeader("Location", ToolUrl + "?=" + guid);
			Response.End();
		}
	}
}
