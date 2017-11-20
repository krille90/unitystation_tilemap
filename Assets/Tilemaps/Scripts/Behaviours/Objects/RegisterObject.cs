using Tilemaps.Scripts.Behaviours.Layers;
using Tilemaps.Scripts.Utils;
using UnityEngine;

namespace Tilemaps.Scripts.Behaviours.Objects
{
    [ExecuteInEditMode]
    public class RegisterObject : MonoBehaviour
    {
        private ObjectLayer layer;

        protected Vector3Int position;

        public bool restricted;

        private void Start()
        {
            position = Vector3Int.FloorToInt(transform.localPosition);
            layer = transform.parent.GetComponent<ObjectLayer>();
            
            layer.Objects.Replace(position, this);
        }
    
        private void OnEnable()
        {
            // In case of recompilation and Start doesn't get called
            layer?.Objects.Replace(position, this);
        }

        public void OnDestroy()
        {
            layer?.Objects.Remove(position);
        }
        
        public virtual bool IsPassable(Vector3Int origin)
        {
            if (restricted)
            {
                var v = Vector3Int.RoundToInt(transform.localRotation * Vector3.down);
                
                return !(origin-position).Equals(v);
            }
            
            return IsPassable();
        }

        public virtual bool IsPassable()
        {
            return true;
        }

        public virtual bool IsAtmosPassable()
        {
            return true;
        }
    }
}