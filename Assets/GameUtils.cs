using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUtils
{
    public static void SetEnabled(string name, bool flag)
    {
        var obj = GameObject.Find(name);
        if (obj != null)
            obj.GetComponent<Animator>().SetBool("Enabled", flag);
    }
}
