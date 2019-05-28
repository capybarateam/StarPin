using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpoilerToggle : MonoBehaviour
{
    public void SetSpoiler(bool expand)
    {
        GetComponent<Animator>().SetBool("Expand", expand);
    }

    public void ToggleSpoiler()
    {
        bool expanded = GetComponent<Animator>().GetBool("Expand");
        SetSpoiler(!expanded);
    }
}
