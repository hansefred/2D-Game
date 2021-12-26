
using Microsoft.Xna.Framework;
using System;

namespace GameEngine.Model
{
    public class GameData
    {

        #region private 

        private TimeSpan _timetilsecond;
        private TimeSpan _ingameTime;
        private int _framestilsecond;
        private int _score;
        public int _framesperSecond;

        #endregion




        #region Konstruktor

        public GameData()
        {
            _timetilsecond = TimeSpan.Zero;
            _ingameTime = TimeSpan.Zero;
            _framestilsecond = 0;

            _score = 0;
            _framestilsecond = 0;

        }

        #endregion


        #region Public

        public virtual void Update(GameTime gameTime)
        {
            _ingameTime = _ingameTime + gameTime.ElapsedGameTime;

            _timetilsecond = _timetilsecond + gameTime.ElapsedGameTime;
            _framestilsecond++;

            if (_timetilsecond > TimeSpan.FromSeconds(1))
            {
                _framesperSecond = _framestilsecond;

                _framestilsecond = 0;
                _timetilsecond = TimeSpan.Zero;
            }
        }

        public int GetScore ()
        {
            return _score;
        }

        public TimeSpan GetGameTime ()
        {
            return _ingameTime;
        }

        public int GetFramesperSecond ()
        {
            return _framesperSecond;
        }

        #endregion


    }
}
