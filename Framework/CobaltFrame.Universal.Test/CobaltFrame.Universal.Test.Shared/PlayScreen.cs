using CobaltFrame.Input;
using CobaltFrame.Screen;
using CobaltFrame.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame
{
    /// <summary>
    /// ゲーム画面
    /// ゲームのプレイヤーなどを配置してゲームをデザインしてください
    /// </summary>
    public class PlayScreen:GameScreen
    {
        //プレイヤー画像
        Texture2DObject _player;
        public PlayScreen()
        {
            this._player = new Texture2DObject("System/Texture/sample_player");
            this._player.SetPosition(new Point(150,150));
            this.AddChild(this._player);
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public override void Init()
        {
            base.Init();

            //Playerが動くという入力を定義
            this.Inputs.RegisterInput("PlayerMove",
                (current, prev) =>
                {
                    //タッチがあったなら
                    return current.IsTouch;
                }, (current, prev) =>
                {
                    //マウスの左ボタンがおされたなら
                    return current.LeftButton==ButtonState.Pressed;
                }, (current, prev) =>
                {
                    //ゲームパッドのAボタンが押されたなら
                    return current.Any(q => q.IsButtonDown(Buttons.A));
                }, (current, prev) =>
                {
                    //キーボードの左キーが押されたなら
                    return current.IsKeyDown(Keys.Left);
                },
                null);
        }

        /// <summary>
        /// リソースの確保
        /// </summary>
        public override void Load()
        {
            base.Load();
        }

        /// <summary>
        /// リソースの解放
        /// </summary>
        public override void Unload()
        {
            base.Unload();
        }

        /// <summary>
        /// ゲーム更新(60fps)
        /// </summary>
        /// <param name="context"></param>
        public override void Update(Context.FrameContext context)
        {
            base.Update(context);

            //定義した入力のどれかの条件がtrueなら
            if (this.Inputs.IsInput("PlayerMove"))
            {
                //プレイヤーを右に動かす
                var pos = this._player.GetPosition();
                this._player.SetPosition(new Point(pos.X + 1, pos.Y));

                
            }
            
            
            //15秒経過したらResult画面に遷移する
            if (context.ElapsedScreenTime > TimeSpan.FromSeconds(15))
            {
                //Result画面にスコア値を渡す
                int score = 10;
                this.Navigate(new ResultScreen(),score);
            }

            
        }

        /// <summary>
        /// ゲーム描画(60fps)
        /// </summary>
        /// <param name="context"></param>
        public override void Draw(Context.FrameContext context)
        {
            base.Draw(context);
        }

        /// <summary>
        /// 別の画面からこの画面に遷移したとき
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="transition"></param>
        public override void NavigateTo(object parameter, IScreenTransition transition = null)
        {
            base.NavigateTo(parameter, transition);
        }
    }
}
