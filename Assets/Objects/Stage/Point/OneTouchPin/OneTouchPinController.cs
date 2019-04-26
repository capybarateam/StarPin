﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneTouchPinController : MonoBehaviour
{
    // スターの管理変数
    StarController starController;

    // Start is called before the first frame update
    void Start()
    {
        starController = GameObject.Find("StarObject").GetComponent<StarController>();
    }

    // Update is called once per frame
    void Update()
    {
        // 触れていてスターが離れた場合
        if (this.gameObject != starController.currentJoint && this.GetComponent<PointController>().touched)
        {
            Destroy(this.gameObject);
        }
    }
}