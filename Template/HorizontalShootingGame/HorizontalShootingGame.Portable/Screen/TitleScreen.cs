using CobaltFrame.Mono.Context;
using CobaltFrame.Mono.Input;
using CobaltFrame.Mono.Object;
using CobaltFrame.Mono.Position;
using CobaltFrame.Mono.Screen;
using CobaltFrame.Mono.Transition;
using HorizontalShootingGame.Portable.Object;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CobaltFrame.Mono.UI;

namespace HorizontalShootingGame.Portable.Screen
{
    public class TitleScreen:GameScreen
    {
		BitmapTextObject _startButton;
        public TitleScreen()
            : base()
        {
			
        }

		public override void Init ()
		{
			base.Init ();
			this._startButton = new BitmapTextObject (new Box2((int)Box.GetCenter().X,(int)Box.GetCenter().Y,500,100),"Font/meiryo","start",4,Color.White);
			this.AddDrawableObject(this._startButton);


			this.Inputs.RegisterInput("start",
				()=>GameInput.TouchCollection.IsTouch&&this._startButton.Box.Contains(GameInput.TouchCollection.First().Position),
				null,
				null,
				null,
				null
			);
		}
        public override void Load()
        {
            base.Load();

            
        }
        public override void Update(CobaltFrame.Core.Context.IFrameContext context)
        {
            base.Update(context);
			if (this.Inputs.IsInput ("start")) 
			{
				this.Navigate (new Stage1Screen(),null
					,new FadeColorTransition(Color.Black,0,255,TimeSpan.FromSeconds(1))
					,new FadeColorTransition(Color.Black,255,0,TimeSpan.FromSeconds(1))
				);
			}
        }

        public override void Draw(CobaltFrame.Core.Context.IFrameContext context)
        {
            base.Draw(context);
        }
    }
}
