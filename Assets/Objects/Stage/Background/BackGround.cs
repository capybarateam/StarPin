using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BackGround : MonoBehaviour
{
    public GameObject bg;
    public Vector2 speed;

    GameObject[] bgs = new GameObject[9];

    Vector2 size;
    Vector2 addPos;
    Vector2 cellSize;

    // Start is called before the first frame update
    void Start()
    {
        // カメラの外枠のスケールをワールド座標系で取得
        Vector3 backPoint = Vector3.forward * bg.transform.position.z;
        Plane plane = new Plane(Vector3.back, backPoint);
        Ray leftTopRay = Camera.main.ViewportPointToRay(new Vector3(0, 0, 0));
        Ray rightBottomRay = Camera.main.ViewportPointToRay(new Vector3(1, 1, 0));
        Vector3 leftTop = backPoint;
        Vector3 rightBottom = backPoint;
        if (plane.Raycast(leftTopRay, out float leftTopDist))
            leftTop = leftTopRay.GetPoint(leftTopDist);
        if (plane.Raycast(rightBottomRay, out float rightBottomDist))
            rightBottom = rightBottomRay.GetPoint(rightBottomDist);

        var sp = bg.GetComponent<SpriteRenderer>();
        cellSize = new Vector2(bg.transform.localScale.x * sp.sprite.bounds.size.x, bg.transform.localScale.y * sp.sprite.bounds.size.y);
        sp.size = size = (Vector2)(rightBottom - leftTop) / bg.transform.localScale * 4;
    }

    // Update is called once per frame
    void Update()
    {
        var campos = Camera.main.transform.position;

        // プレイヤーを追いかける
        var position = new Vector3((int)(campos.x / cellSize.x) * cellSize.x, (int)(campos.y / cellSize.y) * cellSize.y, bg.transform.position.z);

        // 背景の移動
        addPos += new Vector2(speed.x, speed.y) * 60 * Time.deltaTime;

        // 位置を決める
        bg.transform.position = position + new Vector3(addPos.x % cellSize.x, addPos.y % cellSize.y);
    }
}
