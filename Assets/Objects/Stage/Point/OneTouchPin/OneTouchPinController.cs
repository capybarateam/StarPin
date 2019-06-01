using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneTouchPinController : MonoBehaviour
{
    // スターの管理変数
    StarController starController;

    List<Rigidbody2D> renderObjectRigidbodys = new List<Rigidbody2D>();

    private bool flag = true;

    // Start is called before the first frame update
    void Start()
    {
        starController = StarController.latestStar == null ? null : StarController.latestStar.GetComponentInChildren<StarController>();
        foreach (Renderer render in this.GetComponentsInChildren<Renderer>())
        {
            Rigidbody2D rigid = render.gameObject.GetComponent<Rigidbody2D>();
            if (rigid)
            {
                rigid.gravityScale = 0.0f;
                renderObjectRigidbodys.Add(rigid);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 触れていてスターが離れた場合
        if (this.gameObject != starController?.currentJoint && (this.GetComponentInChildren<PointController>()?.touched ?? false))
        {
            if (flag)
            {
                GetComponent<AudioSource>().Play();
                flag = false;
            }

            this.GetComponentInChildren<CircleCollider2D>().enabled = false;
            foreach (Rigidbody2D rigid in renderObjectRigidbodys)
            {
                rigid.gravityScale = 1.0f;
            }
            Destroy(this.gameObject, 10.0f);
        }
    }
}
