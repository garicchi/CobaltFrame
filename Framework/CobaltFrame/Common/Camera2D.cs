using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Common
{
    //http://www.david-amador.com/2009/10/xna-camera-2d-with-zoom-and-rotation/
    public class Camera2D
    {
        protected float _zoom; // Camera Zoom
        public Matrix Transform{ get; set; } // Matrix Transform
        public Vector2 _position; // Camera Position
        protected float _rotation; // Camera Rotation


        public float Zoom
        {
            get { return _zoom; }
            set { _zoom = value; if (_zoom < 0.1f) _zoom = 0.1f; } // Negative zoom will flip image
        }

        public float Rotation
        {
            get { return _rotation; }
            set { _rotation = value; }
        }

        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        // Auxiliary function to move the camera
        public void Move(Vector2 amount)
        {
            Position += amount;
        }
        // Get set position
        

        public Camera2D()
        {
            _zoom = 1.0f;
            _rotation = 0;
            _position = Vector2.Zero;
        }

        public Matrix GetTransMatrix(Viewport v)
        {
            Transform =       // Thanks to o KB o for this solution
              Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0)) *
                                         Matrix.CreateRotationZ(Rotation) *
                                         Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                                         Matrix.CreateTranslation(new Vector3(v.Width * 0.5f, v.Height * 0.5f, 0));
            return Transform;
        }
    }
}
