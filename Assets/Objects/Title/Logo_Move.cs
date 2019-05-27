using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Logo_Move : MonoBehaviour
{

    //タイトルロゴ
    RectTransform m_logo_rect;
    Vector3 m_position = new Vector3(0, 100, 0);

    //ストーリー
    RectTransform m_story_rect;
    Vector3 m_storyPosition = new Vector3(840, 120, 0);

    //動かないようにする
    bool m_keyflag = false;

    //ロゴの動き
    bool m_flag = false;
    bool m_flag2 = false;

   
    // Start is called before the first frame update
    void Start()
    {
        //タイトルロゴを見つける
        m_logo_rect = GameObject.Find("Title_Logo").GetComponent<RectTransform>();
        //ストーリーテクスチャを見つける
        m_story_rect = GameObject.Find("Custom").GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        //各ポジションを代入
        m_logo_rect.localPosition = m_position;
        m_story_rect.localPosition = m_storyPosition;

        //動き
        if (!m_keyflag)
        {
            //右押したら左に動く
            if (Input.GetMouseButton(1))
            {
                m_flag = true;
            }
            if (m_flag)
            { 
                m_position.x -= 10;
                m_storyPosition.x -= 10;
                
            }

            if(m_storyPosition.x<=17)
            {
                m_storyPosition.x = 17;
            }

            if (m_position.x <= -1011)
            {
                m_position.x = -1011;

                m_flag = false;
                m_keyflag = true;
            }
        }


        if(m_keyflag)
        {
            //左キー押したら右に動く
            if (Input.GetMouseButton(1))
            {
                m_flag2 = true;

            }
            if (m_flag2)
            {
                m_position.x += 10;
                m_storyPosition.x += 10;
            }

            if (m_storyPosition.x >= 840)
            {
                m_storyPosition.x = 840;
            }


            if (m_position.x >= 0)
            {
                m_position.x = 0;

                m_flag2 = false;
                m_keyflag = false;
            }
        }
    }

}
