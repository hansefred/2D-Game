using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MapEditor
{
    public class MapType
    {

       public SolidColorBrush Color { get; set; }
        public string Name { get; set; }





        public MapType ShallowCopy()
        {
            return (MapType)this.MemberwiseClone();
        }
    }

    public class SpawnerType : MapType
    {
        public int MaxEnemy { get; set; }

        public int SpawningTimeinSec { get; set; }
    }
    
    public class WallType : MapType
    {
        
    }
}
