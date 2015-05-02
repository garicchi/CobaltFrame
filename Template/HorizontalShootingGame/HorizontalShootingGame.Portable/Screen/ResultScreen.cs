using System;
using CobaltFrame.Mono.Screen;
using CobaltFrame.Mono.UI;
using Microsoft.Xna.Framework;
using CobaltFrame.Mono.Position;
using HorizontalShootingGame.Portable.Screen;
using CobaltFrame.Mono.Transition;
using CobaltFrame.Core.Data;
using HorizontalShootingGame.Portable.Data;
using CobaltFrame.Mono;

namespace HorizontalShootingGame.Portable
{
	public class ResultScreen:GameScreen
	{
		private BitmapTextObject _scoreText;
		private BitmapTextObject _yourResultText;
		ButtonObject _titleButton;
		public ResultScreen ()
			:base()
		{
		}

		public override void Init ()
		{
			base.Init ();
			this._yourResultText = new BitmapTextObject (new Box2((int)Box.GetCenter().X-250,(int)Box.GetCenter().Y-200,600,200),"Font/meiryo","your score is",3,Color.White);
			this.AddDrawableObject (this._yourResultText);

			this._scoreText = new BitmapTextObject (new Box2((int)Box.GetCenter().X-100,(int)Box.GetCenter().Y+20,200,200),"Font/meiryo","0",6,Color.White);
			this.AddDrawableObject (this._scoreText);

			this._titleButton = new ButtonObject (new Box2(10,10,150,80),"Texture/titlebutton_on","Texture/titlebutton_off");
			this._titleButton.OnClick += (sender,pos) => 
			{
				this.Navigate(new TitleScreen(),null
					,new FadeColorTransition(Color.Black,0,255,TimeSpan.FromSeconds(1))
					,new FadeColorTransition(Color.Black,255,0,TimeSpan.FromSeconds(1)));
			};
			this.AddDrawableObject (this._titleButton);
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

			if (SaveDataStore<SaveData>.Data.PreviousScore < score) 
			{
				SaveDataStore<SaveData>.Data.PreviousScore = score;
				NotificationContext.Notify ("ClearAlert","ハイスコアです "+score);
			}
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

