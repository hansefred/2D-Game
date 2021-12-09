using GameEngine.Helper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

using System.Text;

namespace GameEngine.Model
{
    public class GameObject
    {
        #region readonly


        internal Vector2 _size;

       

        #endregion

        #region Konstruktor
        public GameObject()
        {
            
        }


        public GameObject(Vector2 position, Vector2 size, float speed, Vector2 direction , Animation currentAnimation, int layer = 0)
        {
           
            Layer = layer;
            Position = position;
            Speed = speed;
            Direction = direction;
            _size = size;
            CurrentAnimation = currentAnimation;

  

        }

        #endregion

        #region Property

        public Animation CurrentAnimation { get; set; }
       

        public Vector2 Position { get; set; }

    
        public float Speed { get; set; }
        public Vector2 Direction { get; set; }

        public double Rotation { get; set; }

        public int Layer { get; set; }


       






        #endregion


        #region Method

        public virtual void Update (GameTime gameTime)
        {
            Position =  new Vector2 (Position.X + Speed * Direction.X * (float)gameTime.ElapsedGameTime.TotalSeconds, Position.Y + Speed *Direction.Y * (float)gameTime.ElapsedGameTime.TotalSeconds);

            if (CurrentAnimation != null)
            {
                CurrentAnimation.Update (gameTime);
            }

          
        }

        public virtual void Render (SpriteBatch spriteBatch)
        {
            if (CurrentAnimation != null)
            {
                CurrentAnimation.Render(spriteBatch,this);
            }
        }

     



        #endregion


       
    }

    
}
