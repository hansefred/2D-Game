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

      
    }
}
