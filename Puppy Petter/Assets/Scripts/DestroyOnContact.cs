using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnContact : MonoBehaviour {
	public GameObject player;
	
	void OnTriggerEnter2D (Collider2D other) {
		// Get rid of corgi object
		if (other.gameObject.tag == "Corgi" ) {
			Destroy (other.gameObject);
			ScoreScript scoring = player.gameObject.GetComponent<ScoreScript>();
			scoring.DecScore ();
		}
	}
}
