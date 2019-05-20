using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class EnergyUI : MonoBehaviour
{
    StarController starController;
    TextMeshPro text;

    // Start is called before the first frame update
    void Start()
    {
        starController = GameObject.Find("Player").GetComponentInChildren<StarController>();
        text = this.GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        var manager = GameDirector.Get(transform)?.pointManager;
        if (manager != null)
        {
            text.text = Convert.ToString(manager.health);
        }
    }
}
