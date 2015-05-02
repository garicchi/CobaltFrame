using System;
using CobaltFrame.Mono.Screen;
using CobaltFrame.Mono.UI;
using CobaltFrame.Mono.Position;
using System.Threading.Tasks;
using CobaltFrame.Mono;
using CobaltFrame.Mono.Font;
using HorizontalShootingGame.Portable.Screen;
using CobaltFrame.Mono.Transition;
using CobaltFrame.Core.Animation;
using Microsoft.Xna.Framework;
using CobaltFrame.Mono.Context;

namespace HorizontalShootingGame.Portable.Screen
{
	public class LoadScreen:GameScreen
	{
		Texture2DObject _loadingTexture;
		InstantTimeAnimation<int> _loadingAnimation;

		Task _loadingTask;
		public LoadScreen ()
		{
		}

		public override void Init ()
		{
			this._loadingTexture = new Texture2DObject (new Box2(10,10,350,100),"Texture/loading");
			this.AddDrawableObject (this._loadingTexture);
			this._loadingAnimation = new InstantTimeAnimation<int> (255,0,TimeSpan.FromSeconds(2),(start,end,progress)=>
			{
				return (int)((float)(end-start)*progress)+start;
			});
			this._loadingAnimation.IsLoop = true;
			this._loadingAnimation.Start ();
			this.AddObject (this._loadingAnimation);


			_loadingTask =Task.Run (()=>
			{
				ContentContext.LoadWithoutManager<FontFile> ("Font/meiryo",()=>FontLoader.Load("Font/meiryo"));
			});
			
			base.Init ();
		}

		public override void Load ()
		{
			base.Load ();


		}

		public override void Update (CobaltFrame.Core.Context.IFrameContext context)
		{
			base.Update (context);
			if (_loadingTask.IsCompleted) {
				this.Navigate (new TitleScreen(),null
					,new FadeColorTransition(Color.Black,0,255,TimeSpan.FromSeconds(1))
					,new FadeColorTransition(Color.Black,255,0,TimeSpan.FromSeconds(1))
				);
			} else {
				this._loadingTexture.DrawColor = Color.FromNonPremultiplied (255,255,255,this._loadingAnimation.CurrentValue);
			}
		}

		public override void Draw (CobaltFrame.Core.Context.IFrameContext context)
		{
			base.Draw (context);
		}
	}
}

