using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.UI
{
    public class Texture2DPhysicsObject:Texture2DObject
    {
        protected Body _body;

        public Body Body
        {
            get { return _body; }
            set { _body = value; }
        }
        public Texture2DPhysicsObject(string texturePath,Body body)
            :base(texturePath)
        {
            this._body = body;
        }

        public override void Update(Context.FrameContext context)
        {
            base.Update(context);
            float posX = ConvertUnits.ToDisplayUnits(Body.Position.X);
            float posY = ConvertUnits.ToDisplayUnits(Body.Position.Y);
            this.Rotation = this.Body.Rotation;
            this.SetPosition(new Point((int)posX - this.GetSize().X / 2, (int)posY - this.GetSize().Y / 2));

            
        }
    }
}
