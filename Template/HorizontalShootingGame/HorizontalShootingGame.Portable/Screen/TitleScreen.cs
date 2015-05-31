using HorizontalShootingGame.Portable.Object;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HorizontalShootingGame.Portable.Data;
using CobaltFrame.Screen;
using CobaltFrame.Common;
using CobaltFrame.Input;
using CobaltFrame.Context;
using CobaltFrame.Transition;
using CobaltFrame.UI;

namespace HorizontalShootingGame.Portable.Screen
{
    public class TitleScreen:GameScreen
    {
		BitmapTextObject _startButton;
		BitmapTextObject _scoreText;
        public TitleScreen()
            : base()
        {
			
        }

		public override void Init ()
		{
			base.Init ();
            
            this._startButton = new BitmapTextObject("System/Font/ipagothic", "start", 4, Color.White);
            this._startButton.SetRect(new Rectangle((int)this.GetRect().GetCenter().X, (int)this.GetRect().GetCenter().Y, 500, 100));
			this.AddChild(this._startButton);

            this._scoreText = new BitmapTextObject("System/Font/ipagothic", "high score ", 4, Color.White);
            this._scoreText.SetRect(new Rectangle(10,10,500,100));
			this.AddChild(this._scoreText);


			this.Inputs.RegisterInput("start",
				(current,prev)=>current.IsTouch&&this._startButton.GetRect().Contains(current.First().Position),
				null,
				null,
				null,
				null
			);
		}
        public override void Load()
        {
            base.Load();
			this._scoreText.Text ="high score "+ DataContext<SaveData>.Data.PreviousScore.ToString();
            
        }
        public override void Update(FrameContext context)
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

        public override void Draw(FrameContext context)
        {
            base.Draw(context);
        }
    }
}
