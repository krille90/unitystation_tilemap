using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Tilemaps.Scripts.Tiles;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Tilemaps.Scripts.Utils
{
    public static class PreviewSpriteBuilder
    {
        private const string previewPath = "Assets/Textures/Resources/TilePreviews/";
        
        public static Sprite LoadSprite(Object obj)
        {
            return AssetDatabase.LoadAssetAtPath<Sprite>(GetFilePath(obj.name));
        }

        public static void DeleteSprite(Object obj)
        {
            AssetDatabase.DeleteAsset(GetFilePath(obj.name));
        }
        
        public static Sprite Create(GameObject gameObject)
        {
            if (gameObject == null)
                return null;

            var sprites = GetObjectSprites(gameObject);

            var sprite = MergeSprites(sprites);

            return SaveSpriteToEditorPath(sprite, gameObject.name);
        }

        public static Sprite Create(MetaTile metaTile)
        {
            if (metaTile == null)
                return null;
            
            List<Sprite> sprites = new List<Sprite>();

            foreach (var tile in metaTile.GetTiles())
            {
                sprites.Add(tile.PreviewSprite);
            }
            
            var sprite = MergeSprites(sprites);

            return SaveSpriteToEditorPath(sprite, metaTile.name);
        }

        private static IReadOnlyList<Sprite> GetObjectSprites(GameObject gameObject)
        {
            List<Sprite> sprites = new List<Sprite>();

            if (gameObject != null)
            {
                var renderers = gameObject.GetComponentsInChildren<SpriteRenderer>(true).ToList();

                if (renderers.Count > 0)
                {
                    renderers.Sort(RendererComparer.Compare);

                    foreach (var r in renderers)
                    {
                        sprites.Add(r.sprite);
                    }
                }
            }

            return sprites;
        }

        private static Sprite MergeSprites(IReadOnlyList<Sprite> sprites)
        {
            var colors = new Color[1024];
            foreach (var s in sprites)
            {
                var rect = s.rect;
                var pixels = s.texture.GetPixels((int) rect.x, (int) rect.y, (int) rect.width, (int) rect.height);

                for (int i = 0; i < pixels.Length; i++)
                {
                    var px = pixels[i];

                    if (px.a > 0)
                    {
                        colors[i] = colors[i] * (1 - px.a) + px * px.a;
                    }
                }
            }

            var old = sprites[0];
            var texture = new Texture2D((int) old.rect.width, (int) old.rect.height, old.texture.format, false);

            texture.SetPixels(colors);
            texture.Apply();

            return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), old.pixelsPerUnit);
        }

        private static Sprite SaveSpriteToEditorPath(Sprite sprite, string filename)
        {
            var path = GetFilePath(filename);
            
            var dir = Path.GetDirectoryName(path);

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
            textureImporter.isReadable = true;

            EditorUtility.SetDirty(textureImporter);
            textureImporter.SaveAndReimport();

            return AssetDatabase.LoadAssetAtPath<Sprite>(path);
        }

        private static string GetFilePath(string filename)
        {
            return previewPath + filename + ".png";
        }
    }
}