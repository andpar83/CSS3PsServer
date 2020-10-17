<%@ Page Language="C#" CodeFile="Default.aspx.cs" Inherits="_Default"
%><!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" lang="en" xml:lang="en">
	<head>
		<meta http-equiv="Content-Type" content="text/html;charset=utf-8" />
		<title></title>
		<meta name="keywords" content="" />
		<meta name="description" content="" />
		<link type="text/css" rel="stylesheet" href="<%= Url("~/app/styles.css") %>" />
		<link type="text/css" rel="stylesheet" href="<%= ResolveUrl("~/app/styles.css") %>" />
<% if(this.Styles.Length > 0){ %>
<style type="text/css"><% for(int i = 0; i < this.Styles.Length; i++){ %>
	.Style<%= i %>
	{
		<%= string.Join(Environment.NewLine + "		", this.Styles[i].ToArray(Ps2Css.Css.Type.css)) %>
	}
	<% for(int j = 0; j < this.Styles[i].Text.Count; j++){ %>
	.Style<%= i %> .span<%= j %>
	{
		<%= string.Join(Environment.NewLine + "		", this.Styles[i].Text[j].ToArray(Ps2Css.Css.Type.css)) %>
	}
	<% } %>
<% } %></style>
<% } %>	</head>
	<body>
		<a href="<%= Url("~/") %>"><img src="<%= Url("~/app/img/Master/logo.png") %>" alt="CSS3Ps - CSS3 PHOTOSHOP PLUGIN" /></a>
		<a href="mailto:support@css3ps.com" title="support@css3ps.com 
Do not hesitate to contact us for any reason">support@css3ps.com</a>
		<div id="content">
			<div>
