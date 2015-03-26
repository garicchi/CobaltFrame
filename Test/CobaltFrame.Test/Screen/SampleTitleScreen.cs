using CobaltFrame.Common;
using CobaltFrame.Screen;
using CobaltFrame.Test.Object;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Test.Screen
{
    public class SampleTitleScreen:ScreenBase
    {
        private SampleTexture2DObject _sampleObject1;
        public SampleTitleScreen(ScreenContext context)
            : base(context)
        {
            this._sampleObject1 = new SampleTexture2DObject(new ObjectContext(context.Game),Vector2.Zero,"face");
            this.AddObject(_sampleObject1);
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
            this._game.GraphicsDevice.Clear(Color.SteelBlue);
            base.Draw(frameContext);
            
        }

        protected override void Navigate(ScreenBase screen, object parameter)
        {
            base.Navigate(screen, parameter);
        }
    }
}
