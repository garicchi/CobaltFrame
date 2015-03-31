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

        protected Game _game;
        public GameScreenManager(GameContext context,GameScreen firstScreen,object param,Vector2 defaultResolution,ScaleMode screenScaleMode)
            : base(context,firstScreen,param)
        {
            this._defaultResolution = defaultResolution;
            this._game = context.Game;
            this._screenScaleMode = screenScaleMode;
            
        }
        public override void Initialize()
        {
            base.Initialize();
            ScreenResolutionChanged();
        }

        public override void Draw(Core.Context.IFrameContext context)
        {
            (context as FrameContext).ScreenScale = ScreenScale;
            base.Draw(context);
        }
        public override void ScreenResolutionChanged()
        {
            float scaleX = 1.0f;
            float scaleY = 1.0f;
            float aspectRate = (float)this._game.GraphicsDevice.Viewport.Width/(float)this._game.GraphicsDevice.Viewport.Height;
            switch (this._screenScaleMode)
            {
                case ScaleMode.None:
                    break;
                case ScaleMode.Fill:
                    scaleX = ((float)this._game.GraphicsDevice.Viewport.Width) / (float)this._defaultResolution.X;
                    scaleY = ((float)this._game.GraphicsDevice.Viewport.Height) / (float)this._defaultResolution.Y;
                    break;
                case ScaleMode.WidthFit:
                    scaleX = ((float)this._game.GraphicsDevice.Viewport.Width) / (float)this._defaultResolution.X;
                    scaleY = (1 / aspectRate) * scaleX;
                    break;
                case ScaleMode.HeightFit:
                    scaleY = ((float)this._game.GraphicsDevice.Viewport.Height) / (float)this._defaultResolution.Y;
                    scaleX = aspectRate * scaleY;
                    break;
            }
            
            this._screenScale = Matrix.CreateScale(scaleX,scaleY,1.0f);
            
        }

        
    }
}
