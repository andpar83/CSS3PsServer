<%@ WebHandler Language="C#" Class="Ps2Css.Handler" %>

using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Ps2Css
{
	public class Handler : IHttpHandler
	{
		private static string LOG = Path.Combine(System.Configuration.ConfigurationManager.AppSettings["logs"], "Handler.ashx.txt");

		private static object @lock = new object();

		public void ProcessRequest(HttpContext context)
		{
			try
			{
				string ip = context.Request.UserHostAddress;
				Guid guid = new Guid(context.Request["id"]);
				string data = context.Request.Form["data"];
				if(data != null)
				{
					Save(guid, ip, Convert.FromBase64String(data));
				}
				else
				{
					string xml = context.Request.Form["xml"];
					Save(guid, ip, Zlib.Compress(Encoding.UTF8.GetBytes(xml)));
				}
				context.Response.Write(guid.ToString());
			}
			catch(HttpException ex)
			{
				string path = Path.Combine(context.Server.MapPath("~/"), LOG);
				string contents = string.Format("{1:dd.MM.yyyy HH.mm.ss UTCzz}{0}{2}{0}{0}", Environment.NewLine, DateTime.Now, ex.ToString());
				lock(@lock)
				{
					File.AppendAllText(path, contents);
				}

				context.Response.Write(ex.Message);
				context.Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
			}
			catch(Exception ex)
			{
				string path = Path.Combine(context.Server.MapPath("~/"), LOG);
				string contents = string.Format("{1:dd.MM.yyyy HH.mm.ss UTCzz}{0}{2}{0}{0}", Environment.NewLine, DateTime.Now, ex.ToString());
				lock(@lock)
				{
					File.AppendAllText(path, contents);
				}

				context.Response.Write(ex.Message);
				context.Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
			}
		}

		private void Save(Guid guid, string ip, byte[] data)
		{
			Meta.SQL.IDatabase db = new Meta.SQL.Module().Database;
			DB.Request.Module module = new DB.Request.Module(db);
			DB.Request.Item item = new DB.Request.Item(guid);
			item.IP = ConvertIP(ip);
			item.Data = data;
			module.Commit(item);
		}

		private static byte[] ConvertIP(string value)
		{
			Match match = Regex.Match(value, @"(\d{1,3})\.(\d{1,3})\.(\d{1,3})\.(\d{1,3})");
			return match.Success ? new byte[] { byte.Parse(match.Groups[1].Value), byte.Parse(match.Groups[2].Value), byte.Parse(match.Groups[3].Value), byte.Parse(match.Groups[4].Value)} : new byte[0];
		}

		public bool IsReusable
		{
			get
			{
				return true;
			}
		}
	}
}
