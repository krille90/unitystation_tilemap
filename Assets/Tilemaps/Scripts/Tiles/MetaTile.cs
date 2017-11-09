using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using World.Utils;

namespace Tilemaps.Scripts.Tiles
{
    public class MetaTile : GenericTile
    {
        public LayerTile Structure;
        public LayerTile Object;
        public LayerTile Floor;
        public LayerTile Base;
        
        public override Sprite PreviewSprite => Structure.PreviewSprite; //  TODO make proper preview sprite

        private void OnValidate()
        {
            CheckTileType(ref Structure, LayerType.Structures);
            CheckTileType(ref Object, LayerType.Objects);
            CheckTileType(ref Floor, LayerType.Floors);
            CheckTileType(ref Base, LayerType.Base);
        }

        private static void CheckTileType(ref LayerTile tile, LayerType requiredType)
        {
            if (tile != null && tile.LayerType != requiredType)
            {
                tile = null;
            }
        }

        public IEnumerable<LayerTile> GetTiles()
        {
            var list = new List<LayerTile>();

            if(Base) list.Add(Base);
            if(Floor) list.Add(Floor);
            if(Object) list.Add(Object);
            if(Structure) list.Add(Structure);

            return list.ToArray();
        }
    }
}