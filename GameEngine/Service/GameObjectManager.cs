using GameEngine.Model;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine.Service
{
    internal class GameObjectManager
    {
        #region private
        private List<GameObject> _gameObjects = new List<GameObject>();
        private readonly SpriteBatch _spriteBatch;

        #endregion

        #region Konstruktor

        public GameObjectManager(SpriteBatch spriteBatch)
        {
            _spriteBatch = spriteBatch;
        }
        #endregion


        #region Method

        public void Add(GameObject gameObject)
        {
            _gameObjects.Add(gameObject);
        }


        public void Update(GameTime gameTime)
        {
            foreach (GameObject gameObject in _gameObjects)
            {
                gameObject.Update(gameTime);
            }

            CheckHit();
            ClearDead();
        }

        public void Render()
        {
            foreach (GameObject gameObject in _gameObjects)
            {
                gameObject.Render(_spriteBatch);
            }

        }

        public Player GetPlayer()
        {
            return (_gameObjects.Where(o => o is Player).FirstOrDefault()) as Player;
        }

        #endregion

        #region private Methode 

        private void CheckHit()
        {
            var player = GetPlayer();
            if (player != null)
            {
                foreach (Enemy Enemy in _gameObjects.Where(o => o is Enemy && o.Status == ObjectStatus.Alive))
                {
                    player.CheckHit(Enemy);

                }
            }
        }

        private void ClearDead()
        {
            for (int i = 0; i < _gameObjects.Count; i++)
            {
                if (!_gameObjects[i].isAlive)
                {
                    _gameObjects.RemoveAt(i);
                }
            }
        }

        #endregion

    }
}
