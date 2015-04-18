using CobaltFrame.Mono.Animation;
using CobaltFrame.Mono.Context;
using CobaltFrame.Mono.Position;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizontalShootingGame.Portable.Object
{
    public class Enemy1:EnemyBase
    {
        public Enemy1(Box2TimeAnimation animation,TimeSpan startTime)
            : base(new Box2(-100, -100, 120, 80), "Texture/Enemy1",animation,startTime)
        {
            
        }

        
    }
}
