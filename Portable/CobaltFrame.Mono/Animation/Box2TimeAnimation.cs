using CobaltFrame.Mono.Context;
using CobaltFrame.Core.Animation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CobaltFrame.Core.Progress;
using CobaltFrame.Mono.Position;
using Microsoft.Xna.Framework;

namespace CobaltFrame.Mono.Animation
{
    public class Box2TimeAnimation:TimeProgress<Box2>
    {

        
        public Box2TimeAnimation(TimeSpan duration,Box2 begin,Box2 end)
            : base(duration,begin,end)
        {

        }


        protected override Box2 UpdateExpression(Box2 begin, Box2 end, float currentProgress)
        {
            var box = new Box2(begin);
            var deltaVec = end.GetLocation() - begin.GetLocation();
            var multiVec = new Vector2(
                deltaVec.X*currentProgress,
                deltaVec.Y*currentProgress
                );
            box.SetLocation(multiVec + begin.GetLocation());
            return box;
        }
    }
}
