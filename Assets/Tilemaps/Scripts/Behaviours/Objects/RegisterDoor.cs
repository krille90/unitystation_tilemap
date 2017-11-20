using Tilemaps.Scripts.Utils;
using UnityEngine;

namespace Tilemaps.Scripts.Behaviours.Objects
{
    [ExecuteInEditMode]
    public class RegisterDoor : RegisterObject
    {
        public bool closed = true;

        public override bool IsPassable(Vector3Int origin)
        {
            return !closed || base.IsPassable(origin);
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