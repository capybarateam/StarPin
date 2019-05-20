using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleBackground_Move : MonoBehaviour
{

    Vector3 m_pos;
    Vector3 m_pos2;

    GameObject title_Back2;

    // Start is called before the first frame update
    void Start()
    {
        title_Back2 = GameObject.Find("title_Back2");
        m_pos = new Vector3(30.6f, 1.7f, 0);
        m_pos2=  new Vector3(143.3f, 1.7f, 0.0f);

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = m_pos;
        title_Back2.transform.position = m_pos2;

        m_pos.x += -0.02f;

        if(m_pos.x < -80.5f)
        {
            m_pos.x = 143.3f;
        }


        m_pos2.x += -0.02f;
     
        if (m_pos2.x < -80.5f)
        {
            m_pos2.x = 143.3f;
        }
    }
}
