$(function()
{
	$('input[name="email"]').watermark("your email")
	$("form").submit(function()
	{
		var form = this
		var data = {  }
		$.ajax(
		{
			type: "POST",
			url: this.action,
			data: data,
			success: this.success || function(data, textStatus)
			{
				$(form).replaceWith(data)
			},
			error: this.error || function(data, textStatus)
			{
				$(form).replaceWith("ERROR")
			}
		});
		return false
	})
})
