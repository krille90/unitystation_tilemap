using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Tilemaps.Scripts.Utils
{
    public static class PreviewSpriteBuilding
    {
        public static Sprite Create(GameObject gameObject)
        {
            if (gameObject == null)
                return null;

            var renderers = gameObject.GetComponentsInChildren<SpriteRenderer>().ToList();

            if (renderers.Count == 0)
                return null;

            renderers.Sort(RendererComparer.Compare);

            var colors = new Color[1024];
            foreach (var r in renderers)
            {
                var rect = r.sprite.rect;
                var pixels = r.sprite.texture.GetPixels((int) rect.x, (int) rect.y, (int) rect.width, (int) rect.height);

                for (int i = 0; i < pixels.Length; i++)
                {
                    var px = pixels[i];

                    if (px.a > 0)
                    {
                        colors[i] = colors[i] * (1 - px.a) + px * px.a;
                    }
                }
            }

            var old = renderers[0].sprite;
            var texture = new Texture2D((int) old.rect.width, (int) old.rect.height, old.texture.format, false);

            texture.SetPixels(colors);
            texture.Apply();

            var sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), old.pixelsPerUnit);

            return SaveSpriteToEditorPath(sprite, "Assets/Textures/Resources/TilePreviews/" + gameObject.name + ".png");
        }

        private static Sprite SaveSpriteToEditorPath(Sprite sprite, string path)
        {
            string dir = Path.GetDirectoryName(path);

            if (dir == null)
            {
                return null;
            }

            Directory.CreateDirectory(dir);

            File.WriteAllBytes(path, sprite.texture.EncodeToPNG());

            AssetDatabase.Refresh();
            AssetDatabase.AddObjectToAsset(sprite, path);
            AssetDatabase.SaveAssets();

            var textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;

            if (textureImporter == null)
            {
                return null;
            }

            textureImporter.spritePixelsPerUnit = sprite.pixelsPerUnit;
            textureImporter.mipmapEnabled = false;
            textureImporter.textureCompression = TextureImporterCompression.Uncompressed;
            textureImporter.filterMode = FilterMode.Point;

            EditorUtility.SetDirty(textureImporter);
            textureImporter.SaveAndReimport();

            return AssetDatabase.LoadAssetAtPath<Sprite>(path);
        }
    }
}