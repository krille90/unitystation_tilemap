using System.Collections.Generic;

namespace World.Utils
{
    public class Type
    {
        protected int value;
        public string Name { get; }

        public Type(string name, int value)
        {
            Name = name;
            this.value = value;
        }

        public static implicit operator int(Type type)
        {
            return type?.value ?? -1;
        }
    }

    [System.Flags]
    public enum TileProperty
    {
        HasFloor = 1,
        AtmosNotPassable = 2,
        NotPassable = 4,
        Structure = 8,
        PassableRestrictions = 16,
        IsItem = 128
    }

    public class TileType : Type
    {
        private static List<TileType> tileTypes = new List<TileType>();
        public static IList<TileType> List => tileTypes.AsReadOnly();

        public static TileType None   = new TileType("None");
        public static TileType Floor  = new TileType("Floor", (int) (TileProperty.HasFloor | TileProperty.Structure));
        public static TileType Object = new TileType("Object", (int) TileProperty.NotPassable);
        public static TileType Door   = new TileType("Door", (int) (TileProperty.AtmosNotPassable | TileProperty.NotPassable | TileProperty.Structure));
        public static TileType Window = new TileType("Window", (int) (TileProperty.AtmosNotPassable | TileProperty.NotPassable | TileProperty.Structure));
        public static TileType Wall   = new TileType("Wall", (int) (TileProperty.HasFloor | TileProperty.AtmosNotPassable | TileProperty.NotPassable | TileProperty.Structure));
        public static TileType Item   = new TileType("Item", (int) TileProperty.IsItem);
        public static TileType Player = new TileType("Player", (int) TileProperty.NotPassable);

        //this is for tiles with glass that prevents movement in some of the directions
        public static TileType RestrictedMovement = new TileType("RestrictedMovement", (int) TileProperty.PassableRestrictions);

        public TileType(string name, int value = 0) : base(name, value)
        {
            tileTypes.Add(this);
        }

        public bool HasProperty(TileProperty property)
        {
            return (value & (int) property) > 0;
        }
    }
}