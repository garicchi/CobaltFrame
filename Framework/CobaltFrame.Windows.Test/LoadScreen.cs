using System;
using System.Threading.Tasks;
using CobaltFrame.Mono;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using CobaltFrame.Screen;
using CobaltFrame.Animation;
using CobaltFrame.Context;
using CobaltFrame.UI;
using CobaltFrame.Transition;

namespace CobaltFrame
{
    /// <summary>
    /// 時間のかかるデータのロードをする画面
    /// </summary>
    public class LoadScreen : GameScreen
    {
        //ロード時に表示される画像
        Texture2DObject _loadingTexture;
        //ロード画像を点滅させるアニメーション
        InstantTimeAnimation<int> _loadingAnimation;
        //ロード用非同期タスク
        Task _loadingTask;
        public LoadScreen()
        {
            this._loadingTexture = new Texture2DObject("System/Texture/loading");
            this._loadingTexture.SetRect(new Rectangle(10, 10, 350, 80));
            this.AddChild(this._loadingTexture);

            this._loadingAnimation = new InstantTimeAnimation<int>(255, 0, TimeSpan.FromSeconds(2), (start, end, progress) =>
            {
                return (int)((float)(end - start) * progress) + start;
            });
            this._loadingAnimation.IsLoop = true;
            this._loadingAnimation.Start();
            this.AddChild(this._loadingAnimation);
        }

        public override void Init()
        {
            //リソースの非同期読み込みの開始
            this._loadingTask = Task.Run(() => this.LoadResource());

            base.Init();
        }

        /// <summary>
        /// ここでリソースを読み込む
        /// </summary>
        private void LoadResource()
        {
            //ここをかえてあらかじめ読み込みたいリソースを確保してください

            var bitmapText = new BitmapTextObject("System/Font/ipagothic", "", 1, Color.White);
            bitmapText.Init();
            bitmapText.Load();

            var player = new Texture2DObject("System/Texture/sample_player");
            player.Init();
            player.Load();
        }


        public override void Update(FrameContext context)
        {
            base.Update(context);

            if (_loadingTask.IsCompleted)
            {
                //ロードが完了したなら
                //タイトル画面へ遷移
                this.Navigate(new TitleScreen(), null
                    , new FadeColorTransition(Color.Black, 0, 255, TimeSpan.FromSeconds(1))
                    , new FadeColorTransition(Color.Black, 255, 0, TimeSpan.FromSeconds(1))
                );

            }
            else
            {
                //ロードが完了してないなら
                //画像表示
                this._loadingTexture.DrawColor = Color.FromNonPremultiplied(255, 255, 255, this._loadingAnimation.CurrentValue);

            }



        }

    }
}

