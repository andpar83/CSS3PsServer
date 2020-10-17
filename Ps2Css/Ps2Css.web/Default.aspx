<%@ Page Title="CSS3Ps - free cloud based photoshop plugin that converts layers to CSS3 styles." Language="C#" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ContentPlaceHolderID="cphKeywords" runat="server">free, download, CSS, CSS3, Adobe, Photoshop, CS6, CS5, CS4, CS3, plugin, extension, convert psd to css</asp:Content>
<asp:Content ContentPlaceHolderID="cphDescription" runat="server">CSS3Ps is a free, cloud based, photoshop plugin for converting layers to CSS3 styles.</asp:Content>
<asp:Content ContentPlaceHolderID="cphContent" runat="server">
	<a href="<%= Url("~/Download/") %>">Free Download</a>
	<div id="video">
		<a href="#play">play</a>
	</div>
	<div id="content">
		<h1><strong>CSS3Ps</strong> - free cloud based photoshop plugin that converts your layers to CSS3.</h1>
		<div>
			<img src="app/img/Pages/Home/photoshop.jpg" alt="Just select layers in Photoshop and click to CSS3Ps button." /><img src="app/img/Pages/Home/browser.png" alt="Get your styles and preview in a browser window!" />
		</div>
		<p>Still can't believe? Take a look at real <strong>examples</strong>:</p>
		<div>
			<a href="<%= Url("~/app/img/Pages/Home/CSS3Ps Example.psd") %>">Example PSD</a>
			<a href="#video">How it works video</a>
			<a href="http://css3.ps/?=306c793d-e2d2-51e2-3493-92574e0128f9" target="_blank">CSS3 result</a>
		</div>
		<div class="toggle closed">
			<iframe width="853" height="480" src="http://www.youtube.com/embed/bwVL6zMauxc?rel=0" frameborder="0" allowfullscreen></iframe>
		</div>
		<p><strong>Features</strong> that we support for today:</p>
		<ul>
			<li class="text">text layers<span class="new">new</span></li>
			<li class="cloud">cloud service</li>
			<li class="layers">multiple layers selection</li>
			<li class="scss">SCSS for Compass</li>
			<li class="vendors">vendor prefixes</li>
			<li class="sass">SASS for Compass</li>
			<li class="size">size</li>
			<li class="border-radius">border radius</li>
			<li class="stroke">stroke</li>
			<li class="gradient">gradient overlay</li>
			<li class="inner-shadow">inner shadow</li>
			<li class="inner-glow">inner glow</li>
			<li class="outer-glow">outer glow</li>
			<li class="drop-shadow">drop shadow</li>
			<li id="ask-for-feature"><a href="mailto:support@css3ps.com">ask for feature</a></li>
		</ul>
		<div id="social-networks">
			<div class="g-plusone" data-size="medium" data-href="http://css3ps.com/"></div>
			<script type="text/javascript">(function() { var po = document.createElement('script'); po.type = 'text/javascript'; po.async = true; po.src = 'https://apis.google.com/js/plusone.js'; var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(po, s); })();</script>
			<a href="https://twitter.com/share" class="twitter-share-button">Tweet</a>
			<script type="text/javascript">!function(d,s,id){var js,fjs=d.getElementsByTagName(s)[0];if(!d.getElementById(id)){js=d.createElement(s);js.id=id;js.src="//platform.twitter.com/widgets.js";fjs.parentNode.insertBefore(js,fjs);}}(document,"script","twitter-wjs");</script>
			<div class="fb-like" data-send="false" data-layout="button_count" data-width="450" data-show-faces="false"></div>
			<div id="fb-root"></div>
			<script type="text/javascript">(function(d, s, id) {var js, fjs = d.getElementsByTagName(s)[0];if (d.getElementById(id)) return;js = d.createElement(s); js.id = id;js.src = "//connect.facebook.net/en_US/all.js#xfbml=1";fjs.parentNode.insertBefore(js, fjs);}(document, 'script', 'facebook-jssdk'));</script>
		</div>
<%--		<div class="toggle closed">
			<form action="<%= Url("~/Send/Default.ashx") %>" method="post">
				<input type="text" name="email" value="" />
				<textarea name="message"></textarea>
				<button>Send Message</button>
			</form>
		</div>
--%>	</div>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphScript" runat="server">
		<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/swfobject/2.2/swfobject.js"></script>
		<script type="text/javascript" src="<%= Url("~/app/js/Page/Home.js") %>"></script>
</asp:Content>
