using System;
using System.Configuration;
using System.IO;

public class Page : System.Web.UI.Page
{
	public string Resource(string resourceKey)
	{
		return ((string)GetLocalResourceObject(resourceKey)).Replace("\n", "<br />");
	}

	private static object @lock = new object();
	public void Log(string message)
	{
		string path = Path.Combine(MapPath("~/"), Path.Combine(ConfigurationManager.AppSettings["logs"], base.ToString().Substring(4).Replace("_aspx", ".aspx") + ".txt"));
		string contents = string.Format("{1:dd.MM.yyyy HH.mm.ss UTCzz} - {2}{0}{3}{0}{0}", Environment.NewLine, DateTime.Now, base.Request.Url, message);
		lock(@lock)
		{
			File.AppendAllText(path, contents);
		}
	}
	public void Log(Exception ex)
	{
		this.Log(ex.ToString());
	}

	protected static readonly Uri SiteUri = new Uri(ConfigurationManager.AppSettings["SiteUrl"]);
	public string Url(string relativeUrl)
	{
		return new Uri(SiteUri, base.ResolveClientUrl(relativeUrl)).ToString();
	}
}
