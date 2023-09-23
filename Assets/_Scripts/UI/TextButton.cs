using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Button))]
public class TextButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{

    TextMeshProUGUI txt;
    Color32 baseColor;
    Color32 baseOutlineColor;
    Color32 baseFaceColor;
    Button btn;
    bool interactableDelay;

    void Start()
    {
        txt = GetComponentInChildren<TextMeshProUGUI>();
        baseColor = txt.color;
        baseOutlineColor = txt.outlineColor;
        baseFaceColor = txt.faceColor;
        btn = GetComponent<Button>();
        interactableDelay = btn.interactable;
    }

    void Update()
    {
        if (btn.interactable != interactableDelay)
        {
            if (btn.interactable)
            {
                txt.color = baseColor * btn.colors.normalColor * btn.colors.colorMultiplier;
                txt.outlineColor = baseOutlineColor * btn.colors.normalColor * btn.colors.colorMultiplier;
                txt.faceColor = baseFaceColor * btn.colors.normalColor * btn.colors.colorMultiplier;
            }
            else
            {
                txt.color = baseColor * btn.colors.disabledColor * btn.colors.colorMultiplier;
                txt.outlineColor = baseOutlineColor * btn.colors.disabledColor * btn.colors.colorMultiplier;
                txt.faceColor = baseFaceColor * btn.colors.disabledColor * btn.colors.colorMultiplier;
            }
        }
        interactableDelay = btn.interactable;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (btn.interactable)
        {
            txt.color = baseColor * btn.colors.highlightedColor * btn.colors.colorMultiplier;
            txt.outlineColor = baseOutlineColor * btn.colors.highlightedColor * btn.colors.colorMultiplier;
            txt.faceColor = baseFaceColor * btn.colors.highlightedColor * btn.colors.colorMultiplier;
        }
        else
        {
            txt.color = baseColor * btn.colors.disabledColor * btn.colors.colorMultiplier;
            txt.outlineColor = baseOutlineColor * btn.colors.disabledColor * btn.colors.colorMultiplier;
            txt.faceColor = baseFaceColor * btn.colors.disabledColor * btn.colors.colorMultiplier;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (btn.interactable)
        {
            txt.color = baseColor * btn.colors.pressedColor * btn.colors.colorMultiplier;
            txt.outlineColor = baseOutlineColor * btn.colors.pressedColor * btn.colors.colorMultiplier;
            txt.faceColor = baseFaceColor * btn.colors.pressedColor * btn.colors.colorMultiplier;
        }
        else
        {
            txt.color = baseColor * btn.colors.disabledColor * btn.colors.colorMultiplier;
            txt.outlineColor = baseOutlineColor * btn.colors.disabledColor * btn.colors.colorMultiplier;
            txt.faceColor = baseFaceColor * btn.colors.disabledColor * btn.colors.colorMultiplier;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (btn.interactable)
        {
            txt.color = baseColor * btn.colors.highlightedColor * btn.colors.colorMultiplier;
            txt.outlineColor = baseOutlineColor * btn.colors.highlightedColor * btn.colors.colorMultiplier;
            txt.faceColor = baseFaceColor * btn.colors.highlightedColor * btn.colors.colorMultiplier;
        }
        else
        {
            txt.color = baseColor * btn.colors.disabledColor * btn.colors.colorMultiplier;
            txt.outlineColor = baseOutlineColor * btn.colors.disabledColor * btn.colors.colorMultiplier;
            txt.faceColor = baseFaceColor * btn.colors.disabledColor * btn.colors.colorMultiplier;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (btn.interactable)
        {
            txt.color = baseColor * btn.colors.normalColor * btn.colors.colorMultiplier;
            txt.outlineColor = baseOutlineColor * btn.colors.normalColor * btn.colors.colorMultiplier;
            txt.faceColor = baseFaceColor * btn.colors.normalColor * btn.colors.colorMultiplier;
        }
        else
        {
            txt.color = baseColor * btn.colors.disabledColor * btn.colors.colorMultiplier;
            txt.outlineColor = baseOutlineColor * btn.colors.disabledColor * btn.colors.colorMultiplier;
            txt.faceColor = baseFaceColor * btn.colors.disabledColor * btn.colors.colorMultiplier;
        }
    }

}