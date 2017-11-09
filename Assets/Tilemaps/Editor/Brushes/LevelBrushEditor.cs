using System.Linq;
using Tilemaps.Editor.Brushes;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tilemaps.Editor
{
    [CustomEditor(typeof(LevelBrush))]
    public class LevelBrushEditor : GridBrushEditor
    {
        public override void RegisterUndo(GameObject layer, GridBrushBase.Tool tool)
        {
            foreach (var tilemap in layer.GetComponentsInChildren<Tilemap>())
            {
                Undo.RegisterCompleteObjectUndo(tilemap, "Paint");
            }
        }

        public override void PaintPreview(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
        {
            // TODO paint proper preview
        }

        public override GameObject[] validTargets
        {
            get
            {
                Grid[] grids = FindObjectsOfType<Grid>();

                return grids?.Select(x => x.gameObject).ToArray();
            }
        }
    }
}