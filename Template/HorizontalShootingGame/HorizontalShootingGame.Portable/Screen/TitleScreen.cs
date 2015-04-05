using CobaltFrame.Context;
using CobaltFrame.Position;
using CobaltFrame.Screen;
using HorizontalShootingGame.Portable.Object;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizontalShootingGame.Portable.Screen
{
    public class TitleScreen:GameScreen
    {
        public TitleScreen(GameContext context)
            : base(context)
        {
            var player = new Player(context,new Position2D(50,300,200,200),"Texture/Player");
            this.AddDrawableObject(player);
        }
    }
}
