using GameEngine.Model;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine.Helper
{

    static class GameObjectHelper
    {
        static public Vector2 GetDirection(GameObject From, Vector2 To)
        {
            return (Vector2.Normalize(Vector2.Subtract(To, From.Position)));

        }

        static public Vector2 GetDirection(GameObject From, GameObject To)
        {
            return (Vector2.Normalize(Vector2.Subtract(To.Position, From.Position)));

        }



        static public Vector2 GetRotationPostition(GameObject gameObject)
        {
            return new Vector2((float)(gameObject.Position.X + gameObject.CurrentAnimation.SpriteSizes[0].Width * gameObject.CurrentAnimation.Scale.X / 2 * Math.Cos(gameObject.Rotation)), (float)(gameObject.Position.Y + gameObject.CurrentAnimation.SpriteSizes[0].Height / 2 * gameObject.CurrentAnimation.Scale.Y * Math.Sin(Convert.ToDouble(gameObject.Rotation))));
        }


        public static float GetRotation(GameObject From, GameObject To)
        {

            double angle = (float)Math.Atan2(To.Position.Y - From.Position.Y, To.Position.X - From.Position.X);
            return (float)angle;

        }

        public static float GetRotation(GameObject From, Vector2 To)
        {

            double angle = (float)Math.Atan2(To.Y - From.Position.Y, To.X - From.Position.X);
            return (float)angle;

        }
    }

}
