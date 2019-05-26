using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageAchievement
{
    public static void SetCleared(IStage stage, int clearLevel)
    {
        PlayerPrefs.SetInt($"stage.cleared.{stage.SceneName}", clearLevel);
    }

    public static bool IsCleared(IStage stage, int defaultClearLeve)
    {
        return GetCleared(stage, defaultClearLeve) != 0;
    }

    public static int GetCleared(IStage stage, int defaultClearLevel)
    {
        return PlayerPrefs.GetInt($"stage.cleared.{stage.SceneName}", defaultClearLevel);
    }

    public static void SetLastStage(string world, IStage lastStage)
    {
        PlayerPrefs.SetString($"stage.laststage.{world}", lastStage.SceneName);
    }

    public static string GetLastStageSceneName(string world)
    {
        if (!PlayerPrefs.HasKey("stage.laststage"))
            return null;
        return PlayerPrefs.GetString($"stage.laststage.{world}");
    }
}
