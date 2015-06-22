using CobaltFrame.Common;
using CobaltFrame.Context;
using CobaltFrame.Object;
using CobaltFrame.Transition;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Screen
{
	/// <summary>
	/// ゲーム画面
	/// </summary>
    public abstract class GameScreen:GameObject2D,IGameScreen
    {
        
        public GameScreen()
        {
            this._isNavigateStarted = false;
            this._firstUpdate = false;

            this.OnNavigate += (sc,obj,trans) => { };
            this.SetRect(new Rectangle(0, 0, GameContext.DefaultResolution.X, GameContext.DefaultResolution.Y));
            this._camera2D = new Camera2D();
        }
        #region Field
        protected TimeSpan _screenElapsedTime;
        protected TimeSpan _screenBeginTime;

        private bool _firstUpdate;
        private Color _backgroundColor;
        private bool _isNavigateStarted;
        protected Camera2D _camera2D;
        #endregion

        #region Property
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

        public Camera2D Camera2D
        {
            get { return _camera2D; }
            set { _camera2D = value; }
        }

        #endregion

        #region Method
        public override void Init()
        {
            base.Init();
            
        }

        public override void Update(Context.FrameContext context)
        {
            context.CameraTrans = this._camera2D.GetTransMatrix(this._game.GraphicsDevice.Viewport);
            //もし最初の更新なら
            if (this._firstUpdate)
            {
                //スクリーンが始まった時間を記録
                this._screenBeginTime = context.TotalGameTime;
                this._firstUpdate = false;
            }
            //スクリーンが始まった時間から現在のスクリーンの経過時間を計算
            this._screenElapsedTime = context.TotalGameTime - this._screenBeginTime;
            context.ElapsedScreenTime = this._screenElapsedTime;
            
            base.Update(context);
        }

        /// <summary>
        /// 自身のスクリーンから別のスクリーンへ遷移したいときに自身が呼び出す
        /// </summary>
        /// <param name="screen"></param>
        /// <param name="parameter"></param>
        /// <param name="fromTrans">From trans.</param>
        /// <param name="toTrans">To trans.</param>
        public void Navigate(IGameScreen screen, object parameter = null, IScreenTransition fromTrans = null, IScreenTransition toTrans = null)
        {
            if (_isNavigateStarted == false)
            {
                if (fromTrans != null)
                {
                    this.AddChild(fromTrans);

                    fromTrans.OnCompleted += () =>
                    {
                        RemoveChild(fromTrans);
                        this.OnNavigate(screen, parameter, toTrans);
                    };

                    fromTrans.Start();
                }
                else
                {
                    this.OnNavigate(screen, parameter, toTrans);
                }

                this._isNavigateStarted = true;
            }

            
        }


        /// <summary>
        /// 別のスクリーンから自身のスクリーンに来た時に呼び出される
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="transition">Transition.</param>
        public virtual void NavigateTo(object parameter, IScreenTransition transition = null)
        {
            if (transition != null)
            {
                this.AddChild(transition);

                transition.Start();
                transition.OnCompleted += () =>
                {
                    this.RemoveChild(transition);
                };
            }

            this._firstUpdate = true;
        }
        #endregion

        #region Event
        /// <summary>
        /// ナビゲート開始したとき
        /// </summary>
        public event Action<IGameScreen, object, IScreenTransition> OnNavigate;


        #endregion

        
    }
}
