using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    void Start()
    {
        this.Delay(.1f, () =>
        {
            if (transform.childCount > 0)
                transform.GetChild(0).GetComponentInChildren<Selectable>().Select();
        });
    }

    public void SetVisible(bool starting)
    {
        foreach (Transform obj in transform)
        {
            var anim = obj.GetComponent<Animator>();
            if (anim != null)
                anim.SetBool("Enabled", starting);
        }
    }
}
