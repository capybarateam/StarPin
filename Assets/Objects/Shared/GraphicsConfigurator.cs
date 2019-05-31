using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicsConfigurator : MonoBehaviour
{
    void Awake()
    {
        // PC向けビルドだったらサイズ変更
        if (Application.platform == RuntimePlatform.WindowsPlayer ||
            Application.platform == RuntimePlatform.OSXPlayer ||
            Application.platform == RuntimePlatform.LinuxPlayer)
        {
            Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
        }
        QualitySettings.SetQualityLevel(QualitySettings.names.Length - 1, true);
    }
}