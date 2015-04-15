﻿using CobaltFrame.Mono.Context;
using CobaltFrame.Core.Animation;
using CobaltFrame.Position;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CobaltFrame.Core.Progress;

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
            return ((end - begin) * currentProgress) + begin;
        }
    }
}
