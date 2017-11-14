using Tilemaps.Scripts.Tiles;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tilemaps.Editor.Brushes.Utils
{
    public class PreviewTile : LayerTile
    {
        public LayerTile ReferenceTile;

        public override LayerType LayerType => ReferenceTile.LayerType;
        
        public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        {
            tileData.sprite = ReferenceTile.PreviewSprite;
        }

        public override bool IsPassableAt(Vector3Int from, Vector3Int to, Tilemap tilemap)
        {
            return ReferenceTile.IsPassableAt(from, to, tilemap);
        }

        public override bool IsPassableAt(Vector3Int position, Tilemap tilemap)
        {
            return ReferenceTile.IsPassableAt(position, tilemap);
        }

        public override bool IsAtmosPassableAt(Vector3Int position, Tilemap tilemap)
        {
            return ReferenceTile.IsAtmosPassableAt(position, tilemap);
        }

        public override bool IsSpaceAt(Vector3Int position, Tilemap tilemap)
        {
            return ReferenceTile.IsSpaceAt(position, tilemap);
        }
    }
}