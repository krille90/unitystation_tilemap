using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tilemaps.Scripts.Tiles
{
    public abstract class BasicTile : LayerTile
    {
        public bool Passable;
        public bool AtmosPassable;
        public bool IsSealed;
        
        public bool IsPassable()
        {
            return Passable;
        }

        public bool IsAtmosPassable()
        {
            return AtmosPassable;
        }

        public bool IsSpace()
        {
            return IsAtmosPassable() && !IsSealed;
        }
    }
}