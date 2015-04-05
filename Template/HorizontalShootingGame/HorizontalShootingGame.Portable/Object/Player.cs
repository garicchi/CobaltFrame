using CobaltFrame.Context;
using CobaltFrame.Object;
using CobaltFrame.Position;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizontalShootingGame.Portable.Object
{
    public class Player:Texture2DObject
    {
        public Player(GameContext context,Position2D position,string texturePath)
            : base(context, position, texturePath)
        {

        }
    }
}
