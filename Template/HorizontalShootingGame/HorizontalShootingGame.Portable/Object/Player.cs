using CobaltFrame.Context;
using CobaltFrame.Core.Context;
using CobaltFrame.Object;
using CobaltFrame.Position;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizontalShootingGame.Portable.Object
{
    public class Player:Texture2DObject
    {
        private Vector2 touchInputDelta;
        private Vector2 prevTouchPoint;
        public Player(GameContext context,Position2D position,string texturePath)
            : base(context, position, texturePath)
        {
            touchInputDelta = Vector2.Zero;
            prevTouchPoint = Vector2.Zero;
        }

        public override void Update(IFrameContext context)
        {
            base.Update(context);
            TouchCollection collection = TouchPanel.GetState();

            foreach (TouchLocation state in collection)
            {
                var prev = new TouchLocation();
                if (prev.TryGetPreviousLocation(out prev))
                {
                    Debug.WriteLine(prev);
                }
                TouchLocationState tLState = state.State;
                var input = Vector2.Transform(state.Position, (context as FrameContext).GetScreenTransInvert());
                if (tLState == TouchLocationState.Moved)
                {
                    
                }
            }

            
        }
    }
}
