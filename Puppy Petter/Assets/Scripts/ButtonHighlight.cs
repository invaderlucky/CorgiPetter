using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHighlight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler {
    public Image corgi;
    public AudioClip puppySounds;

    void Start () {
        corgi.enabled = false;
    }

	public void OnPointerEnter (PointerEventData eventData) {
        corgi.enabled = true;
        SoundManager.instance.PlaySingle(puppySounds);
    }

    public void OnSelect (BaseEventData eventData) {
        corgi.enabled = true;
        SoundManager.instance.PlaySingle(puppySounds);
    }

	public void OnPointerExit (PointerEventData eventData) {
        corgi.enabled = false;
    }

    public void OnDeselect (BaseEventData eventData) {
        corgi.enabled = false;
    }
}
