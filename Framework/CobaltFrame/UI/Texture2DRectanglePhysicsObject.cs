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
    public class Texture2DRectanglePhysicsObject:Texture2DObject
    {
        protected Body _body;

        public Body Body
        {
            get { return _body; }
            set { _body = value; }
        }

        public Texture2DRectanglePhysicsObject(string texturePath, World world, int width, int height, float dencity)
            :base(texturePath)
        {
            this.SetSize(new Point(width,height));

            this._body = BodyFactory.CreateRectangle(world, ConvertUnits.ToSimUnits(width),ConvertUnits.ToSimUnits(height), dencity);
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
