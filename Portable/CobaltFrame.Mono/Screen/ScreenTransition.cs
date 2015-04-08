using CobaltFrame.Context;
using CobaltFrame.Core.Animation;
using CobaltFrame.Object;
using CobaltFrame.Position;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Screen
{
    public abstract class ScreenTransition:DrawableGameObject
    {
        protected Texture2D _transTexture;

        public Texture2D TransTexture
        {
            get { return _transTexture; }
            set { _transTexture = value; }
        }

        public ScreenTransition(GameContext context)
            : base(context,new Position2D(0,0,0,0))
        {

        }

    }
}
