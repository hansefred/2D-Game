using GameEngine.Helper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine.Model
{
    internal class Enemy : DestroyableGameObjects
    {
        #region readonly

        private readonly Player _player;

        #endregion


        #region Property 

        public int ScorePoints { get; set; }

        #endregion

        #region Konstruktor



       
        public Enemy(Vector2 position, Vector2 size, float speed, Vector2 direction, int hitPoint, Animation defaultAnimation, Animation onDestroyAnimation,Player player) : base(position, size, speed, direction, hitPoint, defaultAnimation,onDestroyAnimation)
        {
            _player = player;
        }

        #endregion


        #region override 

        public override void Update(GameTime gameTime)
        {
            if (this.Status == DestroyableObjectStatus.Alive)
            {
                Direction = GameObjectHelper.GetDirection(this, _player);
                Rotation = GameObjectHelper.GetRotation(this, _player);
            }
            else
            {
                Speed = 0;
            }

            base.Update(gameTime);
        }
        #endregion
    }
}
