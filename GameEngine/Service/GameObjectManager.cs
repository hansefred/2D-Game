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
        private readonly Game _game;


        #endregion

        #region Konstruktor

        public GameObjectManager(SpriteBatch spriteBatch, Game game)
        {
            _spriteBatch = spriteBatch;
            _game = game;
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
            SpawnEnemy();
        }

        public void Render()
        {
            foreach (GameObject gameObject in _gameObjects.OrderBy ( o => o.Layer))
            {
                if (gameObject is Spawner)
                {
                    gameObject.ToString();
                }
              
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
                foreach (Enemy Enemy in _gameObjects.Where(o => o is Enemy && (o as Enemy).Status == DestroyableObjectStatus.Alive))
                {
                    player.CheckHit(Enemy);

                }
            }
        }

        private void ClearDead()
        {
            for (int i = 0; i < _gameObjects.Count; i++)
            {
                if (_gameObjects[i] is DestroyableGameObjects)
                {
                    if (!(_gameObjects[i] as DestroyableGameObjects).isAlive)
                    {
                        _gameObjects.RemoveAt(i);
                    }
                }
            }
        }


        private void SpawnEnemy ()
        {

            List<GameObject> SpawnerCollection = _gameObjects.Where(o => o is Spawner).ToList();

            foreach (GameObject Spawner in SpawnerCollection)
            {
                if ((Spawner as Spawner).IsReadyToSpawn())
                {
                    _gameObjects.Add(new Enemy(Spawner.Position, new Vector2(50, 50), 100, new Vector2(), 2, new Animation(_game.Content.Load<Texture2D>("Enemy"), new Vector2(100, 100)), new Animation(_game.Content.Load<Texture2D>("Explosion"), new Vector2(100, 100)), GetPlayer()));
                }

            }
        }

        #endregion

    }
}
