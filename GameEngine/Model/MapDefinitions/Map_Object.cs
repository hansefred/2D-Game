using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Model.MapDefinitions
{

    [DataContract]
    [KnownType(typeof(Map_Spawner))]
    [KnownType(typeof(Map_Wall))]
    public class Map_Object
    {
        #region public

        [DataMemberAttribute()]
        public Vector2 MapPosition { get; set; }

        [DataMemberAttribute()]
        public string TextureName { get; set; }



        #endregion


        #region Konstruktor

        public Map_Object()
        {


        }

        #endregion


       

    }



}
