using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeEffectController : MonoBehaviour
{
    // スターの管理変数
    private StarController starController;

    private int lastPoint = 0;

    // Start is called before the first frame update
    void Start()
    {
        starController = GameObject.Find("Player").GetComponentInChildren<StarController>();
        lastPoint = this.gameObject.GetComponent<PointController>().currentPoint;
    }

    // Update is called once per frame
    void Update()
    {
        int nowPoint = this.gameObject.GetComponent<PointController>().currentPoint;

        // スイッチを押す
        if (nowPoint != lastPoint && (this.gameObject == starController.currentJoint))
        {
            this.GetComponent<ParticleSystem>().Play();
            this.GetComponent<AudioSource>().Play();
            this.GetComponent<TargetParticle>().Target = starController.gameObject.transform;
        }

        lastPoint = nowPoint;
    }
}
