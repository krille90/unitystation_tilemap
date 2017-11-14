using Tilemaps.Scripts.Tiles;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tilemaps.Scripts.Behaviours.Layers
{
    [ExecuteInEditMode]
    public class Layer : MonoBehaviour
    {
        public LayerType layerType;
        protected Tilemap tilemap;

        public void Awake()
        {
            tilemap = GetComponent<Tilemap>();
        }

        public virtual void SetTile(Vector3Int position, LayerTile tile)
        {
            tilemap.SetTile(position, tile);
        }
        
        public virtual void RemoveTile(Vector3Int position)
        {
            tilemap.SetTile(position, null);
        }

        public virtual bool IsPassableAt(Vector3Int from, Vector3Int to)
        {
            var tileFrom = tilemap.GetTile<LayerTile>(from);

            if (tileFrom && !tileFrom.IsPassableAt(to, from, tilemap))
            {
                return false;
            }

            var tileTo = tilemap.GetTile<LayerTile>(to);
            return !tileTo || tileTo.IsPassableAt(from, to, tilemap);
        }

        public virtual bool IsPassableAt(Vector3Int position)
        {
            var tile = tilemap.GetTile<LayerTile>(position);
            return !tile || tile.IsPassableAt(position, tilemap);
        }

        public virtual bool IsAtmosPassableAt(Vector3Int position)
        {
            var tile = tilemap.GetTile<LayerTile>(position);
            return !tile || tile.IsAtmosPassableAt(position, tilemap);
        }

        public virtual bool IsSpaceAt(Vector3Int position)
        {
            var tile = tilemap.GetTile<LayerTile>(position);
            return !tile || tile.IsSpaceAt(position, tilemap);
        }

        public void SetPreviewTile(Vector3Int position, LayerTile tile)
        {
            tilemap.SetEditorPreviewTile(position, tile);
        }

        public void ClearPreview()
        {
            tilemap.ClearAllEditorPreviewTiles();
        }
    }
}