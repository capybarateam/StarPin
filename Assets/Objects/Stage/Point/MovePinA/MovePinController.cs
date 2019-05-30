using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePinController : MonoBehaviour
{
    public Transform target;
    public float duration;

    Vector3 posA;
    Vector3 posB;
    float per;

    void Start()
    {
        posA = transform.position;
        posB = target.transform.position;

        GetComponent<LineRenderer>().SetPosition(0, posA);
        GetComponent<LineRenderer>().SetPosition(1, posB);
    }

    // Update is called once per frame
    void Update()
    {
        per += Time.deltaTime / duration;
        var wiper = ((int)per % 2 == 0) ? per % 1 : 1 - per % 1;
        transform.position = Vector3.Lerp(posA, posB, wiper);
    }
}
