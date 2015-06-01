using System;
using Microsoft.Xna.Framework;
using CobaltFrame.Mono;
using CobaltFrame.Screen;
using CobaltFrame.Transition;
using CobaltFrame.Context;
using CobaltFrame.UI;

namespace CobaltFrame
{
    public class ResultScreen : GameScreen
    {
        private BitmapTextObject _scoreText;
        private BitmapTextObject _yourResultText;
        TextButtonObject _titleButton;
        public ResultScreen()
            : base()
        {
        }

        public override void Init()
        {
            base.Init();
            this._yourResultText = new BitmapTextObject("System/Font/ipagothic", "your score is", 3, Color.White);
            this._yourResultText.SetRect(new Rectangle(this.GetCenter().X - 250, this.GetCenter().Y - 200, 600, 200));
            this.AddChild(this._yourResultText);

            this._scoreText = new BitmapTextObject("System/Font/ipagothic", "0", 6, Color.White);
            this._scoreText.SetRect(new Rectangle(this.GetCenter().X - 100, this.GetCenter().Y + 20, 200, 200));
            this.AddChild(this._scoreText);

            this._titleButton = new TextButtonObject("Back Title","System/Font/ipagothic","System/Texture/button_off", "System/Texture/button_on");
            this._titleButton.SetRect(new Rectangle(100, 100, 240, 60));
            this._titleButton.OnClick += (pos) =>
            {
                this.Navigate(new TitleScreen(), null
                    , new FadeColorTransition(Color.Black, 0, 255, TimeSpan.FromSeconds(1))
                    , new FadeColorTransition(Color.Black, 255, 0, TimeSpan.FromSeconds(1)));
            };
            this.AddChild(this._titleButton);
        }

        public override void Load()
        {
            base.Load();


        }

        public override void NavigateTo(object parameter, IScreenTransition transition)
        {
            base.NavigateTo(parameter, transition);
            var score = (int)parameter;
            this._scoreText.Text = score.ToString();

            DataContext<SaveData>.Data.Score = score;
        }

        public override void Unload()
        {
            base.Unload();
        }

        public override void Update(FrameContext context)
        {
            base.Update(context);
        }

        public override void Draw(FrameContext context)
        {
            base.Draw(context);
        }
    }
}

