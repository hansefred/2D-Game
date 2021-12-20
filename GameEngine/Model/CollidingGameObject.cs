using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine.Model
{
    public class CollidingGameObject : GameObject
    {
        public Rectangle BoxCollider { get { return new Rectangle((int)Position.X, (int)Position.Y, (int)_size.X, (int)_size.Y); } }


        public bool CheckHit(CollidingGameObject gameObject)
        {
            if (BoxCollider.Intersects(gameObject.BoxCollider))
            {
              return true;
            }
            else
                return false;
        }


        public CollidingGameObject(Vector2 position, Vector2 size, float speed, Vector2 direction, Animation currentAnimation,  int layer = 0): base(position, size, speed, direction, currentAnimation, layer)
        {
            
        }

    }


   
}
