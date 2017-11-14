using System.Collections.Generic;
using Tilemaps.Scripts.Behaviours.Layers;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tilemaps.Scripts.Tiles
{
    public enum LayerType
    {
        Structures,
        Objects,
        Floors,
        Base,
        None
    }

    public abstract class LayerTile : GenericTile
    {
        public LayerType layerType;

        public virtual LayerType LayerType
        {
            get { return layerType; }
            set { layerType = value; }
        }

        public LayerTile[] RequiredTiles;

        public abstract bool IsPassableAt(Vector3Int from, Vector3Int to, Tilemap tilemap);

        public abstract bool IsPassableAt(Vector3Int position, Tilemap tilemap);

        public abstract bool IsAtmosPassableAt(Vector3Int position, Tilemap tilemap);

        public abstract bool IsSpaceAt(Vector3Int position, Tilemap tilemap);
    }
}