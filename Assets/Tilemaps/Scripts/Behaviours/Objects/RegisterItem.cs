using Tilemaps.Scripts.Behaviours.Layers;
using Tilemaps.Scripts.Utils;
using UnityEngine;

namespace Tilemaps.Scripts.Behaviours.Objects
{
    [ExecuteInEditMode]
    public class RegisterItem : MonoBehaviour
    {
        private ObjectLayer layer;

        private Vector3Int position;

        private void Start()
        {
            position = Vector3Int.FloorToInt(transform.localPosition);
            layer = transform.parent.GetComponent<ObjectLayer>();
            layer.Items.Add(position, this);
        }
    
        private void OnEnable()
        {
            // In case of recompilation and Start doesn't get called
            layer?.Items.Add(position, this);
        }

        public void OnDestroy()
        {
            layer?.Items.Remove(position, this);
        }
    }
}