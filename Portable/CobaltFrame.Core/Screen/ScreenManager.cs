using CobaltFrame.Core.Common;
using CobaltFrame.Core.Context;
using CobaltFrame.Core.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Core.Screen
{
    public abstract class ScreenManager:DrawableObject,IScreenManager,IDisposable
    {

        protected int _screenChacheSize;
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
        public Queue<IScreen> PreviousScreenQueue
        {
            get { return this._previousScreenQueue; }
        }

        protected IScreen _firstScreen;
        public IScreen FirstScreen
        {
            get { return this._firstScreen; }
        }

        protected IScreen _currentScreen;
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

        private void RegisterScreenEvent(IScreen screen)
        {
            screen.OnNavigate += this.OnNavigateHandler;
            screen.OnNavigatePrevious += this.OnNavigatePreviousHandler;
        }

        private void UnRegisterScreenEvent()
        {
            this._currentScreen.OnNavigate -= this.OnNavigateHandler;
            this._currentScreen.OnNavigatePrevious -= this.OnNavigatePreviousHandler;

        }

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

        private void OnNavigatePreviousHandler(int prevCount, object parameter, IScreenTransition transition)
        {
            this.ChangePreviousScreen(prevCount,parameter,transition);
        }


        public void Dispose()
        {
            this.RemoveObject(this._currentScreen);
        }


        public abstract void ScreenResolutionChanged();

        public override void Init()
        {
            base.Init();
            this._loadState = ObjectLoadState.Initialized;
        }

        public override void Load()
        {
            base.Load();
            this._loadState = ObjectLoadState.Loaded;
        }

        public override void Unload()
        {
            base.Unload();
            this._loadState = ObjectLoadState.Unloaded;
        }
        
    }
}
