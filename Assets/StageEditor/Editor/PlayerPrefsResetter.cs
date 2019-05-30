using UnityEngine;
using UnityEditor;

/// <summary>
/// PlayerPrefsを初期化するクラス
/// </summary>
public static class PlayerPrefsResetter
{

    [MenuItem("ステージ作成/セーブ削除")]
    public static void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        Debug.Log("セーブデータを全削除しました。");
    }
}