using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace LoveStar.Base_Components
{
    class Camera
    {
        public static Vector2 offset = Vector2.Zero;

        public static Matrix GetMatrix()
        {
            Matrix Transformation = Matrix.CreateTranslation(new Vector3(-offset.X, -offset.Y, 0));
            return Transformation;
        }
    }
}
