using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHighlight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler{
    public Image corgi;
    public AudioClip puppySounds;
	Animator corgiWalk;

    void Start () {
		corgi.enabled = false;
		corgi.gameObject.SetActive(false);
		corgiWalk = corgi.GetComponent<Animator> ();
    }

	public void OnPointerEnter (PointerEventData eventData) {
		EnableCorgi (true);
    }

    public void OnSelect (BaseEventData eventData) {
		EnableCorgi (true);
    }

	public void OnPointerExit (PointerEventData eventData) {
		EnableCorgi (false);
    }

    public void OnDeselect (BaseEventData eventData) {
		EnableCorgi (false);
    }

	private void EnableCorgi(bool enable) {
		corgi.enabled = enable;
		corgi.gameObject.SetActive(enable);
		corgiWalk.Play ("corgi_idle");
		if (enable)
			SoundManager.instance.PlaySingle(puppySounds);
	}
}
