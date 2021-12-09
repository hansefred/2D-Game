
using System;

namespace GameEngine.Model
{
    public class GameData
    {

        public int Score { get; set; } = 0;
        public TimeSpan IngameTime { get; set; }



        public GameData()
        {
            IngameTime = new TimeSpan(0, 0, 0, 0);
        }
    }
}
