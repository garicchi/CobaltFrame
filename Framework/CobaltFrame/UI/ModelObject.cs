using CobaltFrame.Input;
using CobaltFrame.Object;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.UI
{
    public class ModelObject:GameObject
    {
        private Model _model;
        private string _modelPath;
        public ModelObject(string modelPath)
        {
            this._modelPath = modelPath;
        }

        public override void Init()
        {
            base.Init();
        }

        public override void Load()
        {
            base.Load();
            this._model = this._game.Content.Load<Model>(this._modelPath);

        }

        public override void Unload()
        {
            base.Unload();
        }

        public override void Update(Context.FrameContext context)
        {
            base.Update(context);
        }
        float cameraRotation = 0;
        float cameraArc = 0;
        public override void Draw(Context.FrameContext context)
        {
            base.Draw(context);

            // Compute camera matrices.
            const float cameraDistance = 100;

            Matrix view = Matrix.CreateLookAt(
                new Vector3(4.0f, 4.0f, InputContext.MouseState.ScrollWheelValue),
                Vector3.Zero,
                Vector3.Up
            );

            Matrix projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.ToRadians(45.0f),
                (float)this._game.GraphicsDevice.Viewport.Width / (float)this._game.GraphicsDevice.Viewport.Height,
                1.0f,
                100.0f
            );

            //Matrix[] bones = animationPlayer.GetSkinTransforms();
            //this._game.GraphicsDevice.Clear(Color.Blue);
            foreach (ModelMesh mesh in this._model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    //effect.SetBoneTransforms(bones);
                    effect.View = view;
                    effect.Projection = projection;

                    effect.EnableDefaultLighting();

                    effect.SpecularColor = Vector3.Zero;
                }

                mesh.Draw();
            }
        }
    }
}
