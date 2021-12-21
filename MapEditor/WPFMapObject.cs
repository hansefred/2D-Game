using GameEngine.Model.MapDefinitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MapEditor
{
    public class WPFMapObject
    {
        public Border  Border { get; set; }

        public int X { get; set; }
        public int Y { get; set; }


        private MapType _mapType;

        public MapType MapType
        {
            get { return _mapType; }
            set { 
                if (_mapType != null)
                {
                    Border.Background = value.Color;
                }
                _mapType = value; 
            }
        }


        public Map_Object ToMap_Object ()
        {
            if (_mapType is SpawnerType)
                return new Map_Spawner { TextureName = "Spawner", MapPosition = new Microsoft.Xna.Framework.Vector2(X, Y), MaxEnemy = (_mapType as SpawnerType).MaxEnemy, SpawnTimer = TimeSpan.FromSeconds((_mapType as SpawnerType).SpawningTimeinSec) };
            else if (_mapType is WallType)
                return new Map_Wall { TextureName = "Wall", MapPosition = new Microsoft.Xna.Framework.Vector2(X, Y) };
            else
                return new () { MapPosition = new Microsoft.Xna.Framework.Vector2 (X, Y), TextureName = "Default" };
        }

      
    }
}
