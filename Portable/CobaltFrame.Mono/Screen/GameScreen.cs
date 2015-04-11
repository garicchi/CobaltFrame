using CobaltFrame.Context;
using CobaltFrame.Core.Screen;
using CobaltFrame.Transition;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Screen
{
    public class GameScreen:CobaltFrame.Core.Screen.Screen
    {
        protected Game _game;

        protected TimeSpan _screenElapsedTime;

        protected TimeSpan _screenBeginTime;

        private bool _firstUpdate;

        private Color _screenBackgroundColor;

        public Color ScreenBackgroundColor
        {
            get { return _screenBackgroundColor; }
            set { _screenBackgroundColor = value; }
        }
        
        public GameScreen(GameContext context)
            : base(context)
        {
            this._game = context.Game;
            
        }

        public override void Initialize()
        {
            base.Initialize();
            this._firstUpdate = false;
        }

        public override void LoadObject()
        {
            base.LoadObject();

            var screenWidth = this._game.GraphicsDevice.Viewport.Width;
            var screenHeight = this._game.GraphicsDevice.Viewport.Height;
            var texture = new Texture2D(
                this._game.GraphicsDevice,
                screenWidth,
                screenHeight,
                false,
                SurfaceFormat.Color
                );

            var colors = Enumerable.Repeat<Color>(this._screenBackgroundColor,screenWidth*screenHeight).ToArray();
            texture.SetData<Color>(colors);
            
        }

        public override void Update(Core.Context.IFrameContext context)
        {
            base.Update(context);
            var fContext = context as FrameContext;
            if (this._firstUpdate)
            {
                this._screenBeginTime = fContext.TotalGameTime;
                this._firstUpdate = false;
            }
            this._screenElapsedTime = fContext.TotalGameTime - this._screenElapsedTime;

            
        }

        public override void Draw(Core.Context.IFrameContext context)
        {
            
            
            base.Draw(context);
            
        }



        public override void NavigateTo(object parameter,IScreenTransition transition = null)
        {
            if (transition != null)
            {
                var sTransition = transition as ScreenTransition;
                this.AddDrawableObject(sTransition);
                
                sTransition.Start();
                sTransition.OnCompleted += () =>
                {
                    base.NavigateTo(parameter, transition);
                    this.RemoveDrawableObject(sTransition);
                };
            }
            else
            {
                base.NavigateTo(parameter,transition);
            }
            this._firstUpdate = true;
        }

        public override void Navigate(Core.Screen.IScreen screen, object parameter,IScreenTransition fromTrans=null,IScreenTransition toTrans=null)
        {
            if (fromTrans != null)
            {
                var sFromTrans = fromTrans as ScreenTransition;
                AddDrawableObject(sFromTrans);
                
                sFromTrans.OnCompleted += () =>
                {
                    base.Navigate(screen, parameter,fromTrans,toTrans);
                    RemoveDrawableObject(sFromTrans);
                };
                sFromTrans.Start();
            }
            else
            {
                base.Navigate(screen, parameter, fromTrans, toTrans);
            }
            
        }

        public override void NavigatePrevious(int oldNum, object parameter, IScreenTransition fromTrans = null, IScreenTransition toTrans = null)
        {
            if (fromTrans != null)
            {
                var sFromTrans = fromTrans as ScreenTransition;
                AddDrawableObject(sFromTrans);
                
                sFromTrans.OnCompleted += () =>
                {
                    base.NavigatePrevious(oldNum, parameter, fromTrans, toTrans);
                    RemoveDrawableObject(sFromTrans);
                };
                sFromTrans.Start();
            }
            else
            {
                base.NavigatePrevious(oldNum, parameter, fromTrans, toTrans);
            }
        }
        
    }
}
