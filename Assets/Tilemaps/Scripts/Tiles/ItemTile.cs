using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using World.Utils;

namespace Tilemaps.Scripts.Tiles
{
    [Serializable]
    public class ItemTile : ObjectTile
    {
        public void SpawnItem(Vector3Int position, Tilemap tilemap)
        {
            var go = Instantiate(Object);

            go.SetActive(false);
            go.transform.parent = tilemap.transform;
            go.transform.localPosition = position + new Vector3(0.5f, 0.5f, 0);
            go.transform.rotation = tilemap.transform.rotation;
            go.name = Object.name;
            go.SetActive(true);
        }
    }
}