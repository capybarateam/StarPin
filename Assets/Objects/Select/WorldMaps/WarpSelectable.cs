using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WarpSelectable : MonoBehaviour, ISelectHandler
{
    public Stage destinationStage;

    public void OnSelect(BaseEventData eventData)
    {
        var stageCurrent = GetComponentInParent<SelectCurrent>()?.current;
        var sta = GetComponent<StageSelectable>();
        //if (stageCurrent == sta)
        {
            var stageSelectable = GetComponent<StageSelectable>();
            this.Delay(.5f, () =>
            {
                if (stageSelectable != null && stageSelectable.stage != null)
                    StageAchievement.SetLastStage(stageSelectable.stage.sceneName, destinationStage);
                stageSelectable?.OnClick();
            });
        }
    }
}
