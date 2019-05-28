using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStar : MonoBehaviour
{
    public GameObject level1;
    public GameObject level2;
    public GameObject level3;

    public void SetLevel(int level)
    {
        level1.SetActive(level > 0);
        level2.SetActive(level > 1);
        level3.SetActive(level > 2);
    }
}
