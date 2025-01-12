using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Simple class for toggling button text color. 
/// </summary>
public class ButtonTouchText : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private TMP_Text text;
    private Color idleColor;
    [SerializeField]
    private Color touchedColor;
    
    void Start()
    {
        if(text != null)
        {
            idleColor = text.color;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        text.color = touchedColor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        text.color = idleColor;
    }

    public void SetText(TMP_Text txt)
    {
        text.color = idleColor;
        text = txt;
    }
}
