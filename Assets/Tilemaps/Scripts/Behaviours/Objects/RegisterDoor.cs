using Tilemaps.Scripts.Utils;
using UnityEngine;

namespace Tilemaps.Scripts.Behaviours.Objects
{
    [ExecuteInEditMode]
    public class RegisterDoor : RegisterObject
    {
        public bool closed = true;

        public bool south;
        
        public override bool IsPassable(Vector3Int from)
        {
            if (south)
            {
                return !(position + Vector3Int.down).Equals(from);
            }
            
            return base.IsPassable(from);
        }
        
        public override bool IsPassable()
        {
            return !closed;
        }

        public override bool IsAtmosPassable()
        {
            return !closed;
        }
    }
}