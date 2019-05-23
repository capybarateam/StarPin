using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WarpSelectable : MonoBehaviour, ISelectHandler
{
    public void OnSelect(BaseEventData eventData)
    {
        this.Delay(.5f, () =>
        {
            if (GetComponentInParent<SelectCurrent>()?.current == GetComponent<StageSelectable>())
            {
                GetComponent<StageSelectable>()?.OnClick();
            }
        });
    }
}
