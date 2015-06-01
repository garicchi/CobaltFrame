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
    //最初のゲーム画面
    public class TitleScreen:GameScreen
    {
        BitmapTextObject _titleText;
        TextButtonObject _startButton;
        public TitleScreen()
        {

        }

        /// <summary>
        /// コンテンツ初期化
        /// </summary>
        public override void Init()
        {
            base.Init();

            var gameTitle = "GameTitle";
            this._titleText = new BitmapTextObject("System/Font/ipagothic",gameTitle,6,Color.White);
            this._titleText.SetRect(new Rectangle(this.GetCenter().X/2,this.GetCenter().Y/2,800,100));
            this.AddChild(this._titleText);

            this._startButton = new TextButtonObject("AAAAAAAAAA","System/Font/ipagothic","System/Texture/button_off","System/Texture/button_on");
            this._startButton.SetRect(new Rectangle(this.GetCenter().X / 2+300, this.GetCenter().Y / 2 + 200,450,200));
            this._startButton.TextObject.SetRect(new Rectangle(10,10,400,100));
            this._startButton.OnClick += (pos) =>
            {
                this.Navigate(new PlayScreen(),null
                    , new FadeColorTransition(Color.Black, 0, 255, TimeSpan.FromSeconds(1))
                    , new FadeColorTransition(Color.Black, 255, 0, TimeSpan.FromSeconds(1)));
            };
            
            this.AddChild(this._startButton);
        }

        /// <summary>
        /// リソースのロード
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
        /// コンテンツ更新(60fps)
        /// </summary>
        /// <param name="context"></param>
        public override void Update(Context.FrameContext context)
        {
            base.Update(context);
        }

        /// <summary>
        /// コンテンツ描画(60fps)
        /// </summary>
        /// <param name="context"></param>
        public override void Draw(Context.FrameContext context)
        {
            base.Draw(context);
        }

        /// <summary>
        /// 別の画面から遷移したとき
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="transition"></param>
        public override void NavigateTo(object parameter, IScreenTransition transition = null)
        {
            base.NavigateTo(parameter, transition);
        }

    }
}
