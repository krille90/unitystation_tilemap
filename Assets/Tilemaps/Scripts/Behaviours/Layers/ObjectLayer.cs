using Tilemaps.Scripts.Tiles;
using Tilemaps.Scripts.Utils;
using UnityEngine;

namespace Tilemaps.Scripts.Behaviours.Layers
{
    [ExecuteInEditMode]
    public class ObjectLayer : Layer
    {
        public ItemList Items { get; } = new ItemList();
        public ObjectList Objects { get; } = new ObjectList();

        public override void SetTile(Vector3Int position, LayerTile tile)
        {
            var itemTile = tile as ItemTile;

            if (itemTile)
            {
                itemTile.SpawnItem(position, tilemap);
            }
            else
            {
                base.SetTile(position, tile);
            }
        }

        public override void RemoveTile(Vector3Int position)
        {
            Items.DestroyAll(position);
            base.RemoveTile(position);
        }
    }
}