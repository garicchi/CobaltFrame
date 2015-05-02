using CobaltFrame.Mono.Context;
using CobaltFrame.Core.Screen;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CobaltFrame.Mono.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System.Diagnostics;
using Microsoft.Xna.Framework.Graphics;

namespace CobaltFrame.Mono.Screen
{
    public class GameScreenManager:ScreenManager
    {
        protected Vector2 _defaultResolution;

        public Vector2 DefaultResolution
        {
            get { return _defaultResolution; }
            set { _defaultResolution = value; }
        }

        protected Matrix _screenScale;

        public Matrix ScreenScale
        {
            get { return _screenScale; }
            set { _screenScale = value; }
        }

        protected ScaleMode _screenScaleMode;

        public ScaleMode ScreenScaleMode
        {
            get { return _screenScaleMode; }
            set { _screenScaleMode = value; }
        }

        protected Matrix _screenMargin;

        public Matrix ScreenMargin
        {
            get { return _screenMargin; }
            set { _screenMargin = value; }
        }

        private Color _backgroundColor;

        public Color DisplayBackgroundColor
        {
            get { return _backgroundColor; }
            set { _backgroundColor = value; }
        }

        public GameScreenManager(Game game,GameScreen firstScreen,object param,Vector2 defaultResolution,ScaleMode screenScaleMode,IScreenTransition trans = null)
            : base(firstScreen,param,trans)
        {
            this._defaultResolution = defaultResolution;
			GameContext.Game = game;
			GameContext.GraphicsManager = new GraphicsDeviceManager(game);
            
			GameContext.DefaultResolution = defaultResolution;

			ContentContext.Setup (game.Content);

            this._screenScaleMode = screenScaleMode;
			GameContext.Game.IsMouseVisible = true;
            this._backgroundColor = Color.FromNonPremultiplied(10, 10, 10, 255);


			GameContext.Game.Window.ClientSizeChanged+=(s,e)=>
            {
				if(GameContext.Game.GraphicsDevice!=null)
				{
                	ScreenResolutionChanged();
				}
            };


            
        }
        public override void Init()
        {
            base.Init();


            ScreenResolutionChanged();


        }

        public override void Load()
        {
            base.Load();
        }

        

        public override void Update(Core.Context.IFrameContext context)
        {
            (context as FrameContext).ScreenScale = ScreenScale;
            (context as FrameContext).ScreenMargin = ScreenMargin;
            GameInput.Update(context as FrameContext);
            
            base.Update(context);
        }

        public override void Draw(Core.Context.IFrameContext context)
        {
            (context as FrameContext).ScreenScale = ScreenScale;
            (context as FrameContext).ScreenMargin = ScreenMargin;

			GameContext.Game.GraphicsDevice.Clear(this._backgroundColor);
            
            base.Draw(context);
        }
        public override void ScreenResolutionChanged()
        {
            float scaleX = 1.0f;
            float scaleY = 1.0f;
            float aspectRate = (float)this._defaultResolution.X/(float)this._defaultResolution.Y;
            
            Matrix marginMatrix=Matrix.CreateTranslation(0,0,0);
            float xMargin = 0.0f;
            float yMargin = 0.0f;
            switch (this._screenScaleMode)
            {
			case ScaleMode.None:
				
				xMargin = Math.Abs (GameContext.Game.Window.ClientBounds.Width - this.DefaultResolution.X);
				if (xMargin != 0.0f) {
					xMargin /= 2.0f;
				}
				yMargin = Math.Abs (GameContext.Game.Window.ClientBounds.Height - this.DefaultResolution.Y);
				if (yMargin != 0.0f) {
					yMargin /= 2.0f;
				}

				marginMatrix = Matrix.CreateTranslation (xMargin, yMargin, 0);

                    break;
			case ScaleMode.Fill:
				scaleX = (float)GameContext.Game.Window.ClientBounds.Width / this.DefaultResolution.X;
				scaleY=(float)GameContext.Game.Window.ClientBounds.Height / this.DefaultResolution.Y;
                    break;
			case ScaleMode.WidthFit:
				scaleX = (float)GameContext.Game.Window.ClientBounds.Width / this.DefaultResolution.X;
				scaleY = (1.0f / aspectRate) * scaleX;
				var height = this.DefaultResolution.Y * scaleY;

				yMargin = Math.Abs ((float)GameContext.Game.Window.ClientBounds.Height - (float)height);
				if (yMargin != 0.0f) {
					yMargin /= 2.0f;
				}
				if (GameContext.Game.Window.ClientBounds.Height < height) {
					yMargin = -yMargin;
				}
					marginMatrix=Matrix.CreateTranslation(0,yMargin,0);
                    break;
			case ScaleMode.HeightFit:
				
				scaleY = (float)GameContext.Game.Window.ClientBounds.Height / this.DefaultResolution.Y;
				scaleX = aspectRate * scaleY;
				var width = this.DefaultResolution.X*scaleX;
				xMargin = Math.Abs((float)GameContext.Game.Window.ClientBounds.Width - (float)width);
				if (xMargin != 0.0f)
				{
					xMargin /= 2.0f;
				}
				if (GameContext.Game.Window.ClientBounds.Width < width) {
					xMargin = -xMargin;
				}
				marginMatrix=Matrix.CreateTranslation(xMargin,0,0);
                    break;
            }
            
			this._screenScale = Matrix.CreateScale(scaleX,scaleY,1.0f);
			this._screenMargin = marginMatrix;
        }

        

        
    }
}
