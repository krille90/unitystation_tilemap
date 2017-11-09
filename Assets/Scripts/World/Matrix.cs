using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using World.Utils;

namespace World {

    [Serializable]
    public class Matrix {
        private Matrix() { }
        
        private static NodeDictionary nodes = new NodeDictionary();

        public static MatrixNode At(Vector2 position, bool createIfNull = true) {
            return At(position.x, position.y);
        }

        public static MatrixNode At(float x, float y, bool createIfNull = true) {
            return At(Mathf.RoundToInt(x), Mathf.RoundToInt(y), createIfNull);
        }

        public static MatrixNode At(int x, int y, bool createIfNull = true) {
            if(nodes.ContainsKey(x, y)) {
                return nodes[x, y];
            } 
            
            if(createIfNull) {
                nodes[x, y] = new MatrixNode();
                return nodes[x, y];
            }

            return null;
        }

        public static List<MatrixNode> At(Vector2 position, int radius, bool createIfNull = true)
        {
            return At(position.x, position.y, radius, createIfNull);
        }

        public static List<MatrixNode> At(float x, float y, int radius, bool createIfNull = true)
        {
            var list = new List<MatrixNode>();

            for (float xr = x - radius; xr <= x + radius; xr++)
            {
                for (float yr = y - radius; yr <= y + radius; yr++)
                {
                    var node = At(xr, yr, createIfNull);

                    if (node != null)
                    {
                        list.Add(node);
                    }
                }  
            }

            return list;
        }
    }
}