<% if(this.exception != null){ %>
				<p class="error"><%= Resource("Exception:" + this.exception) %></p>
<% }else{ %>
	<% if(this.wait > 0){ %>
				<img src="<%= ResolveUrl("~/app/img/wait.gif") %>" alt="wait" />
				<p id="wait">Please wait <span id="timer"><%= this.wait %></span> seconds while your styles are being processed.</p>
				<div id="ads"><!-- #Include virtual="~/app/Ads.inc" --></div>
	<% }else{ %>
		<% if(this.Styles.Length == 0){ %>
				<p class="error">Sorry, we could not find any style in your layer.</p>
		<% }else{ %>
				<table id="css" class="active">
			<% for(int i = 0; i < this.Styles.Length; i++){ %>
					<tr>
						<td>
							<div class="Style<%= i %>"><% for(int j = 0; j < this.Styles[i].Text.Count; j++){ %><span class="span<%= j %>"><%= HttpUtility.HtmlEncode(this.Styles[i].Text[j].Text).Replace("\r\n", "<br />").Replace("\r", "<br />").Replace("\n", "<br />") %></span><% } %></div>
						</td>
						<td>
							<code class="css3ps">
<span class="name"><%= HttpUtility.HtmlEncode(this.Styles[i].Name) %></span>
{
	<%= string.Join(Environment.NewLine + "	", Array.ConvertAll<string, string>(this.Styles[i].ToArray(Ps2Css.Css.Type.css), Highlight)) %>
}
<% foreach(var span in this.Styles[i].Text.FindAll(x => x.ToArray(Ps2Css.Css.Type.css).Length > 0)){ %>
<span class="name"><%= HttpUtility.HtmlEncode(span.Text.Trim('\r', '\n')) %></span>
{
	<%= string.Join(Environment.NewLine + "	", Array.ConvertAll<string, string>(span.ToArray(Ps2Css.Css.Type.css), Highlight))%>
}
<% } %>
							</code>
						</td>
					</tr>
			<% } %>
				</table>
				<table id="scss">
					<tr>
						<td></td>
						<td>
							<code class="css3ps">@import "compass/css3";</code>
						</td>
					</tr>
			<% for(int i = 0; i < this.Styles.Length; i++){ %>
					<tr>
						<td>
							<div class="Style<%= i %>"><% for(int j = 0; j < this.Styles[i].Text.Count; j++){ %><span class="span<%= j %>"><%= HttpUtility.HtmlEncode(this.Styles[i].Text[j].Text).Replace("\r\n", "<br />").Replace("\r", "<br />").Replace("\n", "<br />") %></span><% } %></div>
						</td>
						<td>
							<code class="css3ps">
<span class="name"><%= HttpUtility.HtmlEncode(this.Styles[i].Name) %></span>
{
	<%= string.Join(Environment.NewLine + "	", Array.ConvertAll<string, string>(this.Styles[i].ToArray(Ps2Css.Css.Type.scss), Highlight)) %>
}
<% foreach(var span in this.Styles[i].Text.FindAll(x => x.ToArray(Ps2Css.Css.Type.css).Length > 0)){ %>
<span class="name"><%= HttpUtility.HtmlEncode(span.Text.Trim('\r', '\n')) %></span>
{
	<%= string.Join(Environment.NewLine + "	", Array.ConvertAll<string, string>(span.ToArray(Ps2Css.Css.Type.scss), Highlight))%>
}
<% } %>
							</code>
						</td>
					</tr>
			<% } %>
				</table>
				<table id="sass">
					<tr>
						<td></td>
						<td>
							<code class="css3ps">@import "compass/css3"</code>
						</td>
					</tr>
			<% for(int i = 0; i < this.Styles.Length; i++){ %>
					<tr>
						<td>
							<div class="Style<%= i %>"><% for(int j = 0; j < this.Styles[i].Text.Count; j++){ %><span class="span<%= j %>"><%= HttpUtility.HtmlEncode(this.Styles[i].Text[j].Text).Replace("\r\n", "<br />").Replace("\r", "<br />").Replace("\n", "<br />") %></span><% } %></div>
						</td>
						<td>
							<code class="css3ps">
<span class="name"><%= HttpUtility.HtmlEncode(this.Styles[i].Name) %></span>
	<%= string.Join(Environment.NewLine + "	", Array.ConvertAll<string, string>(this.Styles[i].ToArray(Ps2Css.Css.Type.sass), Highlight)) %>
<% foreach(var span in this.Styles[i].Text.FindAll(x => x.ToArray(Ps2Css.Css.Type.css).Length > 0)){ %>
<span class="name"><%= HttpUtility.HtmlEncode(span.Text.Trim('\r', '\n')) %></span>
	<%= string.Join(Environment.NewLine + "	", Array.ConvertAll<string, string>(span.ToArray(Ps2Css.Css.Type.sass), Highlight))%>
<% } %>
							</code>
						</td>
					</tr>
			<% } %>
				</table>
				<div id="ads"><!-- #Include virtual="~/app/Ads2.inc" --></div>
		<% } %>
	<% } %>
<% } %>
			</div>
		</div>
		<div>
			<a href="#css" class="active">CSS</a>
			<a href="#scss">SCSS</a>
			<a href="#sass">SASS</a>
<%--			<a href="#prefixes">Prefixes</a>
--%>			<div id="social-networks">
					<a href="https://twitter.com/share" class="twitter-share-button" data-url="http://css3ps.com" data-text="<%= Request.Url.AbsoluteUri.Replace("default.aspx", string.Empty) %> by " data-count="none">Tweet</a>
					<script type="text/javascript">!function(d,s,id){var js,fjs=d.getElementsByTagName(s)[0];if(!d.getElementById(id)){js=d.createElement(s);js.id=id;js.src="//platform.twitter.com/widgets.js";fjs.parentNode.insertBefore(js,fjs);}}(document,"script","twitter-wjs");</script>
					<div class="fb-send" data-href="<%= Request.Url.AbsoluteUri.Replace("default.aspx", string.Empty) %>"></div>
					<div id="fb-root"></div>
					<script type="text/javascript">!function(d,s,id){var js,fjs=d.getElementsByTagName(s)[0];if(!d.getElementById(id)){js=d.createElement(s);js.id=id;js.src="//connect.facebook.net/en_US/all.js#xfbml=1";fjs.parentNode.insertBefore(js,fjs);}}(document,"script","facebook-jssdk");</script>
			</div>
		</div>
		<script type="text/javascript" src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.7.2.min.js"></script>
		<script type="text/javascript" src="<%= ResolveUrl("~/app/scripts.js") %>"></script>
<!-- #Include virtual="~/app/Yandex.Metrika.inc"
-->	</body>
</html>
