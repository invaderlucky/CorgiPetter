using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {
	public GameObject heart;

	public Text scoreText;
	public int corgiValue;

	private int score;

	// Use this for initialization
	void Start () {
		score = 0;
		DisplayScore ();
	}
	
	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.tag == "Corgi" ) {
			// Get rid of corgi object
			Destroy (other.gameObject);
			// Update score
			score += corgiValue;
			DisplayScore ();

			// Spawn above player
			Vector3 spawnPosition = new Vector3 (
				transform.position.x + 0.5f, 
				transform.position.y + 0.5f, 
				0.0f);

			// No rotation
			Quaternion spawnRotation = Quaternion.identity;

			// Display and destroy heart
			GameObject inst = Instantiate (heart, spawnPosition, spawnRotation);
			Destroy (inst, 1.0f);
		}

	}

	public void DecScore () {
		score -= corgiValue;
		DisplayScore ();
	}

	void DisplayScore () {
		scoreText.text = "Score: " + score;
	}

	public int getScore() {
		return score;
	}
}
