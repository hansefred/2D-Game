using GameEngine.Model;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine.Helper
{
    static class TextureHelper
    {
        public static Vector2 GetScale (Rectangle textureSize, Vector2 size)
        {
            Vector2 scale = new Vector2(1, 1);

            while ((textureSize.Height*scale.Y) > size.Y)
            {
                scale.Y = scale.Y - 0.01f;

            }
            while ((textureSize.Width * scale.X) > size.X)
            {
                scale.X = scale.X - 0.01f;

            }

            return scale;
        }


        public static IEnumerable<Rectangle> GetAnimationRectangles (Texture2D texture2D, AnimationDefinition animationdefinition)
        {
            var List = new List<Rectangle>();

            int xPos = 0;
            int yPos = 0;

            int Stepx = texture2D.Width / animationdefinition.FramesX;
            int Stepy = texture2D.Height / animationdefinition.FramesY;

            for (int a = 0; a <= animationdefinition.FramesY-1; a++)
            {
                for (int i = 0; i <= animationdefinition.FramesX - 1; i++)
                {
                    List.Add(new Rectangle(xPos, yPos, Stepx, Stepy));

                    xPos = xPos + Stepx;
                }
                xPos = 0;
                yPos = yPos + Stepy;
            }


            if (List.Count == 0)
            {
                List.Add (new Rectangle(xPos, yPos, Stepx,Stepy));
            }
            return List;
        }
    }
}
