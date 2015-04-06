using CobaltFrame.Context;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Input
{
    public static class GameTouchPanel
    {
        private static Matrix _screenTrans=Matrix.Identity;
        private static TouchCollection _touchCollectionTranslated=new TouchCollection();
        
        public static TouchCollection GetState()
        {
            return _touchCollectionTranslated;
        }

        public static TouchCollection GetOriginalState()
        {
            return TouchPanel.GetState();
        }

        public static void Update(FrameContext context)
        {
            _screenTrans = context.ScreenTrans;
            var collection = TouchPanel.GetState();
            _touchCollectionTranslated.Clear();
            foreach (TouchLocation state in collection)
            {
                var location = new TouchLocation(
                    state.Id,
                    state.State,
                    Vector2.Transform(state.Position, context.GetScreenTransInvert())
                    );
                
                _touchCollectionTranslated.Add(location);
            }
        }
        
    }
}
