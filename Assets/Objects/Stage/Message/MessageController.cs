using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageController : MonoBehaviour
{
    [TextArea(10, 30)]
    public string message;

    BaseDirector baseDirector;

    float lastTime;

    private void Start()
    {
        baseDirector = BaseDirector.Get();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        baseDirector?.SetPaper(message);
        baseDirector?.ShowSignal();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        baseDirector?.ShowSignal();
    }
}
