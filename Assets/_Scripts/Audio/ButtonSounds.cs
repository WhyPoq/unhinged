using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSounds : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManager.instance.Play("MouseOver");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        AudioManager.instance.Play("MouseClick");
    }

}
