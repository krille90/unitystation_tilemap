using System.Collections.Generic;
using Tilemaps.Scripts.Behaviours.Objects;
using UnityEngine;

namespace Tilemaps.Scripts.Utils
{
    public class ObjectList
    {
        private Dictionary<Vector3Int, RegisterObject> _objects = new Dictionary<Vector3Int, RegisterObject>();
        
        public void Remove(Vector3Int position)
        {
            _objects.Remove(position);
        }

        public void Destroy(Vector3Int position)
        {
            if (_objects.ContainsKey(position))
            {
                Object.DestroyImmediate(_objects[position].gameObject);
                Remove(position);
            }
        }

        public void Replace(Vector3Int position, RegisterObject value)
        {
            Destroy(position);
            _objects[position] = value;
        }
        
        public RegisterObject this[Vector3 position]
        {
            get { return this[Vector3Int.FloorToInt(position)]; }
            
            set { this[Vector3Int.FloorToInt(position)] = value; }
        } 
        
        public RegisterObject this[Vector3Int position] 
        {
            get
            {
                return !_objects.ContainsKey(position) ? null : _objects[position];
            }

            set
            {
                _objects[position] = value;
            }
        } 
    }
}