using System;
using CobaltFrame.Mono.Screen;
using CobaltFrame.Mono.UI;
using Microsoft.Xna.Framework;
using CobaltFrame.Mono.Position;

namespace HorizontalShootingGame.Portable
{
	public class ResultScreen:GameScreen
	{
		private BitmapTextObject _scoreText;
		public ResultScreen ()
			:base()
		{
		}

		public override void Init ()
		{
			base.Init ();
			this._scoreText = new BitmapTextObject (new Box2((int)Box.GetCenter().X,(int)Box.GetCenter().Y,200,200),"Font/meiryo","0",6,Color.White);
			this.AddDrawableObject (this._scoreText);
		}

		public override void Load ()
		{
			base.Load ();

		}

		public override void NavigateTo (object parameter, CobaltFrame.Core.Screen.IScreenTransition transition)
		{
			base.NavigateTo (parameter, transition);
			var score = (int)parameter;
			this._scoreText.Text = score.ToString ();
		}

		public override void Unload ()
		{
			base.Unload ();
		}

		public override void Update (CobaltFrame.Core.Context.IFrameContext context)
		{
			base.Update (context);
		}

		public override void Draw (CobaltFrame.Core.Context.IFrameContext context)
		{
			base.Draw (context);
		}
	}
}

