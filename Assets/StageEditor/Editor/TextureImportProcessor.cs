using UnityEditor;
using UnityEngine;

public class TextureImportProcessor : AssetPostprocessor
{
    // インポート設定のデフォルト値をインポート前に変更可能
    public void OnPreprocessTexture()
    {
        // 新規に読み込まれた場合のみ
        var tImporter = AssetDatabase.LoadAssetAtPath<Texture2D>(assetPath);
        if (tImporter == null)
        {
            // assetImporterがインポート設定を持っている
            var importer = assetImporter as TextureImporter;

            // タイプを変更
            importer.textureType = TextureImporterType.Sprite;
        }
    }
}