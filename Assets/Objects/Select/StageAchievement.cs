using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageAchievement
{
    public static bool isCreativeMode;

    public static void SetCleared(IStage stage, int clearLevel, bool force = false)
    {
        if (isCreativeMode)
            return;
        if (!force)
            clearLevel = Mathf.Max(clearLevel, GetCleared(stage, 0));
        PlayerPrefs.SetInt($"stage.cleared.{stage.SceneName}", clearLevel);
    }

    public static bool IsCleared(IStage stage, int defaultClearLeve)
    {
        if (isCreativeMode)
            return true;
        return GetCleared(stage, defaultClearLeve) != 0;
    }

    public static int GetCleared(IStage stage, int defaultClearLevel)
    {
        if (isCreativeMode)
            return 3;
        return PlayerPrefs.GetInt($"stage.cleared.{stage.SceneName}", defaultClearLevel);
    }

    public static void SetLastStage(string world, IStage lastStage)
    {
        PlayerPrefs.SetString($"stage.laststage.{(isCreativeMode ? "custom" : "story")}.{world}", lastStage.SceneName);
    }

    public static string GetLastStageSceneName(string world)
    {
        if (!PlayerPrefs.HasKey($"stage.laststage.{(isCreativeMode ? "custom" : "story")}.{world}"))
            return null;
        return PlayerPrefs.GetString($"stage.laststage.{(isCreativeMode ? "creative" : "story")}.{world}");
    }
}
