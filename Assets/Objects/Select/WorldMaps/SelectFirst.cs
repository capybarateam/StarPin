using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectFirst : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.Delay(.1f, () =>
        {
            GetComponent<Selectable>().Select();
        });
    }
}
