using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WarpSelectable : MonoBehaviour, ISelectHandler
{
    public Stage destinationStage;

    public void OnSelect(BaseEventData eventData)
    {
        if (GetComponentInParent<SelectCurrent>()?.current == GetComponent<StageSelectable>())
        {
            var stageSelectable = GetComponent<StageSelectable>();
            this.Delay(.5f, () =>
            {
                StageAchievement.SetLastStage(stageSelectable.stage.sceneName, destinationStage);
                stageSelectable?.OnClick();
            });
        }
    }
}
