using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace GameEngine.Model.MapDefinitions
{

    [DataContract]
    public class Map_Spawner : Map_Object
    {

        #region private
        [DataMember()]
        public int MaxEnemy { get; set; }
        [DataMember()]
        public TimeSpan SpawnTimer { get; set; }
        #endregion



        public Map_Spawner()
        {

          
        }



    }


}
