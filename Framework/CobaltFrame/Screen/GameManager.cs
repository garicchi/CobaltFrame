using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using CobaltFrame.Object;
using CobaltFrame.Screen;
using CobaltFrame.Common;
using Microsoft.Xna.Framework;
using CobaltFrame.Context;
using CobaltFrame.Input;

namespace CobaltFrame.Core.Screen
{
	/// <summary>
	/// スクリーンマネージャー
	/// </summary>
    public class GameManager:GameObject,IGameManager,IDisposable
    {
        public GameManager(Game game,Point defaultResolution,Rectangle clientBounds,ScaleMode scaleMode)
        {
            
            //デフォルトの背景色を黒に
            this._backgroundColor = Color.FromNonPremultiplied(10, 10, 10, 255);

            //GameContextにさまざまな情報を渡す
            GameContext.Game = game;
            GameContext.GraphicsManager = new GraphicsDeviceManager(game);
            GameContext.ClientBounds = clientBounds;
            GameContext.DefaultResolution = defaultResolution;
            GameContext.ScreenScaleMode = scaleMode;
            //ContentContextのセットアップ
            ResourceContext.Setup(game.Content);

            
        }

        #region Field

        protected IGameScreen _currentScreen;
        protected Color _backgroundColor;
        //DefaultResolutionからデバイス解像度へのスケール変換行列
        protected Matrix _screenScale;
        //DefaultResolutionからデバイス解像度へのマージン変換行列
        protected Matrix _screenMargin;

        #endregion

        #region Property

        /// <summary>
        /// 現在のスクリーン
        /// </summary>
        /// <value>The current screen.</value>
        public IGameScreen CurrentScreen
        {
            get { return this._currentScreen; }
        }


        public Color BackgroundColor
        {
            get
            {
                return this._backgroundColor;
            }
            set
            {
                this._backgroundColor = value;
            }
        }

        #endregion

        #region Method

        /// <summary>
        /// 画面を変更する
        /// </summary>
        /// <param name="nextScreen">Next screen.</param>
        /// <param name="parameter">Parameter.</param>
        /// <param name="transition">Transition.</param>
        public void ChangeScreen(IGameScreen nextScreen, object parameter, IScreenTransition transition)
        {
            if (this._currentScreen != null)
            {
                this.RemoveChild(this._currentScreen);

                UnRegisterScreenEvent();

                this._currentScreen = null;
            }

            this._currentScreen = nextScreen;

            this.AddChild(this._currentScreen);


            RegisterScreenEvent(this._currentScreen);
            this._currentScreen.NavigateTo(parameter, transition);

        }

        /// <summary>
        /// 画面イベントを登録するメソッド
        /// </summary>
        /// <param name="screen">Screen.</param>
        private void RegisterScreenEvent(IGameScreen screen)
        {
            screen.OnNavigate += this.OnNavigateHandler;
        }

        /// <summary>
        /// 画面イベントの登録を解除するメソッド
        /// </summary>
        private void UnRegisterScreenEvent()
        {
            this._currentScreen.OnNavigate -= this.OnNavigateHandler;

        }

        /// <summary>
        /// _currentScreenから画面遷移時に呼ばれるハンドラー
        /// </summary>
        /// <param name="screen"></param>
        /// <param name="parameter"></param>
        private void OnNavigateHandler(IGameScreen screen, object parameter, IScreenTransition transition)
        {
            this.ChangeScreen(screen, parameter, transition);
        }

        /// <summary>
        /// スクリーンマネージャを破棄
        /// </summary>
        /// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="CobaltFrame.Core.Screen.ScreenManager"/>.
        /// The <see cref="Dispose"/> method leaves the <see cref="CobaltFrame.Core.Screen.ScreenManager"/> in an unusable
        /// state. After calling <see cref="Dispose"/>, you must release all references to the
        /// <see cref="CobaltFrame.Core.Screen.ScreenManager"/> so the garbage collector can reclaim the memory that the
        /// <see cref="CobaltFrame.Core.Screen.ScreenManager"/> was occupying.</remarks>
        public void Dispose()
        {
            this.RemoveChild(this._currentScreen);
        }

