using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        var cam = GameObject.Find("Main Camera");
        var star = GameObject.Find("StarObject");
        cam.GetComponent<CameraController>().SetTarget(star);
        GameObject.Find("LetterBox").GetComponent<Animator>().SetBool("Enabled", true);
    }
}
