<%@ Page Title="CSS3Ps - Download Plugin" Language="C#" %>

<asp:Content ContentPlaceHolderID="cphKeywords" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="cphDescription" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="cphContent" runat="server">
	<div id="content">
		<div>
			<h1>Downloads:</h1>
			<p>
				<a href="<%= Url("~/CSS3Ps.zxp") %>">Download</a> for Adobe Photoshop CS5 and CS6<br />
				<a href="<%= Url("~/CSS3Ps.jsx") %>">Download</a> for Adobe Photoshop CS3 and CS4<br />
			</p>
			<p class="note">Mac OS X Lion requires <a href="http://blogs.adobe.com/cssdk/2011/12/fix-for-extension-signature-bug-on-mac-os-10-7-patch-posted.html" target="_blank">patch from Adobe</a> before installation</p>
			<h2 id="CS5">Installation process for Photoshop CS5 and CS6</h2>
			<ol>
				<li>If Adobe Photoshop is running then close it.</li>
				<li>Open downloaded "CSS3Ps.zpx" file.</li>
				<li>In Adobe Extension Manager click on "Accept" button.<br />
					<img src="<%= Url("~/app/img/Pages/Download/Step3.png") %>" alt="Adobe Extension Manager - Accept License" />
				</li>
				<li>When the installation process is done you'll see the screen below. Now you can run Adobe Photoshop.<br />
					<img src="<%= Url("~/app/img/Pages/Download/Step4.png") %>" alt="Adobe Extension Manager - Plugin Installed" />
				</li>
				<li>In Adobe Photoshop check "Window -> Extensions -> CSS3Ps" menu item.<br />
					<img src="<%= Url("~/app/img/Pages/Download/Menu-item-MacOS.png") %>" alt="Extension menu item in Mac OS" />
					<img src="<%= Url("~/app/img/Pages/Download/Menu-item-Windows.png") %>" alt="Extension menu item in Windows" />
				</li>
				<li>Congratulations! Now you can select your layers and conver them to CSS.</li>
			</ol>
			<h2 id="CS3">Using script in Photoshop CS3 and CS4</h2>
			<ol>
				<li>Select layers that you want to convert<br />
					<img src="<%= Url("~/app/img/Pages/Download/xp-screen-1.png") %>" alt="" />
				</li>
				<li>Select "File -> Scripts -> Browse..." menu item<br />
					<img src="<%= Url("~/app/img/Pages/Download/xp-screen-2.png") %>" alt="" />
				</li>
				<li>Select "CSS3Ps.jsx" file in the folder where you have downloaded it<br />
					<img src="<%= Url("~/app/img/Pages/Download/xp-screen-3.png") %>" alt="" />
				</li>
				<li>The script will run and open a browser window with the conversion result<br />
					<img src="<%= Url("~/app/img/Pages/Download/xp-screen-4.png") %>" alt="" />
				</li>
			</ol>
			<h2 id="alt">Can't install the plugin?</h2>
			<p>You can <a href="mailto:support@css3ps.com">contact us</a> and we will try to help you.</p>
		</div>
	</div>
</asp:Content>
