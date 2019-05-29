using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DetailBar : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TMPro.TMP_Text text;
    [Multiline]
    public string hoverText;

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.text = hoverText;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.text = "";
    }
}
