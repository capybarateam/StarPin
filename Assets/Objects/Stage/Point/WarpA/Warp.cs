using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp : MonoBehaviour, IAttachable
{

    //[SerializeField]
    //GameObject warp_in;

    [SerializeField]
    GameObject warp_out;

    // 引っ付けるかどうか
    public void CheckAttachable(StarController star, ref bool cancel)
    {
    }

    // 引っ付いたときの処理
    public void OnAttached(StarController star)
    {
        star.currentJoint = warp_out;
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
