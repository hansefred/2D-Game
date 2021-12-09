using GameEngine.Model.MapDefinitions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;

namespace GameEngine.Model
{


    public class Map
    {


        #region private

        public List<GameObject> MapObjects { get; set; } = new List<GameObject>();

        #endregion

        public Map()
        {

        }



        #region public


        public void Update(GameTime gameTime)
        {
            MapObjects.ForEach(x => x.Update(gameTime));
        }

        public void Render(SpriteBatch spriteBatch)
        {
            MapObjects.ForEach(x => x.Render(spriteBatch));
        }



        public static Map LoadMap(string MapDefintion, Game game)
        {

            string definitionfile = $@"Map\{MapDefintion}_definition.json";
            MapDefinition mapDefinitions = null;

            using (FileStream fs = new FileStream(definitionfile, FileMode.Open, FileAccess.Read))
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(MapDefinition));

                mapDefinitions = (ser.ReadObject(fs) as MapDefinition);
            }

            return mapDefinitions.ToMap(game);



        }

        public static void SaveMap(string MapDefintion, MapDefinition map)
        {
            string definitionfile = $@"Map\{MapDefintion}_definition.json";



            MemoryStream stream = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(MapDefinition));
            //Here's the exception
            ser.WriteObject(stream, map);
            stream.Position = 0;
            StreamReader sr = new StreamReader(stream);

            File.WriteAllText(definitionfile, sr.ReadToEnd());
        }

        #endregion

    }
}