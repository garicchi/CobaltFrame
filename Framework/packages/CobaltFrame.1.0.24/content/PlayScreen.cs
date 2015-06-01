using CobaltFrame.Screen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame
{
    public class PlayScreen:GameScreen
    {
        public PlayScreen()
        {

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

            if (context.ElapsedScreenTime > TimeSpan.FromSeconds(20))
            {
                int score = 10;
                this.Navigate(new ResultScreen(),score);
            }
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
