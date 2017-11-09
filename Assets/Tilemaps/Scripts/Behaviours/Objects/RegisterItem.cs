using Tilemaps.Scripts.Behaviours.Layers;
using Tilemaps.Scripts.Utils;
using UnityEngine;

namespace Tilemaps.Scripts.Behaviours.Objects
{
    [ExecuteInEditMode]
    public class RegisterItem : MonoBehaviour
    {
        private ItemList _itemList;

        private Vector3Int position;

        private void Start()
        {
            position = Vector3Int.FloorToInt(transform.localPosition);
            _itemList = transform.parent.GetComponent<ObjectLayer>().Items;
            _itemList.Add(position, this);
        }
    
        private void OnEnable()
        {
            // In case of recompilation and Start doesn't get called
            _itemList?.Add(position, this);
        }

        public void OnDestroy()
        {
            _itemList?.Remove(position, this);
        }
    }
}