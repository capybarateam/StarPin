using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

    public GameObject onModel;
    public GameObject offModel;

    // Start is called before the first frame update
    void Start()
    {
        starController = GameObject.Find("Player").GetComponentInChildren<StarController>();

        var prefabEffect = GetComponentInChildren<ParticleSystem>().transform.parent;
        foreach (GameObject obj in PinList)
        {
            Instantiate(prefabEffect, obj.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isP)
        {
            foreach (GameObject obj in PinList)
            {
                var p = obj.GetComponentInChildren<ParticleSystem>();
                if (p != null && !p.isPlaying)
                    p.Play();
                var p2 = obj.GetComponentInChildren<SpriteRenderer>();
                if (p2 != null)
                    p2.enabled = false;

                // 透明化をなくす
                GameObject pin = obj.transform.GetChild(0).gameObject;
                foreach (Renderer render in pin.GetComponentsInChildren<Renderer>())
                {
                    render.enabled = true;
                    //AnTransparent(render.materials);
                }
                // 当たり判定を戻す
                obj.GetComponentInChildren<CircleCollider2D>().enabled = true;
            }

            foreach (var obj in offModel.GetComponentsInChildren<MeshRenderer>())
                obj.enabled = false;
            foreach (var obj in onModel.GetComponentsInChildren<MeshRenderer>())
                obj.enabled = true;
        }
        else
        {
            foreach (GameObject obj in PinList)
            {
                var p = obj.GetComponentInChildren<ParticleSystem>();
                p?.Stop();
                var p2 = obj.GetComponentInChildren<SpriteRenderer>();
                if (p2 != null)
                    p2.enabled = true;

                // 触れていたら何もしない
                if (obj.GetComponentInChildren<PointController>().touched) continue;

                // 透明にする
                GameObject pin = obj.transform.GetChild(0).gameObject;

                foreach (Renderer render in pin.GetComponentsInChildren<Renderer>())
                {
                    render.enabled = false;
                    //Transparent(render.materials);
                }
                // 当たり判定をなくす
                obj.GetComponentInChildren<CircleCollider2D>().enabled = false;
            }

            foreach (var obj in offModel.GetComponentsInChildren<MeshRenderer>())
                obj.enabled = true;
            foreach (var obj in onModel.GetComponentsInChildren<MeshRenderer>())
                obj.enabled = false;
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

    /*
    // マテリアルを透明化
    void Transparent(Material[] materials)
    {
        foreach (Material material in materials)
        {
            BlendModeUtils.SetBlendMode(material, BlendModeUtils.Mode.Fade);
            float alpha = material.color.a;
            alpha -= 0.05f;
            if (alpha < 0.2f) alpha = 0.2f;
            material.color = new Color(material.color.r, material.color.g, material.color.b, alpha);
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
    */
}
