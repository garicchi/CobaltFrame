using CobaltFrame.Animation;
using CobaltFrame.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Object
{
    public abstract class GameObject:ObjectUpdater
    {
        protected ObjectContext _objectContext;

        protected Game _game;
        

        public GameObject(ObjectContext context)
            :base()
        {
            
            this._objectContext = context;
            this._game = context.Game;

        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void LoadObject()
        {
            base.LoadObject();

            
        }
        public override void UnloadObject()
        {
            base.UnloadObject();
        }

        public override void Update(ObjectFrameContext frameContext)
        {
            base.Update(frameContext);
        }

    }
}
