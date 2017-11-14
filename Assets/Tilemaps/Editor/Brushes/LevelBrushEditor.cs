using System.Linq;
using Tilemaps.Editor.Brushes.Utils;
using Tilemaps.Scripts.Behaviours.Layers;
using Tilemaps.Scripts.Tiles;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tilemaps.Editor.Brushes
{
    [CustomEditor(typeof(LevelBrush))]
    public class LevelBrushEditor : GridBrushEditor
    {
        private MetaTileMap currentPreviewTilemap;
        
        public override void RegisterUndo(GameObject layer, GridBrushBase.Tool tool)
        {
            foreach (var tilemap in layer.GetComponentsInChildren<Tilemap>())
            {
                Undo.RegisterCompleteObjectUndo(tilemap, "Paint");
            }
        }

        public override void PaintPreview(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
        {
            var metaTilemap = brushTarget.GetComponent<MetaTileMap>();
            
            var tile = this.brush.cells[0].tile;

            if (tile is LayerTile)
            {
                SetPreviewTile(metaTilemap, position, (LayerTile) tile);
            }
            else if (tile is MetaTile)
            {
                foreach (var layerTile in ((MetaTile)tile).GetTiles())
                {
                    SetPreviewTile(metaTilemap, position, layerTile);
                }
            }
            
            currentPreviewTilemap = metaTilemap;
        }

        public override void ClearPreview()
        {
            if (currentPreviewTilemap)
            {
                currentPreviewTilemap.ClearPreview();
            }
        }

        private static void SetPreviewTile(MetaTileMap metaTilemap, Vector3Int position, LayerTile tile)
        {
            if (tile is ObjectTile)
            {
                var previewTile = ScriptableObject.CreateInstance<PreviewTile>();
                previewTile.ReferenceTile = tile;
                metaTilemap.SetPreviewTile(position, previewTile);
            }
            else
            {
                metaTilemap.SetPreviewTile(position, tile);
            }
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