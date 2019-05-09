using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_SwitchPinController : MonoBehaviour
{
    // 透明にするピン
    public List<GameObject> PinList = new List<GameObject>();

    // 制限時間
    public float LIMIT_TIME = 10;

    // スターの管理変数
    StarController starController;

    // 押されたフラグ
    bool isP = false;

    // 時間
    float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        starController = GameObject.Find("Player").GetComponentInChildren<StarController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isP)
        {
            foreach (GameObject obj in PinList)
            {
                // 透明化をなくす
                GameObject pin = obj.transform.GetChild(0).gameObject;
                foreach (Renderer render in pin.GetComponentsInChildren<Renderer>())
                {
                    AnTransparent(render.materials);
                }
                // 当たり判定を戻す
                obj.GetComponentInChildren<CircleCollider2D>().enabled = true;
            }
        }
        else
        {
            foreach (GameObject obj in PinList)
            {
                // 触れていたら何もしない
                if (obj.GetComponentInChildren<PointController>().touched) continue;

                // 透明にする
                GameObject pin = obj.transform.GetChild(0).gameObject;

                foreach (Renderer render in pin.GetComponentsInChildren<Renderer>())
                {
                    Transparent(render.materials);
                }
                // 当たり判定をなくす
                obj.GetComponentInChildren<CircleCollider2D>().enabled = false;
            }
        }

        // PSwichを起動
        if (this.gameObject == starController.currentJoint)
        {
            isP = true;
            time = LIMIT_TIME;
        }

        // 時間を進める
        time -= Time.deltaTime;

        // 時間切れ
        if (time <= 0)
        {
            isP = false;
            time = 0;
        }
    }

    // マテリアルを透明化
    void Transparent(Material[] materials)
    {
        foreach (Material material in materials)
        {
            //BlendModeUtils.SetBlendMode(material, BlendModeUtils.Mode.Fade);
            float alpha = material.GetColor("BaseColor").a;
            Debug.Log(alpha);
            alpha -= 0.05f;
            if (alpha < 0.2f) alpha = 0.3f;
            material.SetColor("BaseColor", new Color(material.GetColor("BaseColor").r, material.GetColor("BaseColor").g, material.GetColor("BaseColor").b, alpha));
            //material.color = new Color(material.color.r, material.color.g, material.color.b, alpha);
            //material.SetColor("BaseColor", Color.red);
        }
    }

    // マテリアルを非透明化
    void AnTransparent(Material[] materials)
    {
        foreach (Material material in materials)
        {
            float alpha = material.color.a;
            alpha += 0.05f;
            if (alpha > 1.0f)
            {
                alpha = 1.0f;
                BlendModeUtils.SetBlendMode(material, BlendModeUtils.Mode.Opaque);
            }
            material.color = new Color(material.color.r, material.color.g, material.color.b, alpha);
        }
    }
}
