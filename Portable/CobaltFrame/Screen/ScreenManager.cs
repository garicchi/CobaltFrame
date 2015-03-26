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

        
        public ScreenManager(Game game,ScreenBase initScreen)
            :base(game)
        {
            ChangeScreen(initScreen,null);
            
        }

        public override void Initialize()
        {
            //CurrentScreenのInitializeはChangeScreenで呼ぶ
            base.Initialize();
        }

        protected override void LoadContent()
        {
            //CurrentScreenのLoadContentはChangeScreenで呼ぶ
            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            if(_currentScreen!=null)
            _currentScreen.UnloadScreen();
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (_currentScreen != null)
            _currentScreen.Update(new ScreenFrameContext(gameTime));
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (_currentScreen != null)
            _currentScreen.Draw(new ScreenFrameContext(gameTime));
            base.Draw(gameTime);
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
                _currentScreen.UnloadScreen();
                _currentScreen.OnNavigate-=OnNavigateHandler;
                _currentScreen = null;
            }

            nextScreen.Initialize(navigationParameter);
            nextScreen.LoadScreen();
            
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
