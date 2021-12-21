using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace GameEngine.Model.MapDefinitions
{

    [DataContractAttribute]
    public class MapDefinition
    {

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public float MapSizeX { get; set; }
        [DataMember]
        public float MapSizeY { get; set; }

        [DataMember]
        public List<Map_Object> MapObjects = new List<Map_Object>();


        public  Map ToMap (Game game)
        {
            var map = new Map ();

            var RenderSizeX = game.GraphicsDevice.PresentationParameters.BackBufferWidth;
            var RenderSizeY = game.GraphicsDevice.PresentationParameters.BackBufferHeight;

            var MaxX = MapObjects.OrderByDescending (o => o.MapPosition.X).First ();
            var MaxY = MapObjects.OrderByDescending(o => o.MapPosition.Y).First();

            var TextureSizeX = RenderSizeX * MapSizeX / MaxX.MapPosition.X;
            var TextureSizeY = RenderSizeY * MapSizeX / MaxY.MapPosition.Y;

            foreach (Map_Object obj in MapObjects)
            {
                var texture = game.Content.Load<Texture2D>(obj.TextureName);
                var animation = new Animation(texture, new Vector2() { X = TextureSizeX, Y = TextureSizeY });


                if (obj is Map_Spawner)
                {
                    var spawner = (Map_Spawner)obj; 
                    map.MapObjects.Add(new Spawner(spawner.SpawnTimer,spawner.MaxEnemy)
                    {
                        
                        CurrentAnimation = animation,
                        Position = new Vector2(TextureSizeX * obj.MapPosition.X - ((TextureSizeX) / 2), TextureSizeY * obj.MapPosition.Y - ((TextureSizeY) / 2))
                    });

                }
                else if (obj is Map_Wall)
                {
                    map.MapObjects.Add(new CollidingGameObject(new Vector2(TextureSizeX * obj.MapPosition.X - ((TextureSizeX) / 2), TextureSizeY * obj.MapPosition.Y - ((TextureSizeY) / 2)),
                        new Vector2(RenderSizeX, RenderSizeY), 0, new Vector2(0), animation));
                   
                }
                else
                {
                    map.MapObjects.Add(new GameObject()
                    {

                        CurrentAnimation = animation,
                        Position = new Vector2(TextureSizeX * obj.MapPosition.X - ((TextureSizeX) / 2), TextureSizeY * obj.MapPosition.Y - ((TextureSizeY) / 2))
                    });
                }
                   
            }


            return map;
           
        }
    }
}
