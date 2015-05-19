using CobaltFrame.Core.Common;
using CobaltFrame.Core.Context;
using CobaltFrame.Core.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace CobaltFrame.Core.Screen
{
	/// <summary>
	/// スクリーンマネージャー
	/// </summary>
    public abstract class ScreenManager:DrawableObject,IScreenManager,IDisposable
    {
        protected int _screenChacheSize;

		/// <summary>
		/// 過去画面のキャッシュサイズ
		/// </summary>
		/// <value>The size of the screen chache.</value>
        public int ScreenChacheSize
        {
            get
            {
                return this._screenChacheSize;
            }
            set
            {
                this._screenChacheSize = value;
            }
        }

        protected Queue<IScreen> _previousScreenQueue;

		/// <summary>
		/// 過去スクリーンのキュー
		/// </summary>
		/// <value>The previous screen queue.</value>
        public Queue<IScreen> PreviousScreenQueue
        {
            get { return this._previousScreenQueue; }
        }

        protected IScreen _firstScreen;

		/// <summary>
		/// 最初のスクリーン
		/// </summary>
		/// <value>The first screen.</value>
        public IScreen FirstScreen
        {
            get { return this._firstScreen; }
        }

        protected IScreen _currentScreen;

		/// <summary>
		/// 現在のスクリーン
		/// </summary>
		/// <value>The current screen.</value>
        public IScreen CurrentScreen
        {
            get { return this._currentScreen; }
        }

        public ScreenManager(IScreen firstScreen,object param,IScreenTransition trans = null)
            : base()
        {
            this._previousScreenQueue = new Queue<IScreen>();
            this._screenChacheSize = 3;
            this._firstScreen = firstScreen;
            this.ChangeScreen(firstScreen,param,trans);
            
            this._loadState = ObjectLoadState.Created;
        }

		/// <summary>
		/// 画面を変更する
		/// </summary>
		/// <param name="nextScreen">Next screen.</param>
		/// <param name="parameter">Parameter.</param>
		/// <param name="transition">Transition.</param>
        protected void ChangeScreen(IScreen nextScreen, object parameter, IScreenTransition transition)
        {
            if (this._currentScreen != null)
            {
                this.RemoveDrawableObject(this._currentScreen);
                
                UnRegisterScreenEvent();

                this._previousScreenQueue.Enqueue(_currentScreen);

                //キューの画面数がキャッシュサイズに等しくなるまでDequeueし続ける
                while(this._previousScreenQueue.Count > this._screenChacheSize)
                {
                    this._previousScreenQueue.Dequeue();
                }

                this._currentScreen = null;
            }

            this._currentScreen = nextScreen;

            this.AddDrawableObject(this._currentScreen);


            RegisterScreenEvent(this._currentScreen);
            this._currentScreen.NavigateTo(parameter,transition);


            
        }

		/// <summary>
		/// 画面イベントを登録するメソッド
		/// </summary>
		/// <param name="screen">Screen.</param>
        private void RegisterScreenEvent(IScreen screen)
        {
            screen.OnNavigate += this.OnNavigateHandler;
            screen.OnNavigatePrevious += this.OnNavigatePreviousHandler;
        }

		/// <summary>
		/// 画面イベントの登録を解除するメソッド
		/// </summary>
        private void UnRegisterScreenEvent()
        {
            this._currentScreen.OnNavigate -= this.OnNavigateHandler;
            this._currentScreen.OnNavigatePrevious -= this.OnNavigatePreviousHandler;

        }

		/// <summary>
		/// 過去画面に変更するメソッド
		/// </summary>
		/// <param name="prevCount">Previous count.</param>
		/// <param name="parameter">Parameter.</param>
		/// <param name="transition">Transition.</param>
        protected void ChangePreviousScreen(int prevCount,object parameter,IScreenTransition transition)
        {
            if (prevCount <= this._screenChacheSize)
            {
                var targetIndex = (this.PreviousScreenQueue.Count - 1) - prevCount;
                var nextScreen = this.PreviousScreenQueue.ElementAt(targetIndex);
                this.ChangeScreen(nextScreen,parameter,transition);
            }
            else
            {
                throw new ArgumentOutOfRangeException("previous countはキャッシュサイズより小さくなくてはなりません");
            }
        }

        /// <summary>
        /// _currentScreenから画面遷移時に呼ばれるハンドラー
        /// </summary>
        /// <param name="screen"></param>
        /// <param name="parameter"></param>
        private void OnNavigateHandler(IScreen screen, object parameter, IScreenTransition transition)
        {
            this.ChangeScreen(screen, parameter,transition);
        }

		/// <summary>
		/// _currentScreenから画面遷移時に呼ばれるハンドラー
		/// </summary>
		/// <param name="prevCount">Previous count.</param>
		/// <param name="parameter">Parameter.</param>
		/// <param name="transition">Transition.</param>
        private void OnNavigatePreviousHandler(int prevCount, object parameter, IScreenTransition transition)
        {
            this.ChangePreviousScreen(prevCount,parameter,transition);
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
            this.RemoveObject(this._currentScreen);
        }

		/// <summary>
		/// スクリーンの解像度が変更されたとき
		/// </summary>
        public abstract void ScreenResolutionChanged();

		/// <summary>
		/// 初期化関数
		/// </summary>
        public override void Init()
        {
            base.Init();
            this._loadState = ObjectLoadState.Initialized;
        }

		/// <summary>
		/// リソース確保関数
		/// </summary>
        public override void Load()
        {
            base.Load();
            this._loadState = ObjectLoadState.Loaded;
        }

		/// <summary>
		/// リソース解放関数
		/// </summary>
        public override void Unload()
        {
            base.Unload();
            this._loadState = ObjectLoadState.Unloaded;
        }
        
    }
}
