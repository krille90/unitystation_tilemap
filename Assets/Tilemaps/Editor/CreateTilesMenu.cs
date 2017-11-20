using System.IO;
using Tilemaps.Scripts.Tiles;
using UnityEditor;
using UnityEngine;

namespace Tilemaps.Editor
{
    public class CreateTilesMenu : MonoBehaviour
    {
        #region Structures

        [MenuItem("Assets/Create/Tiles/Connected/Wall", false, 0)]
        public static void CreateWallConnected()
        {
            var tile = CreateTile<ConnectedTile>(LayerType.Structures);
            tile.texturePath = "Walls";
            tile.connectCategory = ConnectCategory.Walls;
            tile.connectType = ConnectType.ToSameCategory;

            CreateAsset(tile, "WallTile");
        }

        [MenuItem("Assets/Create/Tiles/Connected/Window", false, 0)]
        public static void CreateWindow()
        {
            var tile = CreateTile<ConnectedTile>(LayerType.Structures);
            tile.texturePath = "Windows";
            tile.connectCategory = ConnectCategory.Windows;
            tile.connectType = ConnectType.ToSameCategory;

            CreateAsset(tile, "WindowTile");
        }

        #endregion

        #region Objects
        
        [MenuItem("Assets/Create/Tiles/Simple Object", false, 0)]
        public static void CreateSimpleObject()
        {
            CreateTile<SimpleTile>(LayerType.Objects, "SimpleObjectTile");
        }

        [MenuItem("Assets/Create/Tiles/Object", false, 0)]
        public static void CreateObject()
        {
            CreateTile<ObjectTile>(LayerType.Objects, "ObjectTile");
        }

        [MenuItem("Assets/Create/Tiles/Door", false, 0)]
        public static void CreateDoor()
        {
            var tile = CreateTile<ObjectTile>(LayerType.Objects);

            CreateAsset(tile, "DoorTile");
        }

        [MenuItem("Assets/Create/Tiles/Connected/Table", false, 0)]
        public static void CreateTable()
        {
            var tile = CreateTile<ConnectedTile>(LayerType.Objects);
            tile.texturePath = "Tables";
            tile.connectCategory = ConnectCategory.Tables;
            tile.connectType = ConnectType.ToSameCategory;

            CreateAsset(tile, "TableTile");
        }

        [MenuItem("Assets/Create/Tiles/Item", false, 0)]
        public static void CreateItem()
        {
            var tile = CreateTile<ObjectTile>(LayerType.Objects);

            CreateAsset(tile, "ItemTile");
        }

        #endregion

        #region Floors

        [MenuItem("Assets/Create/Tiles/Floor", false, 0)]
        public static void CreateFloor()
        {
            CreateTile<SimpleTile>(LayerType.Floors, "FloorTile");
        }

        [MenuItem("Assets/Create/Tiles/Connected/Floor", false, 0)]
        public static void CreateFloorConnected()
        {
            var tile = CreateTile<ConnectedTile>(LayerType.Floors);
            tile.texturePath = "Floors";
            tile.connectCategory = ConnectCategory.Floors;

            CreateAsset(tile, "FloorTile");
        }

        #endregion

        [MenuItem("Assets/Create/Tiles/MetaTile", false, 0)]
        public static void CreateMetaTile()
        {
            var tile = ScriptableObject.CreateInstance<MetaTile>();
            CreateAsset(tile, "MetaTile");
        }

        private static void CreateTile<T>(LayerType layer, string tileName) where T : LayerTile
        {
            var tile = ScriptableObject.CreateInstance<T>();
            tile.LayerType = layer;

            CreateAsset(tile, tileName);
        }

        private static T CreateTile<T>(LayerType layer) where T : LayerTile
        {
            var tile = ScriptableObject.CreateInstance<T>();
            tile.LayerType = layer;
            return tile;
        }

        private static void CreateAsset(Object asset, string tileName)
        {
            var assetPath = Path.Combine(GetPath(), tileName + ".asset");
            AssetDatabase.CreateAsset(asset, assetPath);
        }

        private static string GetPath()
        {
            string path = AssetDatabase.GetAssetPath(Selection.activeObject);

            if (string.IsNullOrEmpty(path))
            {
                path = "Assets";
            }
            else if (Path.GetExtension(path) != "")
            {
                path = path.Replace(Path.GetFileName(path), "");
            }

            return path;
        }
    }
}