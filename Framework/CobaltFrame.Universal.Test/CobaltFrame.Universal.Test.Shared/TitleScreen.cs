using CobaltFrame.Screen;
using CobaltFrame.Transition;
using CobaltFrame.UI;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame
{
    /// <summary>
    /// ゲームのタイトル画面
    /// </summary>
    public class TitleScreen:GameScreen
    {
        //タイトル文字
        BitmapTextObject _titleText;
        //Startボタン
        TextButtonObject _startButton;
        public TitleScreen()
        {
            //ここを自分のゲームのタイトルに変えてください
            var gameTitle = "GameTitle";
            /*
            this._titleText = new BitmapTextObject("System/Font/ipagothic", gameTitle, 6, Color.White);
            this._titleText.SetRect(new Rectangle(this.GetCenter().X / 2, this.GetCenter().Y / 2, 800, 100));
            this.AddChild(this._titleText);

            this._startButton = new TextButtonObject("Start", "System/Font/ipagothic", "System/Texture/button_off", "System/Texture/button_on");
            this._startButton.TextObject.FontScale = 4;
            this._startButton.SetRect(new Rectangle(this.GetCenter().X / 2 + 150, this.GetCenter().Y / 2 + 200, 220, 100));

            this._startButton.OnClick += (pos) =>
            {
                //Startボタンがクリックされたとき、Play画面へ遷移
                this.Navigate(new PlayScreen(), null
                    , new FadeColorTransition(Color.Black, 0, 255, TimeSpan.FromSeconds(1))
                    , new FadeColorTransition(Color.Black, 255, 0, TimeSpan.FromSeconds(1)));
            };

            this.AddChild(this._startButton);
            */
            //var model = new ModelObject("testmodel");
            //AddChild(model);


        }

        public override void Init()
        {
            base.Init();

        }


        public override void Load()
        {
            base.Load();
        }

        public override void Unload()
        {
            base.Unload();
        }

        public override void Update(Context.FrameContext context)
        {
            base.Update(context);
        }


        public override void Draw(Context.FrameContext context)
        {
            base.Draw(context);
        }


        public override void NavigateTo(object parameter, IScreenTransition transition = null)
        {
            base.NavigateTo(parameter, transition);
        }

    }
}
