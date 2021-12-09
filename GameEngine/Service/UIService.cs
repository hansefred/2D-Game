using GameEngine.HUDs;
using GameEngine.Model;
using Microsoft.Xna.Framework;
using Myra.Graphics2D.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine.Service
{
    internal class UIService
    {
        private Desktop _desktop;


        public UIService(Game1 game, GameObjectManager gameObjectManager)
        {
            Myra.MyraEnvironment.Game = game;
            _desktop = new Desktop();
        }



        public void Update(GameTime gameTime)
        {
            if (_desktop.Root is IngameHUD)
            {
                (_desktop.Root as IngameHUD).Update(gameTime);
            }
            

        }

        public void Render()
        {
            _desktop.Render();
        }

        public void ChangeUI(Widget UI)
        {
            _desktop.Root = UI;
        }



    }
}
