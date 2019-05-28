using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageController : MonoBehaviour
{
    [TextArea(10, 30)]
    public string message;

    StageDirector stageDirector;

    float lastTime;

    private void Start()
    {
        stageDirector = StageDirector.Get();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        stageDirector?.SetPaper(message);
        stageDirector?.ShowSignal();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        stageDirector?.ShowSignal();
    }
}
