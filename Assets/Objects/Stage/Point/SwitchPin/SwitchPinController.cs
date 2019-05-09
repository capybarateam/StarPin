using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPinController : MonoBehaviour
{
    // スターの管理変数
    StarController starController;

    // スイッチを変えるフラグ
    bool isOffFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        starController = GameObject.Find("Player").GetComponentInChildren<StarController>();
    }

    // Update is called once per frame
    void Update()
    {
        // トリガーの更新
        if (this.GetComponentInChildren<PointController>().touched && this.gameObject != starController.currentJoint)
        {
            isOffFlag = true;
        }

        // スイッチを押す
        if (isOffFlag && (this.gameObject == starController.currentJoint))
        {
            isOffFlag = false;
            this.GetComponentInChildren<PointController>().touched = false;
        }
    }
}
