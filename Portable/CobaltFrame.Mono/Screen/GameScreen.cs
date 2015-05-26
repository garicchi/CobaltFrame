using CobaltFrame.Mono.Context;
using CobaltFrame.Core.Screen;
using CobaltFrame.Mono.Transition;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CobaltFrame.Mono.Input;
using CobaltFrame.Mono.Object;
using CobaltFrame.Mono.Position;

namespace CobaltFrame.Mono.Screen
{
    public class GameScreen:CobaltFrame.Core.Screen.Screen,IGameObject
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

        private GameInputCollection _inputs;

        public GameInputCollection Inputs
        {
            get { return _inputs; }
            set { _inputs = value; }
        }


		public Box2 Box{ get { return new Box2 (0, 0, (int)GameContext.DefaultResolution.X, (int)GameContext.DefaultResolution.Y);} }
        
		private bool _isNavigateStarted;

        public GameScreen()
            : base()
        {
            
            this._inputs = new GameInputCollection();
			this._isNavigateStarted = false;

        }

        public override void Init()
        {
            base.Init();
            this._firstUpdate = false;
			this._game = GameContext.Game;

        }

        public override void Load()
        {
            base.Load();

            
        }

        public override void Unload()
        {
            base.Unload();
            this._inputs.UnregisterAllInput();
        }

        public override void Update(Core.Context.IFrameContext context)
        {
            var fContext = context as FrameContext;
            if (this._firstUpdate)
            {
                this._screenBeginTime = fContext.TotalGameTime;
                this._firstUpdate = false;
            }
            this._screenElapsedTime = fContext.TotalGameTime - this._screenBeginTime;
            fContext.ElapsedScreenTime = this._screenElapsedTime;
            
            this._inputs.Update();
            
            base.Update(context);
            
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
			if (_isNavigateStarted == false) {
				if (fromTrans != null) {
					var sFromTrans = fromTrans as ScreenTransition;
					AddDrawableObject (sFromTrans);

					sFromTrans.OnCompleted += () => {
						base.Navigate (screen, parameter, fromTrans, toTrans);
						RemoveDrawableObject (sFromTrans);
					};
					sFromTrans.Start ();
				} else {
					base.Navigate (screen, parameter, fromTrans, toTrans);
				}
				this._isNavigateStarted = true;
			}
        }

        public override void NavigatePrevious(int oldNum, object parameter, IScreenTransition fromTrans = null, IScreenTransition toTrans = null)
        {
			if (_isNavigateStarted == false) {
				if (fromTrans != null) {
					var sFromTrans = fromTrans as ScreenTransition;
					AddDrawableObject (sFromTrans);
                
					sFromTrans.OnCompleted += () => {
						base.NavigatePrevious (oldNum, parameter, fromTrans, toTrans);
						RemoveDrawableObject (sFromTrans);
					};
					sFromTrans.Start ();
				} else {
					base.NavigatePrevious (oldNum, parameter, fromTrans, toTrans);
				}
				this._isNavigateStarted = true;
			}
        }


    }
}
