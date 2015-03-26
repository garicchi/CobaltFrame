using CobaltFrame.Common;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Screen
{
    public class ScreenManager:DrawableGameComponent
    {
        protected ScreenBase _currentScreen;

        private ComponentState _componentState;
        public ScreenManager(Game game,ScreenBase initScreen)
            :base(game)
        {
            this._componentState = ComponentState.Non;
            
            ChangeScreen(initScreen,null);

            this._componentState = ComponentState.ConstractorCalled;
        }

        public override void Initialize()
        {
            base.Initialize();
            if (_currentScreen != null)
                _currentScreen.Initialize();

            this._componentState = ComponentState.Initialized;
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            if (_currentScreen != null)
                _currentScreen.LoadObject();

            this._componentState = ComponentState.Loaded;
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
            if(_currentScreen!=null)
            _currentScreen.UnloadObject();

            this._componentState = ComponentState.Unloaded;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (_currentScreen != null)
                _currentScreen.Update(new ObjectFrameContext(gameTime));
        }    

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            if (_currentScreen != null)
            _currentScreen.Draw(new ObjectFrameContext(gameTime));
            
        }

        /// <summary>
        /// スクリーンを切り替える
        /// </summary>
        /// <param name="nextScreen">次の画面</param>
        /// <param name="navigationParameter">画面遷移時のパラメータ</param>
        protected void ChangeScreen(ScreenBase nextScreen,object navigationParameter)
        {
            if (_currentScreen != null)
            {
                if (this._componentState >= ComponentState.Unloaded)
                {
                    _currentScreen.UnloadObject();
                }
                _currentScreen.OnNavigate-=OnNavigateHandler;
                _currentScreen = null;
            }

            if (this._componentState >= ComponentState.Initialized)
            {
                nextScreen.Initialize();
                nextScreen.NavigateTo(navigationParameter);
            }
            if (this._componentState >= ComponentState.Loaded)
            {
                nextScreen.LoadObject();
            }
            
            nextScreen.OnNavigate += OnNavigateHandler;

            _currentScreen = nextScreen;
            
        }

        /// <summary>
        /// _currentScreenから画面遷移時に呼ばれるハンドラー
        /// </summary>
        /// <param name="screen"></param>
        /// <param name="parameter"></param>
        private void OnNavigateHandler(ScreenBase screen, object parameter)
        {
            ChangeScreen(screen,parameter);
        }
    }
}
