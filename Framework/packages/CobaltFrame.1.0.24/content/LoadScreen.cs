﻿using System;
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
    public class LoadScreen : GameScreen
    {
        Texture2DObject _loadingTexture;
        InstantTimeAnimation<int> _loadingAnimation;

        Task _loadingTask;
        public LoadScreen()
        {
        }

        public override void Init()
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


            this._loadingTask = Task.Run(() => this.LoadResource());

            base.Init();
        }

        /// <summary>
        /// ここでリソースを読み込む
        /// </summary>
        private void LoadResource()
        {
            var bitmapText = new BitmapTextObject("System/Font/ipagothic", "", 1, Color.White);
            bitmapText.Init();
            bitmapText.Load();
        }


        public override void Update(FrameContext context)
        {
            base.Update(context);

            if (_loadingTask.IsCompleted)
            {
                //ロードが完了したなら
                //画面遷移
                this.Navigate(new TitleScreen(), null
                    , new FadeColorTransition(Color.Black, 0, 255, TimeSpan.FromSeconds(1))
                    , new FadeColorTransition(Color.Black, 255, 0, TimeSpan.FromSeconds(1))
                );

            }
            else
            {
                //ロードが完了してないなら
                this._loadingTexture.DrawColor = Color.FromNonPremultiplied(255, 255, 255, this._loadingAnimation.CurrentValue);

            }



        }

    }
}

