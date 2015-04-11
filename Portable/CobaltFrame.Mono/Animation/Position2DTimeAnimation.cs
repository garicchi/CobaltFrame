﻿using CobaltFrame.Mono.Context;
using CobaltFrame.Core.Animation;
using CobaltFrame.Position;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Mono.Animation
{
    public class Position2DTimeAnimation:TimeAnimationBase<Position2D>
    {

        
        public Position2DTimeAnimation(GameContext context,TimeSpan duration,Position2D begin,Position2D end)
            : base(context, duration,begin,end)
        {

        }


        protected override Position2D UpdateExpression(Position2D begin, Position2D end, float currentProgress)
        {
            return ((end - begin) * currentProgress) + begin;
        }
    }
}
