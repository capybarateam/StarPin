using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageAchievement
{
    public static void SetCleared(Stage stage, int clearLevel)
    {
        PlayerPrefs.SetInt($"stage.cleared.{stage.sceneName}", clearLevel);
    }

    public static bool IsCleared(Stage stage, int defaultClearLeve)
    {
        return GetCleared(stage, defaultClearLeve) != 0;
    }

    public static int GetCleared(Stage stage, int defaultClearLevel)
    {
        return PlayerPrefs.GetInt($"stage.cleared.{stage.sceneName}", defaultClearLevel);
    }

    public static void SetLastStage(string world, Stage lastStage)
    {
        PlayerPrefs.SetString($"stage.laststage.{world}", lastStage.sceneName);
    }

    public static string GetLastStageSceneName(string world)
    {
        if (!PlayerPrefs.HasKey("stage.laststage"))
            return null;
        return PlayerPrefs.GetString($"stage.laststage.{world}");
    }
}
