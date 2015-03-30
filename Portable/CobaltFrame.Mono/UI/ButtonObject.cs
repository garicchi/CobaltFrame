using CobaltFrame.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.UI
{
    public class ButtonObject:UIObject
    {
        protected ButtonState _state;

        public ButtonState State
        {
            get { return _state; }
        }
        public ButtonObject(GameContext context)
            : base(context)
        {

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

        public override void Update(Core.Context.IFrameContext context)
        {
            base.Update(context);
        }

        public override void Draw(Core.Context.IFrameContext context)
        {
            base.Draw(context);
        }
    }
}
