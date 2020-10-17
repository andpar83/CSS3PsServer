package com.css3ps
{
	import flash.display.Graphics;
	import flash.display.BitmapData;
	import flash.geom.Matrix;
	import mx.skins.ProgrammaticSkin;

	public class RunButtonSkin extends ProgrammaticSkin
	{
		[Bindable]
		[Embed(source="CSS3PsButton.png")]
		private var ButtonImage:Class;

		[Bindable]
		[Embed(source="CSS3PsDarkButton.png")]
		private var DarkButtonImage:Class;

		[Bindable]
		[Embed(source="CSS3PsButtonWaiting.png")]
		private var ButtonWaitingImage:Class;

		[Bindable]
		[Embed(source="CSS3PsDarkButtonWaiting.png")]
		private var DarkButtonWaitingImage:Class;
		
		public function RunButtonSkin()
		{
			super();
		}

		private function darker(color:uint, value:uint) : uint {
			var r:uint = (color >> 16) & 0xff;
			var g:uint = (color >> 8) & 0xff;
			var b:uint = color & 0xff; 
			r = r > value ? (r - value) : 0;
			g = g > value ? (g - value) : 0;
			b = b > value ? (b - value) : 0;
			return (r << 16) + (g << 8) + b;
		}

		private static function drawImage(g:Graphics, image:BitmapData, x:int, y:int):void {
			var mtx:Matrix = new Matrix();
			mtx.translate(x, y);
			g.beginBitmapFill(image, mtx, false, false);
			g.drawRect(x, y, image.width, image.height);
			g.endFill();
		}

		override protected function updateDisplayList(w:Number, h:Number):void {
			try {
				var fillColors:Array = getStyle("fillColors");
				var backgroundFillColor:Number = fillColors[0];
				var light:Boolean = (backgroundFillColor % 256) > 128;
				var lineThickness:Number = 1;
				var drawBorder:Boolean = false;
				var bitmap:BitmapData = light ? new ButtonImage().bitmapData : new DarkButtonImage().bitmapData;

				switch (name) {
					case "overSkin":
						drawBorder = true;
						backgroundFillColor = darker(backgroundFillColor, 5);
						break;
					case "downSkin":
						drawBorder = true;
						backgroundFillColor = darker(backgroundFillColor, 15);
						break;
					case "selectedDisabledSkin":
						bitmap = light ? new ButtonWaitingImage().bitmapData : new DarkButtonWaitingImage().bitmapData;
						break;
				}

				var g:Graphics = graphics;
				g.clear();
				g.beginFill(backgroundFillColor, 1.0);
				g.drawRect(0, 0, w, h);
				g.endFill();

				drawImage(g, bitmap, (w - bitmap.width) / 2, (h - bitmap.height) / 2);

				if (drawBorder) {
					// Border has to be above image
					g.lineStyle(lineThickness, 0x00b3ff);
					g.beginFill(0xffffff, 0.0);
					g.drawRect(0, 0, w - lineThickness, h - lineThickness);
					g.endFill();
				}
			} catch (e:Error) {
			}
		}
	}
}