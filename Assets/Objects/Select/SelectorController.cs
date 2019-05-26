using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectorController : MonoBehaviour
{
    public float opacity;
    Vector3 lastPosition;

    // 完全な表示状態にするまでに必要な移動量 (px)
    public float opacityPerMove = 40;
    public float startVisibleMove = 10;
    public float hideStartTime = 2;
    public float hideTime = 4;
    public float moveEps = 1e-2f;

    // Start is called before the first frame update
    void Start()
    {
        this.Delay(.1f, () =>
        {
            if (transform.childCount > 0)
                transform.GetChild(0).GetComponentInChildren<Selectable>().Select();
        });
    }

    // Update is called once per frame
    void Update()
    {
        // マウス
        var position = Input.mousePosition;
        var move = position - lastPosition;
        var moveLength = move.magnitude;
        lastPosition = position;

        var max = 1 + hideStartTime / hideTime;
        var min = -startVisibleMove / opacityPerMove;
        opacity += moveLength / opacityPerMove;
        //opacity -= Time.deltaTime / hideTime;
        if (Mathf.Abs(Input.GetAxis("Vertical")) > 0 || Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
            opacity -= .5f;
        opacity = Mathf.Clamp(opacity, min, max);

        if (opacity > .5f)
        {
            Ray ray = CameraController.Get().GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, LayerMask.NameToLayer("StageDisplay")))
            {
                // the object identified by hit.transform was clicked
                // do whatever you want
                var stageSelector = hit.transform.gameObject.GetComponentInParent<StageSelectable>();
                if (stageSelector != null && stageSelector.interactable)
                {
                    var selectable = stageSelector?.GetComponent<Selectable>();
                    var nav = EventSystem.current.currentSelectedGameObject?.GetComponentInParent<Selectable>();
                    if (nav == null ||
                        (nav.FindSelectableOnLeft() == selectable ||
                        nav.FindSelectableOnRight() == selectable ||
                        nav.FindSelectableOnUp() == selectable ||
                        nav.FindSelectableOnDown() == selectable))
                    {
                        selectable?.Select();
                    }

                    if (Input.GetButtonDown("Click"))
                        stageSelector.OnClick();
                }
            }
        }
    }
}
