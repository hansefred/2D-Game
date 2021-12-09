using GameEngine.Helper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace GameEngine.Model
{
    public class Animation
    {

        public List<Rectangle> SpriteSizes = new List<Rectangle>();
        public Vector2 Scale;
        public int Counter;



        #region private

        private AnimationDefinition _animationDefinition;
        private Texture2D _texture2D;
        private TimeSpan _timeperframe = new TimeSpan ();

        




        private int _currentFrame;


        #endregion




        #region Konstruktor


        public Animation(Texture2D Texture, Vector2 size)
        {
         
            _currentFrame = 1;
            _texture2D = Texture;
  
            Counter = 0;
           string definitionfile = $@"Content\{Texture.Name}_definition.json";
            if (File.Exists(definitionfile))
            {
                _animationDefinition = JsonSerializer.Deserialize<AnimationDefinition>(File.ReadAllText(definitionfile));
            }
            else
            {
                _animationDefinition = new AnimationDefinition() { FramesX = 1 , FramesY = 1};
            }


            SpriteSizes = (List<Rectangle>)TextureHelper.GetAnimationRectangles(_texture2D,_animationDefinition);


            Scale = TextureHelper.GetScale(SpriteSizes[0], size);

        }

        #endregion



        public void Update(GameTime elapsedgameTime)
        {
            _timeperframe = _timeperframe + elapsedgameTime.ElapsedGameTime;

            if (_animationDefinition.ElapsedTimeperFrame < _timeperframe)
            {
                _timeperframe = new TimeSpan(0);

                _currentFrame++;
                if (_currentFrame > (_animationDefinition.FramesX * _animationDefinition.FramesY))
                {
                    _currentFrame = 1;
                    Counter++;

                }
            }
        }


        public void Render (SpriteBatch spriteBatch,GameObject gameObject)
        {

            spriteBatch.Draw(_texture2D, gameObject.Position, SpriteSizes[_currentFrame - 1], Color.White, (float)gameObject.Rotation, new Vector2(SpriteSizes[0].Width / 2, SpriteSizes[0].Height / 2), gameObject.CurrentAnimation.Scale, SpriteEffects.None, 0);
        }
    }

    public class AnimationDefinition
    {
        public int FramesX { get; set; }
        public int FramesY { get; set; }
        public TimeSpan ElapsedTimeperFrame { get; set; }

    }
}
