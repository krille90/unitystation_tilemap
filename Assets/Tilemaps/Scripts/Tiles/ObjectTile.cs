using System;
using Tilemaps.Scripts.Behaviours.Layers;
using Tilemaps.Scripts.Behaviours.Objects;
using Tilemaps.Scripts.Utils;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tilemaps.Scripts.Tiles
{
    [Serializable]
    public class ObjectTile: LayerTile
    {
        public GameObject Object;

        private GameObject _objectCurrent;

        public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
        {
            if (go)
            {
                go.hideFlags = HideFlags.None;
                go.transform.position += new Vector3(0.5f, 0.5f, 0);
                go.name = Object.name;
            }

            return base.StartUp(position, tilemap, go);
        }

        public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        {
            if (tilemap.GetComponent<Tilemap>().name == "Layer1")
            {
                base.GetTileData(position, tilemap, ref tileData);
                return;
            }

            tileData.sprite = null;
            tileData.gameObject = Object;
            tileData.flags = TileFlags.LockAll;
            tileData.colliderType = Tile.ColliderType.None;
        }

        private void OnValidate()
        {
            if (Object != null && Object != _objectCurrent)
            {
                EditorApplication.delayCall+=()=>
                {
                    PreviewSprite = PreviewSpriteBuilding.Create(Object);
                };
            }
            _objectCurrent = Object;
        }
        
        public override bool IsPassableAt(Vector3Int from, Vector3Int to, Tilemap tilemap)
        {
            var obj = GetObjectAt(to, tilemap);
            return  obj.IsPassable(from);
        }
        
        public override bool IsPassableAt(Vector3Int position, Tilemap tilemap)
        {
            var obj = GetObjectAt(position, tilemap);
            return  obj.IsPassable();
        }

        public override bool IsAtmosPassableAt(Vector3Int position, Tilemap tilemap)
        {
            var obj = GetObjectAt(position, tilemap);
            return  obj.IsAtmosPassable();
        }

        public override bool IsSpaceAt(Vector3Int position, Tilemap tilemap)
        {
            return true;
        }

        private static RegisterObject GetObjectAt(Vector3Int position, Tilemap tilemap)
        {
            var objectList = tilemap.GetComponent<ObjectLayer>().Objects;

            return objectList?[position];
        }
    }
}