using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectLock : MonoBehaviour
{
    public GameObject lockObject;
    public Stage requiredAchievement;

    // Start is called before the first frame update
    void Start()
    {
        bool cleared = StageAchievement.IsCleared(requiredAchievement, 0);
        GetComponent<StageSelectable>().interactable = cleared;
        lockObject.SetActive(!cleared);
    }
}
