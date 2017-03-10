using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHighlight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler {
    public Image corgi;
    Image inst;

	public void OnPointerEnter (PointerEventData eventData) {
        // New position
        Vector3 spawnPosition = new Vector3 (
            transform.position.x, 
            transform.position.y + 2.0f, 
            0.0f);

        // No rotation
        Quaternion spawnRotation = Quaternion.identity;

        // Display and destroy heart
        Instantiate (corgi, spawnPosition, spawnRotation);
    }

    public void OnSelect (BaseEventData eventData) {
        // New position
        Vector3 spawnPosition = new Vector3 (
            transform.position.x, 
            transform.position.y + 2.0f, 
            0.0f);

        // No rotation
        Quaternion spawnRotation = Quaternion.identity;

        // Display and destroy heart
        inst = Instantiate (corgi, spawnPosition, spawnRotation);
    }

	public void OnPointerExit (PointerEventData eventData) {
        Destroy (inst);
    }

    public void OnDeselect (BaseEventData eventData) {
        Destroy (inst);
    }
}
