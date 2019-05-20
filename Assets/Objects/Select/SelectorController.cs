using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectorController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.Delay(.1f, () =>
        {
            if (transform.childCount > 0)
                transform.GetChild(0).GetComponent<Selectable>().Select();
        });
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = CameraController.Get().GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, LayerMask.NameToLayer("StageDisplay")))
        {
            // the object identified by hit.transform was clicked
            // do whatever you want
            var disp = hit.transform.gameObject.GetComponentInParent<StageSelectable>();
            var obj = disp?.gameObject;
            var selectable = obj?.GetComponent<Selectable>();
            var nav = EventSystem.current.currentSelectedGameObject?.GetComponentInParent<Selectable>();
            if (EventSystem.current.currentSelectedGameObject == null ||
                nav != null && (nav.FindSelectableOnLeft() == selectable ||
                nav.FindSelectableOnRight() == selectable ||
                nav.FindSelectableOnUp() == selectable ||
                nav.FindSelectableOnDown() == selectable))
                selectable?.Select();

            if (Input.GetButtonDown("Click"))
                disp.OnClick();
        }
    }
}
