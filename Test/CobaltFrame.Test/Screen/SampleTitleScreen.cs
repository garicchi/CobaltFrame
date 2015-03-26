using CobaltFrame.Common;
using CobaltFrame.Screen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Test.Screen
{
    public class SampleTitleScreen:ScreenBase
    {
        public SampleTitleScreen(ScreenContext context)
            : base(context)
        {

        }

        public override void Initialize(object navigationParameter)
        {
            base.Initialize(navigationParameter);
        }

        public override void LoadScreen()
        {
            base.LoadScreen();
        }

        public override void UnloadScreen()
        {
            base.UnloadScreen();
        }

        public override void Update(ScreenFrameContext frameContext)
        {
            base.Update(frameContext);
        }

        public override void Draw(ScreenFrameContext frameContext)
        {
            base.Draw(frameContext);
        }

        protected override void Navigate(ScreenBase screen, object parameter)
        {
            base.Navigate(screen, parameter);
        }
    }
}
