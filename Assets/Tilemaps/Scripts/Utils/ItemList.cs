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
        private Dictionary<Vector3Int, List<RegisterItem>> _items = new Dictionary<Vector3Int, List<RegisterItem>>();

        public void Add(Vector3Int position, RegisterItem item)
        {
            if (!_items.ContainsKey(position))
            {
                _items[position] = new List<RegisterItem>();
            }

            if (!_items[position].Contains(item))
            {
                _items[position].Add(item);
            }
        }

        public void Remove(Vector3Int position)
        {
            if (_items.ContainsKey(position))
            {
                _items.Remove(position);
            }
        }

        public void Destroy(Vector3Int position)
        {
            if (_items.ContainsKey(position))
            {
                foreach (var item in _items[position].ToArray())
                {
                    Object.DestroyImmediate(item.gameObject);
                }
                Remove(position);
            }
        }

        public void Remove(Vector3Int position, RegisterItem item)
        {
            if (_items.ContainsKey(position))
            {
                _items[position].Remove(item);
            }
        }

        public RegisterItem[] GetItems(Vector3Int position)
        {
            if (!_items.ContainsKey(position))
            {
                _items[position] = new List<RegisterItem>();
            }
            return _items[position].ToArray();
        }
    }
}