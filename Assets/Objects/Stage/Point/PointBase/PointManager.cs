using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PointManager : MonoBehaviour
{
    public List<PointController> allPoints;
    public List<PointController> allImportantPoints;

    public int maxHealth = 8;
    public int health;

    public int colorIndex;

    private void Start()
    {
        health = maxHealth;
    }

    public bool IsGotAllPoints()
    {
        return allPoints.All(e => e.rawTouched);
    }

    public bool IsGotAllImportantPoints()
    {
        return allImportantPoints.All(e => e.touched);
    }

    public void RegisterPoint(PointController point, bool important)
    {
        if (!allPoints.Contains(point))
        {
            allPoints.Add(point);
            if (important)
                allImportantPoints.Add(point);
        }
    }
}
