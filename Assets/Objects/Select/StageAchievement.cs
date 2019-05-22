using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageAchievement
{
    public static void SetCleared(Stage stage, bool cleared)
    {
        PlayerPrefs.SetInt($"stage.cleared.{stage.stageName}", cleared ? 1 : 0);
    }

    public static bool GetCleared(Stage stage, bool defaultCleared)
    {
        return PlayerPrefs.GetInt($"stage.cleared.{stage.stageName}", defaultCleared ? 1 : 0) != 0;
    }
}