        public override void Init()
        {
            base.Init();
            this.UpdateScreenSize(GameContext.DefaultResolution,GameContext.ClientBounds);
        }


        public override void Update(FrameContext context)
        {
            context.ScreenScale = _screenScale;
            context.ScreenMargin = _screenMargin;
            //ゲーム入力を最新の状態に
            InputContext.Update(context);
            
            base.Update(context);
        }

        public override void Draw(FrameContext context)
        {
            context.ScreenScale = _screenScale;
            context.ScreenMargin = _screenMargin;

            //背景色でクリア
            GameContext.Game.GraphicsDevice.Clear(this._backgroundColor);
            
            base.Draw(context);
        }

        protected void UpdateScreenSize(Point defaultResolution,Rectangle clientBounds)
        {
            GameContext.DefaultResolution = defaultResolution;
            GameContext.ClientBounds = clientBounds;
            float scaleX = 1.0f;
            float scaleY = 1.0f;
            //アスペクト比を計算
            float aspectRate = (float)GameContext.DefaultResolution.X / (float)GameContext.DefaultResolution.Y;

            Matrix marginMatrix = Matrix.CreateTranslation(0, 0, 0);
            float xMargin = 0.0f;
            float yMargin = 0.0f;

            switch (GameContext.ScreenScaleMode)
            {
                case ScaleMode.None:
                    //画面の中央に表示されるようにする
                    xMargin = Math.Abs(GameContext.ClientBounds.Width - GameContext.DefaultResolution.X);
                    if (xMargin != 0.0f)
                    {
                        xMargin /= 2.0f;
                    }
                    yMargin = Math.Abs(GameContext.ClientBounds.Height - GameContext.DefaultResolution.Y);
                    if (yMargin != 0.0f)
                    {
                        yMargin /= 2.0f;
                    }

                    marginMatrix = Matrix.CreateTranslation(xMargin, yMargin, 0);

                    break;
                case ScaleMode.Fill:
                    //XとY方向に拡大縮小
                    scaleX = (float)GameContext.ClientBounds.Width / GameContext.DefaultResolution.X;
                    scaleY = (float)GameContext.ClientBounds.Height / GameContext.DefaultResolution.Y;
                    break;
                case ScaleMode.WidthFit:
                    //X方向に拡大縮小
                    scaleX = (float)GameContext.ClientBounds.Width / GameContext.DefaultResolution.X;
                    
                    //Y方向はアスペクト比に応じて計算
                    scaleY = (1.0f / aspectRate) * scaleX;
                    var height = GameContext.DefaultResolution.Y * scaleY;

                    yMargin = Math.Abs((float)GameContext.ClientBounds.Height - (float)height);
                    if (yMargin != 0.0f)
                    {
                        yMargin /= 2.0f;
                    }
                    if (GameContext.ClientBounds.Height < height)
                    {
                        yMargin = -yMargin;
                    }
                    marginMatrix = Matrix.CreateTranslation(0, yMargin, 0);
                    break;
                case ScaleMode.HeightFit:
                    //Y方向に拡大縮小
                    scaleY = (float)GameContext.ClientBounds.Height / GameContext.DefaultResolution.Y;
                    
                    //X方向はアスペクト比に応じて計算
                    scaleX = aspectRate * scaleY;
                    var width = GameContext.DefaultResolution.X * scaleX;
                    xMargin = Math.Abs((float)GameContext.ClientBounds.Width - (float)width);
                    if (xMargin != 0.0f)
                    {
                        xMargin /= 2.0f;
                    }
                    if (GameContext.ClientBounds.Width < width)
                    {
                        xMargin = -xMargin;
                    }
                    marginMatrix = Matrix.CreateTranslation(xMargin, 0, 0);
                    break;
            }

            this._screenScale = Matrix.CreateScale(scaleX, scaleY, 1.0f);
            this._screenMargin = marginMatrix;
        }

        #endregion





        public void ScreenResolutionChanged(Point defaultResolution)
        {
            this.UpdateScreenSize(defaultResolution,GameContext.ClientBounds);
        }

        public void WindowSizeChanged(Rectangle clientBounds)
        {
            this.UpdateScreenSize(GameContext.DefaultResolution, clientBounds);
        }
    }
}
