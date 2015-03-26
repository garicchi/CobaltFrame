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

        public override void Initialize()
        {
            base.Initialize();
            
        }

        public override void LoadObject()
        {
            base.LoadObject();
        }

        public override void UnloadObject()
        {
            base.UnloadObject();
        }

        public override void Update(ObjectFrameContext frameContext)
        {
            base.Update(frameContext);
        }

        public override void Draw(ObjectFrameContext frameContext)
        {
            this._game.GraphicsDevice.Clear(Color.SteelBlue);
            base.Draw(frameContext);
            
        }

        public override void NavigateTo(object parameter)
        {
            base.NavigateTo(parameter);
        }
    }
}
