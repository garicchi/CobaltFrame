using CobaltFrame.Context;
using CobaltFrame.Core.Screen;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Screen
{
    public class GameScreenManager:ScreenManager
    {
        protected Vector2 _defaultResolution;

        public Vector2 DefaultResolutionResolution
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
        

        protected Game _game;
        public GameScreenManager(GameContext context,GameScreen firstScreen,object param,Vector2 defaultResolution,ScaleMode screenScaleMode)
            : base(context,firstScreen,param)
        {
            this._defaultResolution = defaultResolution;
            this._game = context.Game;
            this._screenScaleMode = screenScaleMode;
            this._game.IsMouseVisible = true;
        }
        public override void Initialize()
        {
            base.Initialize();
            ScreenResolutionChanged();
        }

        public override void LoadObject()
        {
            base.LoadObject();
        }

        public override void Update(Core.Context.IFrameContext context)
        {
            (context as FrameContext).ScreenScale = ScreenScale;
            (context as FrameContext).ScreenMargin = ScreenMargin;
            base.Update(context);
        }

        public override void Draw(Core.Context.IFrameContext context)
        {
            (context as FrameContext).ScreenScale = ScreenScale;
            (context as FrameContext).ScreenMargin = ScreenMargin;
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
                    xMargin=Math.Abs(this._game.GraphicsDevice.Viewport.Width-this.DefaultResolutionResolution.X);
                    if(xMargin!=0.0f){
                        xMargin/=2.0f;
                    }
                    yMargin=Math.Abs(this._game.GraphicsDevice.Viewport.Height-this.DefaultResolutionResolution.Y);
                    if(yMargin!=0.0f){
                        yMargin/=2.0f;
                    }
                    marginMatrix=Matrix.CreateTranslation(xMargin,yMargin,0);
                    break;
                case ScaleMode.Fill:
                    scaleX = ((float)this._game.GraphicsDevice.Viewport.Width) / (float)this._defaultResolution.X;
                    scaleY = ((float)this._game.GraphicsDevice.Viewport.Height) / (float)this._defaultResolution.Y;
                    
                    break;
                case ScaleMode.WidthFit:
                    scaleX = ((float)this._game.GraphicsDevice.Viewport.Width) / (float)this._defaultResolution.X;
                    scaleY = (1 / aspectRate) * scaleX;
                    yMargin = Math.Abs(this._game.GraphicsDevice.Viewport.Height - (float)this._defaultResolution.Y*scaleY);
                    if (yMargin != 0.0f)
                    {
                        yMargin /= 2.0f;
                    }
                    if (this._game.GraphicsDevice.Viewport.Height < (float)this._defaultResolution.Y * scaleY)
                    {
                        yMargin = -yMargin;
                    }
                    marginMatrix=Matrix.CreateTranslation(0,yMargin,0);
                    break;
                case ScaleMode.HeightFit:
                    scaleY = ((float)this._game.GraphicsDevice.Viewport.Height) / (float)this._defaultResolution.Y;
                    scaleX = aspectRate * scaleY;

                    xMargin = Math.Abs(this._game.GraphicsDevice.Viewport.Width - ((float)this._defaultResolution.X*scaleX));
                    if (xMargin != 0.0f)
                    {
                        xMargin /= 2.0f;
                    }
                    if (this._game.GraphicsDevice.Viewport.Width < ((float)this._defaultResolution.X * scaleX))
                    {
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
