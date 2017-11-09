using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using World.Behaviours;
using World.Utils;

namespace World
{
    [Serializable]
    public class MatrixNode
    {
        private int tileValue = 0;

//        private List<GameObject> structures = new List<GameObject>();
//
//        private List<ObjectBehaviour> items = new List<ObjectBehaviour>();
//
//        private List<HealthBehaviour> damageables = new List<HealthBehaviour>();
//
//        private List<ObjectBehaviour> players = new List<ObjectBehaviour>();

        private Dictionary<TileType, List<GameObject>> tiles = new Dictionary<TileType, List<GameObject>>();


        //Holds the details if tile is blocking movement in certain directions
//        private RestrictedMoveStruct restrictedMoveStruct;

        public bool TryAddTile(GameObject gameObject)
        {
            var registerTile = gameObject.GetComponent<RegisterTile>();
            if (!registerTile)
                return false;

            tiles[registerTile.TileType].Add(gameObject);

//            if (tileType == TileType.Player)
//            {
//                players.Add(gameObject.GetComponent<ObjectBehaviour>());
//            }
//            else if (tileType & TileProperty.Structure > 0)
//            {
//                structures.Add(gameObject);
//            }

//            if (tileType == TileType.RestrictedMovement)
//            {
//                restrictedMoveStruct = gameObject.GetComponent<RestrictiveMoveTile>().GetRestrictedData;
//                isRestrictiveTile = true;
//            }
            UpdateValues();

            return true;
        }

        public bool TryRemoveTile(GameObject gameObject)
        {
            var registerTile = gameObject.GetComponent<RegisterTile>();

            if (!tiles[registerTile.TileType].Contains(gameObject))
                return false;

            tiles[registerTile.TileType].Remove(gameObject);

            UpdateValues();
            return true;
        }

        public bool Contains(GameObject gameObject)
        {
            var registerTile = gameObject.GetComponent<RegisterTile>();

            return tiles[registerTile.TileType].Contains(gameObject);
        }

        public bool FitsTile(GameObject gameObject)
        {
            var registerTile = gameObject.GetComponent<RegisterTile>();
            return registerTile && (tileValue & registerTile.TileType) == 0;
        }
        
        public bool HasTileType(TileType tileType)
        {
            return tiles.ContainsKey(tileType) && tiles[tileType].Count > 0;
        }

        public List<GameObject> GetTiles()
        {
            return tiles.SelectMany(d => d.Value).ToList();
        }

        private void UpdateValues()
        {
            tileValue = 0;
            
            foreach (var tileType in tiles.Keys)
            {
                if(tiles[tileType].Count == 0)
                    continue;

                tileValue |= tileType;
            }
        }

//        public bool IsSpace()
//        {
//            return (tileValue & (int) (TileProperty.AtmosNotPassable | TileProperty.HasFloor)) == 0;
//        }
//
//        public bool IsPassable()
//        {
//            return (tileValue & (int) TileProperty.NotPassable) == 0;
//        }
//
//        public bool IsAtmosPassable()
//        {
//            return (tileValue & (int) TileProperty.AtmosNotPassable) == 0;
//        }
//
//        public bool IsDoor()
//        {
//            return false;
//        }
//
//        public bool IsWindow()
//        {
//            return false;
//        }
//
//        public bool IsWall()
//        {
//            return false;
//        }
//
//        public bool IsPlayer()
//        {
//            return false;
//        }
//
//        public bool IsObject()
//        {
//            return false;
//        }
//
//        public bool IsRestrictiveTile()
//        {
//            return false;
//        }

//        public bool IsEmpty()
//        {
//            return structures.Count == 0;
//        }


//        public DoorController GetDoor()
//        {
//            if (isDoor)
//            {
//                foreach (var tile in tiles)
//                {
//                    var registerTile = tile.GetComponent<RegisterTile>();
//                    if (registerTile.TileType == TileType.Door)
//                    {
//                        var doorControl = registerTile.gameObject.GetComponent<DoorController>();
//                        return doorControl;
//                    }
//                }
//            }
//            return null;
//        }

//        public RestrictedMoveStruct GetMoveRestrictions()
//        {
//            return restrictedMoveStruct;
//        }

//        public PushPull GetPushPull()
//        {
//            if (isObject)
//            {
//                foreach (var tile in tiles)
//                {
//                    var registerTile = tile.GetComponent<RegisterTile>();
//                    if (registerTile.TileType == TileType.Object)
//                    {
//                        var objCollisions = registerTile.gameObject.GetComponent<PushPull>();
//                        return objCollisions;
//                    }
//                }
//            }
//            return null;
//        }

//        public List<ObjectBehaviour> GetItems()
//        {
//            List<ObjectBehaviour> newList = new List<ObjectBehaviour>(items);
//            return newList;
//        }
//
//        public List<ObjectBehaviour> GetPlayers()
//        {
//            List<ObjectBehaviour> newList = new List<ObjectBehaviour>(players);
//            return newList;
//        }

//        public List<HealthBehaviour> GetDamageables()
//        {
//            return damageables;
//        }

//        public FloorTile GetFloorTile()
//        {
//            foreach (var tile in tiles)
//            {
//                var registerTile = tile.GetComponent<RegisterTile>();
//                if (registerTile.TileType == TileType.Floor)
//                {
//                    return registerTile.gameObject.GetComponent<FloorTile>();
//                }
//            }
//            return null;
//        }
    }
}