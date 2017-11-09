using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using NUnit.Framework.Constraints;
using Tilemaps.Scripts.Behaviours.Objects;
using UnityEngine;

namespace Tilemaps.Scripts.Utils
{
    public class ItemList
    {
        private Dictionary<Vector3Int, List<RegisterItem>> items = new Dictionary<Vector3Int, List<RegisterItem>>();

        public void Add(Vector3Int position, RegisterItem item)
        {
            if (!items.ContainsKey(position))
            {
                items[position] = new List<RegisterItem>();
            }

            if (!items[position].Contains(item))
            {
                items[position].Add(item);
            }
        }

        public void RemoveAll(Vector3Int position)
        {
            if (items.ContainsKey(position))
            {
                items.Remove(position);
            }
        }

        public void DestroyAll(Vector3Int position)
        {
            if (items.ContainsKey(position))
            {
                foreach (var item in items[position].ToArray())
                {
                    Object.DestroyImmediate(item.gameObject);
                }
                RemoveAll(position);
            }
        }

        public void Remove(Vector3Int position, RegisterItem item)
        {
            if (items.ContainsKey(position))
            {
                items[position].Remove(item);
            }
        }

        public RegisterItem[] GetItems(Vector3Int position)
        {
            if (!items.ContainsKey(position))
            {
                items[position] = new List<RegisterItem>();
            }
            return items[position].ToArray();
        }
    }
}