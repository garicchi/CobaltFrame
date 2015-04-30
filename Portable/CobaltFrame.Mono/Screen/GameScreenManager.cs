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
        protected Game _game;
        public GameScreenManager(GameScreen firstScreen,object param,Vector2 defaultResolution,ScaleMode screenScaleMode,IScreenTransition trans = null)
            : base(firstScreen,param,trans)
        {
            this._defaultResolution = defaultResolution;
            this._game = GameContext.Game;
            this._screenScaleMode = screenScaleMode;
            this._game.IsMouseVisible = true;
            this._backgroundColor = Color.FromNonPremultiplied(10, 10, 10, 255);

			GameContext.GraphicsManager.PreferredBackBufferWidth = (int)this.DefaultResolution.X;
			GameContext.GraphicsManager.PreferredBackBufferHeight = (int)this.DefaultResolution.Y;


            this._game.Window.ClientSizeChanged+=(s,e)=>
            {
				if(this._game.GraphicsDevice!=null)
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

            this._game.GraphicsDevice.Clear(this._backgroundColor);
            
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
				scaleX = (float)this._defaultResolution.X/((float)this._game.Window.ClientBounds.Width);
				scaleY = (float)this._defaultResolution.Y/((float)this._game.Window.ClientBounds.Height);


				xMargin = Math.Abs (this._game.Window.ClientBounds.Width - this.DefaultResolution.X);
				if (xMargin != 0.0f) {
					xMargin /= 2.0f;
				}
				yMargin = Math.Abs (this._game.Window.ClientBounds.Height - this.DefaultResolution.Y);
				if (yMargin != 0.0f) {
					yMargin /= 2.0f;
				}

				marginMatrix = Matrix.CreateTranslation (xMargin, yMargin, 0);

                    break;
                case ScaleMode.Fill:
                    
                    break;
			case ScaleMode.WidthFit:
                    
				var height = (1 / aspectRate) * this._game.Window.ClientBounds.Width;
				scaleY = height / (float)this._game.Window.ClientBounds.Height;
				yMargin = Math.Abs ((float)this._game.Window.ClientBounds.Height - (float)height);
				if (yMargin != 0.0f) {
					yMargin /= 2.0f;
				}
				if (this._game.Window.ClientBounds.Height < height) {
					yMargin = -yMargin;
				}
					marginMatrix=Matrix.CreateTranslation(0,yMargin,0);
                    break;
                case ScaleMode.HeightFit:
				var width = aspectRate * this._game.Window.ClientBounds.Height;
				scaleX = width / (float)this._game.Window.ClientBounds.Width;
				xMargin = Math.Abs((float)this._game.Window.ClientBounds.Width - (float)width);
				if (xMargin != 0.0f)
				{
					xMargin /= 2.0f;
				}
				if (this._game.Window.ClientBounds.Width < width) {
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
