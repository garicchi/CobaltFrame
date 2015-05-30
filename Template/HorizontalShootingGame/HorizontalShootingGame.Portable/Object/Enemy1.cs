using CobaltFrame.Animation;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizontalShootingGame.Portable.Object
{
    public class Enemy1:EnemyBase
    {
        public Enemy1(IAnimation<Point> animation,TimeSpan startTime)
            : base("Texture/enemy1",animation,startTime)
        {
            
        }

        
    }
}
