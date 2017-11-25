using System.Runtime.Remoting;
using UnityEngine;

namespace Tilemaps.Scripts.Behaviours.Objects
{
    [ExecuteInEditMode]
    public class RegisterObject : RegisterTile
    {
        //[HideInInspector]
        public Vector3Int Offset = Vector3Int.zero;
        
        protected override void OnAddTile(Vector3Int oldPosition, Vector3Int newPosition)
        {
            var obj = layer?.Objects.GetFirst<RegisterObject>(newPosition);

            if (obj != null)
            {
                DestroyImmediate(obj.gameObject);
            }
            
            base.OnAddTile(oldPosition, newPosition);
        }
    }
}