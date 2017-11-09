using Tilemaps.Scripts.Behaviours.Layers;
using Tilemaps.Scripts.Utils;
using UnityEngine;

namespace Tilemaps.Scripts.Behaviours.Objects
{
    [ExecuteInEditMode]
    public class RegisterObject : MonoBehaviour
    {
        private ObjectList _objectList;

        protected Vector3Int position;

        private void Start()
        {
            position = Vector3Int.FloorToInt(transform.localPosition);

            _objectList = transform.parent.GetComponent<ObjectLayer>().Objects;

            _objectList.Replace(position, this);
        }

        public void OnDestroy()
        {
            _objectList?.Remove(position);
        }

        public virtual bool IsPassable(Vector3Int from)
        {
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