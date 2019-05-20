using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BackGround : MonoBehaviour
{
    public GameObject bg;
    public Vector2 speed;
    
    GameObject[] bgs = new GameObject[9];

    GameObject player;
    float width = 0;
    float height = 0;
    float max_width = 0;
    float max_height = 0;

    Vector3 position;
    Vector3 addPos;
    Vector3 originPos;

    // Start is called before the first frame update
    void Start()
    {
        // プレイヤーを取得
        player = GameObject.Find("Player").GetComponentInChildren<StarController>().gameObject;

        // 背景をプレイヤーの位置に合わせる
        bg.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, bg.transform.position.z);

        width = bg.GetComponent<Renderer>().bounds.size.x;
        height = bg.GetComponent<Renderer>().bounds.size.y;
        

        // カメラの外枠のスケールをワールド座標系で取得
        var distance = Vector3.Distance(bg.transform.position, Camera.main.transform.position);
        Vector3 leftTop = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightBottom = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, distance));
        
        width = rightBottom.x - leftTop.x;
        max_width = width * 10;
        height = rightBottom.y - leftTop.y;
        max_height = height * 10;

        var sp = bg.GetComponent<SpriteRenderer>();
        sp.size = new Vector2(max_width, max_height);

        position = new Vector3(player.transform.position.x, player.transform.position.y, bg.transform.position.z);
        bg.transform.position = position;

        bg.transform.localScale = new Vector3(width / sp.sprite.bounds.size.x, height / sp.sprite.bounds.size.y, 1);

        originPos = position - new Vector3((bg.transform.localScale.x * sp.sprite.bounds.size.x) / 2, (bg.transform.localScale.y * sp.sprite.bounds.size.y) / 2, 0);
    }

    // Update is called once per frame
    void Update()
    {
        // 背景の移動
        // 位置を決める
        var sp = bg.GetComponent<SpriteRenderer>();
        addPos += new Vector3(speed.x, speed.y, 0);
        bg.transform.position = position + new Vector3(addPos.x % (bg.transform.localScale.x * sp.sprite.bounds.size.x), addPos.y % (bg.transform.localScale.y * sp.sprite.bounds.size.y));

        // プレイヤーを追いかける
        Vector3 pos = player.transform.position - originPos;
        int xIndex = (int)(pos.x / (bg.transform.localScale.x * sp.sprite.bounds.size.x));
        position += new Vector3(xIndex * (bg.transform.localScale.x * sp.sprite.bounds.size.x), 0, 0);
        int yIndex = (int)(pos.y / (bg.transform.localScale.y * sp.sprite.bounds.size.y));
        position += new Vector3(0, yIndex * (bg.transform.localScale.y * sp.sprite.bounds.size.y), 0);

        // 軸となる座標を更新する
        originPos = position - new Vector3((bg.transform.localScale.x * sp.sprite.bounds.size.x) / 2, (bg.transform.localScale.y * sp.sprite.bounds.size.y) / 2, 0);
    }
}
