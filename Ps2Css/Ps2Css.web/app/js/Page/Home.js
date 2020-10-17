$(function()
{
	$("#video a").click(function()
	{
		var player = document.createElement("div")
		player.id = "player"
		var video = document.getElementById("video")
		video.appendChild(player)
		video.className = "video"
		swfobject.embedSWF("http://www.youtube.com/v/0ZMs9S9TdNE?autoplay=1&controls=0&rel=0&showinfo=0&iv_load_policy=3&theme=light", "player", "601", "338", "8")
		return false
	})
//	$("#ask-for-feature").click(function()
//	{
//		var div = $(this.parentNode).next()[0]
//		div.className = div.className == "toggle closed" ? "toggle" : "toggle closed"
//		return false
//	})
	$("a[href=\"#video\"]").click(function()
	{
		var div = $(this.parentNode).next()[0]
		div.className = div.className == "toggle closed" ? "toggle" : "toggle closed"
		return false
	})
	$('textarea[name="message"]').watermark("message")
})
