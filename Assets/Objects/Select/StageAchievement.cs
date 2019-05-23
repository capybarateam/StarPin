using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageAchievement
{
    public static void SetCleared(Stage stage, int clearLevel)
    {
        PlayerPrefs.SetInt($"stage.cleared.{stage.stageName}", clearLevel);
    }

    public static bool IsCleared(Stage stage, int defaultClearLeve)
    {
        return GetCleared(stage, defaultClearLeve) != 0;
    }

    public static int GetCleared(Stage stage, int defaultClearLevel)
    {
        return PlayerPrefs.GetInt($"stage.cleared.{stage.stageName}", defaultClearLevel);
    }
}
