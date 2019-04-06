using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroIndicatorTest : MonoBehaviour
{
    public GameObject HeroIndicator;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!HeroIndicator.activeSelf)
            {
                HeroIndicator.SetActive(true);
            }
            else
            {
                HeroIndicator.GetComponentInChildren<Animator>().Play("HeroAnimation");
            }
        }
    }
}
