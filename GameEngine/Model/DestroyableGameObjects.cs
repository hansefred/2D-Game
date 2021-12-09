using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine.Model
{
    public class DestroyableGameObjects : GameObject
    {

        #region Public
        public Animation OnDestroyAnimation { get; set; }
        public Rectangle BoxCollider { get { return new Rectangle((int)Position.X, (int)Position.Y, (int)_size.X, (int)_size.Y); } }

        public int HitPoint { get; set; }

        public DestroyableObjectStatus Status { get; set; }


        public bool isAlive
        {
            get { if (HitPoint < 1 && Status == DestroyableObjectStatus.Dead) return false; else return true; }
        }

        #endregion

        #region Konstruktor

        public DestroyableGameObjects()
        {

        }

        public DestroyableGameObjects(Vector2 position, Vector2 size, float speed, Vector2 direction, int hitPoint,Animation defaultAnimation, Animation onDestroyAnimation): base(position,size,speed,direction,defaultAnimation,1)
        {
            OnDestroyAnimation = onDestroyAnimation;
            HitPoint = hitPoint;
            Status = DestroyableObjectStatus.Alive;
        }

        public override void Update(GameTime gameTime)
        {

            if (HitPoint < 1 && Status == DestroyableObjectStatus.Alive)
            {
                Status = DestroyableObjectStatus.OnDestroy;
                CurrentAnimation = OnDestroyAnimation;
            }

            if (Status == DestroyableObjectStatus.OnDestroy && CurrentAnimation.Counter >= 1)
            {
                Status = DestroyableObjectStatus.Dead;
            }

            base.Update(gameTime);
        }


        public override void Render(SpriteBatch spriteBatch)
        {
            base.Render(spriteBatch);
        }

        public void CheckHit(DestroyableGameObjects gameObject)
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

    public enum DestroyableObjectStatus
    {
        Alive,
        OnDestroy,
        Dead
    }
}
