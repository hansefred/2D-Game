using GameEngine.Helper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine.Model
{
    public class Player : DestroyableGameObjects
    {
        #region Property 
        private PlayerInputSettings _playerInputSettings { get; set; } = new PlayerInputSettings();

        #endregion


        #region Konstruktor




        public Player(Vector2 position, Vector2 size, float speed, Vector2 direction, int hitPoint, Animation defaultAnimation,Animation onDestroyAnimation) : base(position,size,speed,direction,hitPoint, defaultAnimation,onDestroyAnimation)
        {

        }

        #endregion

        #region Override 

        public override void Update (GameTime gametime)
        {
            // Update Movement
            int X = 0, Y = 0;
            if (Keyboard.GetState().IsKeyDown(_playerInputSettings.Movedown))
            {
                Y++;
            }
            if (Keyboard.GetState().IsKeyDown(_playerInputSettings.Moveleft))
            {
               X--;
            }
            if (Keyboard.GetState().IsKeyDown(_playerInputSettings.Moveright))
            {
                X++;

            }
            if (Keyboard.GetState().IsKeyDown(_playerInputSettings.Moveup))
            {
                Y--;
            }


            if (Keyboard.GetState().IsKeyDown(_playerInputSettings.Fire))
            {
                
            }
            Direction = new Vector2(X, Y);


            //Update Rotation
            var mousestate = Mouse.GetState();
            Rotation = GameObjectHelper.GetRotation(this, mousestate.Position.ToVector2());


            base.Update(gametime);

        }

        #endregion
    }
}
