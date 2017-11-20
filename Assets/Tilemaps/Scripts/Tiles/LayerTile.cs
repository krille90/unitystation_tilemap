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

        public virtual Matrix4x4 Rotate(Matrix4x4 transformMatrix, bool clockwise)
        {
            return transformMatrix;
        }
    }
}