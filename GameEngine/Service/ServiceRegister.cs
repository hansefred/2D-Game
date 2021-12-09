using Autofac;
using GameEngine.Model;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine.Service
{
    internal class ServiceRegister
    {

        public static IContainer Register (Game game, GraphicsDeviceManager graphicsDeviceManager, SpriteBatch spriteBatch)
        {
            var builder = new ContainerBuilder ();

            builder.RegisterInstance<Game> (game).SingleInstance ();
            builder.RegisterInstance<Game1>(game as Game1).SingleInstance();
            builder.RegisterInstance<GraphicsDeviceManager>(graphicsDeviceManager);
            builder.RegisterInstance<SpriteBatch>(spriteBatch);
            builder.RegisterInstance<GraphicsDevice>(game.GraphicsDevice).SingleInstance();
            builder.RegisterType<PlayerManager>().SingleInstance();
            builder.RegisterType<GameObjectManager>().SingleInstance();
            builder.RegisterType<GameObject>();
            builder.RegisterType<UIService>();





            return builder.Build ();
        }
    }
}
