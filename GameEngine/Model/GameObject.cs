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


        private Vector2 _size;

       

        #endregion

        #region Konstruktor
        public GameObject()
        {
            
        }


        public GameObject(Vector2 position, Vector2 size, float speed, Vector2 direction,int hitPoint , Animation currentAnimation, Animation onDestroyAnimation)
        {
            OnDestroyAnimation = onDestroyAnimation;
            Position = position;
            Speed = speed;
            Direction = direction;
            HitPoint = hitPoint;
            _size = size;
            CurrentAnimation = currentAnimation;

            Status = ObjectStatus.Alive;

        }

        #endregion

        #region Property

        public Animation CurrentAnimation { get; set; }
        public Animation OnDestroyAnimation { get; set;}

    public Vector2 Position { get; set; }

        public Rectangle BoxCollider { get { return new Rectangle((int)Position.X, (int)Position.Y, (int)_size.X, (int)_size.Y); } }
        public float Speed { get; set; }
        public Vector2 Direction { get; set; }

        public double Rotation { get; set; }

        public int HitPoint { get; set; }

        public ObjectStatus Status { get; set; }


        public bool isAlive
        {
            get {   if (HitPoint < 1 && Status == ObjectStatus.Dead) return false; else return true; }
        }






        #endregion


        #region Method

        public virtual void Update (GameTime gameTime)
        {
            Position =  new Vector2 (Position.X + Speed * Direction.X * (float)gameTime.ElapsedGameTime.TotalSeconds, Position.Y + Speed *Direction.Y * (float)gameTime.ElapsedGameTime.TotalSeconds);

            if (CurrentAnimation != null)
            {
                CurrentAnimation.Update (gameTime);
            }

            if (HitPoint < 1 && Status == ObjectStatus.Alive)
            {
                Status = ObjectStatus.OnDestroy;
                CurrentAnimation = OnDestroyAnimation;
            }

            if (Status == ObjectStatus.OnDestroy && CurrentAnimation.Counter >= 1)
            {
                Status = ObjectStatus.Dead;
            }
        }

        public virtual void Render (SpriteBatch spriteBatch)
        {
            if (CurrentAnimation != null)
            {
                CurrentAnimation.Render(spriteBatch,this);
            }
        }

        public void CheckHit (GameObject gameObject)
        {
            if (BoxCollider.Intersects(gameObject.BoxCollider))
            {
                Hit();
                gameObject.Hit();
            }
        }



        #endregion


        #region private Methode
        private void Hit()
        {
            HitPoint--;

        }


        #endregion
    }

    public enum ObjectStatus
    {
        Alive,
        OnDestroy,
        Dead
    }
}
