using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine.Model
{
    internal class Spawner : GameObject
    {
        #region private

        private int _maxEnemy { get; set; }

        private TimeSpan _spawnTimer { get; set; }
        private TimeSpan _currentTimer { get; set; }

        private bool _readyToSpawn { get; set; }
        #endregion



        public Spawner( TimeSpan spawntimer, int MaxEnemySize)
        {
            _spawnTimer = spawntimer;
            _maxEnemy = MaxEnemySize;
            _readyToSpawn = false;
            _currentTimer = new TimeSpan(0);
        }


        public bool IsReadyToSpawn ()
        {
            if (_readyToSpawn)
            {
                _readyToSpawn = false;
                return true;
            }
            else
                return false;
        }


        public override void Update(GameTime gameTime)
        {
            if (_maxEnemy > 0 && _readyToSpawn == false)
            {

                _currentTimer = _currentTimer + gameTime.ElapsedGameTime;

                if (_currentTimer > _spawnTimer)
                {
                    _readyToSpawn = true;
                    _maxEnemy--;
                    _currentTimer = new TimeSpan(0);
                }
            }

            base.Update(gameTime);
        }

    }
}
