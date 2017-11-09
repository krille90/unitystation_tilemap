using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tilemaps.Scripts.Tiles
{
    public abstract class BasicTile : LayerTile
    {
        public bool Passable;
        public bool AtmosPassable;
        public bool IsSpace;
        
        public override bool IsPassableAt(Vector3Int from, Vector3Int to, Tilemap tilemap)
        {
            return IsPassableAt(to, tilemap);
        }

        public override bool IsPassableAt(Vector3Int position, Tilemap tilemap)
        {
            return Passable;
        }

        public override bool IsAtmosPassableAt(Vector3Int position, Tilemap tilemap)
        {
            return AtmosPassable;
        }

        public override bool IsSpaceAt(Vector3Int position, Tilemap tilemap)
        {
            return IsAtmosPassableAt(position, tilemap) && IsSpace;
        }
    }
}