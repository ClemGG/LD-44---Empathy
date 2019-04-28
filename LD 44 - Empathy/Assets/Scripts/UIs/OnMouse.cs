using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnMouse : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public AudioClip btn, btnHover;
    public AudioSource a;

    public void OnPointerEnter(PointerEventData eventData)
    {
        a.PlayOneShot(btnHover);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        a.PlayOneShot(btn);
    }
}
