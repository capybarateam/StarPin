using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorController : MonoBehaviour
{
    Image image;

    Vector3 lastPosition;
    float opacity;

    // 完全な表示状態にするまでに必要な移動量 (px)
    public float opacityPerMove = 40;
    public float startVisibleMove = 10;

    public float hideStartTime = 2;
    public float hideTime = 4;

    public float moveEps = 1e-2f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;

        image = GetComponent<Image>();

        lastPosition = Input.mousePosition;
    }

    // Update is called once per frame
    void Update()
    {
        var position = Input.mousePosition;
        var move = position - lastPosition;
        var moveLength = move.magnitude;
        transform.position = lastPosition = position;

        var max = 1 + hideStartTime / hideTime;
        var min = -startVisibleMove / opacityPerMove;
        opacity += moveLength / opacityPerMove;
        opacity -= Time.deltaTime / hideTime;
        if (opacity >= 1 && moveLength > moveEps)
            opacity = max;
        opacity = Mathf.Clamp(opacity, min, max);

        var color = image.color;
        color.a = Mathf.Clamp(opacity, 0, 1);
        image.color = color;
    }

    void OnApplicationFocus(bool focus)
    {
        if (focus)
            Cursor.visible = false;
    }
}
