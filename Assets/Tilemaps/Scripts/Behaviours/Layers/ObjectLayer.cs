using Tilemaps.Scripts.Behaviours.Objects;
using Tilemaps.Scripts.Tiles;
using Tilemaps.Scripts.Utils;
using UnityEngine;

namespace Tilemaps.Scripts.Behaviours.Layers
{
    [ExecuteInEditMode]
    public class ObjectLayer : Layer
    {
        private ItemList _items;
        private ObjectList _objects;

        public ItemList Items => _items ?? (_items = new ItemList());
        public ObjectList Objects => _objects ?? (_objects = new ObjectList());

        public override void SetTile(Vector3Int position, GenericTile tile, Matrix4x4 transformMatrix)
        {
            var objectTile = tile as ObjectTile;

            if (objectTile)
            {
                if(!objectTile.IsItem)
                    tilemap.SetTile(position, null);
                objectTile.SpawnObject(position, tilemap, transformMatrix);
            }
            else
            {
                base.SetTile(position, tile, transformMatrix);
            }
        }

        public override void RemoveTile(Vector3Int position)
        {
            Items.Destroy(position);
            Objects.Destroy(position);
            base.RemoveTile(position);
        }

        public override bool IsPassableAt(Vector3Int origin, Vector3Int to)
        {
            if (Objects[to] && !Objects[to].IsPassable(origin))
            {
                return false;
            }
            
            return Objects[origin] && Objects[origin].restricted? Objects[origin].IsPassable(to) : base.IsPassableAt(origin, to);
        }

        public override bool IsPassableAt(Vector3Int position)
        {
            return Objects[position] ? Objects[position].IsPassable() : base.IsPassableAt(position);
        }

        public override bool IsAtmosPassableAt(Vector3Int position)
        {
            return Objects[position] ? Objects[position].IsAtmosPassable() : base.IsAtmosPassableAt(position);
        }

        public override bool IsSpaceAt(Vector3Int position)
        {
            return IsAtmosPassableAt(position) && base.IsSpaceAt(position);
        }
    }
}