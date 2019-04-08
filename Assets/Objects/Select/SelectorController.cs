using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectorController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
            hit.transform.gameObject.GetComponentInParent<StageDisplay>()?.gameObject.GetComponent<Selectable>().Select();
        }
    }
}
