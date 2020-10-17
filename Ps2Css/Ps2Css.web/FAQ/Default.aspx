<%@ Page Title="CSS3Ps - FAQ" Language="C#" %>

<asp:Content ContentPlaceHolderID="cphKeywords" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="cphDescription" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="cphContent" runat="server">
	<div id="content">
		<div>
			<h1>FAQ:</h1>
			<h2>Requirements</h2>
			<p>Windows or Mac OS X and Photoshop CS3 and later</p>
			<h2>Price</h2>
			<p>It's absolutely free!</p>
			<h2>Why it is in cloud?</h2>
			<p>All calculations are made in our cloud, so most of updates and bug-fixes are transparent to you, there is no need to update the plugin to use new features. You can see the result styles in browser and share them with other people.</p>
			<h2>Supported Features</h2>
			<ul>
				<li><strong>Evidence</strong> - you can easy see the converting result online in your browser</li>
				<li><strong>Multiple Layers</strong> - You can select several layers and groups of layers and convert them in one click</li>
				<li><strong>Prefixes</strong> - we support prefixes for all popular browsers: "-webkit-" for Safari and Chrome, "-moz-" for Firefox, "-ms-" for IE 10 and "-o-" for Opera.</li>
				<li><strong>Blending</strong> - for now only normal modes supported but we will add other blending modes soon</li>
				<li><strong>Size</strong> - we convert size of layer (shape or bitmap) to CSS <em>width</em> and <em>height</em> properties</li>
				<li><strong>Border Radius</strong> - we calculate border radius for shape if it is possible to represent the shape as a rectangle with rounded corners; you can convert really <a href="http://css3.ps/?=14199CC9-C969-E749-7109-96BFEFE90F67">a lot of shape types</a></li>
				<li><strong>Bevel and Emboss</strong> - we do not support it <em>yet</em></li>
				<li><strong>Stroke</strong> - is converted into CSS <em>border</em> property</li>
				<li><strong>Inner Shadow</strong> - is converted into CSS <em>box-shadow</em> property</li>
				<li><strong>Inner Glow</strong> - is converted into CSS <em>box-shadow</em> property</li>
				<li><strong>Satin</strong> - we do not support it</li>
				<li><strong>Color Overlay</strong> - we do not support it for now but we will add it soon</li>
				<li><strong>Gradients Overlay</strong> - for now we support only linear gradients but we will add radial and reflected gradients soon</li>
				<li><strong>Pattern</strong> - we do not support it for now but we are working on it</li>
				<li><strong>Outer Glow</strong> - is converted into CSS <em>box-shadow</em> property</li>
				<li><strong>Drop Shadow</strong> - is converted into CSS <em>box-shadow</em> property</li>
				<li><strong>Text Layers</strong> - we support CSS <em>font-family</em>, <em>font-size</em>, <em>font-weight</em>, <em>font-style</em>, <em>text-transform</em>, <em>text-decoration</em>, <em>color</em> and <em>text-shadow</em> properties for the text layers</li>
			</ul>
			<h2>Waiting time</h2>
			<p>We also want to make it smaller but our resources are limited while the number of users of our service is growing up.<br />We decreased waiting time to 20 seconds and have removed the 10 seconds delay between layers convertion.</p>
		</div>
	</div>
</asp:Content>
