using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine.Model
{
    internal class PlayerInputSettings
    {
        #region Property
        public Microsoft.Xna.Framework.Input.Keys Moveup { get; set; }
        public Microsoft.Xna.Framework.Input.Keys Movedown { get; set; }
        public Microsoft.Xna.Framework.Input.Keys Moveleft { get; set; }
        public Microsoft.Xna.Framework.Input.Keys Moveright { get; set; }

        public Microsoft.Xna.Framework.Input.Keys Fire { get; set; }

        #endregion

        #region Konstruktor

        public PlayerInputSettings()
        {
            Moveup = Microsoft.Xna.Framework.Input.Keys.Up;
            Movedown = Microsoft.Xna.Framework.Input.Keys.Down;
            Moveleft = Microsoft.Xna.Framework.Input.Keys.Left;
            Moveright = Microsoft.Xna.Framework.Input.Keys.Right;
            Fire = Microsoft.Xna.Framework.Input.Keys.Space;
        }
        #endregion
    }
}
