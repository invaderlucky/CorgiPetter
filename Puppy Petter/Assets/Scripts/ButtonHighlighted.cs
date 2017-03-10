using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonHighlighted : MonoBehaviour, IPointerEnterHandler, ISelectHandler {
	public GameObject corgi;

	public void OnPointerEnter (PointerEventData eventData) {
			// Spawn above button
			Vector3 spawnPosition = new Vector3 (
				transform.position.x, 
				transform.position.y + 0.5f, 
				0.0f);

			// No rotation
			Quaternion spawnRotation = Quaternion.identity;

			// Display corgi
			GameObject inst = Instantiate (corgi, spawnPosition, spawnRotation);
	}

	public void OnSelect (BaseEventData eventData) {
			// Spawn above button
			Vector3 spawnPosition = new Vector3 (
				transform.position.x, 
				transform.position.y + 0.5f, 
				0.0f);

			// No rotation
			Quaternion spawnRotation = Quaternion.identity;

			// Display corgi
			GameObject inst = Instantiate (corgi, spawnPosition, spawnRotation);
	}
}
