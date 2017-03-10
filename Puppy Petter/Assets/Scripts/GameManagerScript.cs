using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour {
	public static int GAME_OVER = 3;

	public Camera cam;
	public GameObject corgi;
	public GameObject player;

	public float lowDelay;
	public float highDelay;
	public float timeLeft;
	public Text timerText;

	private float maxHeight;
	private float corgiHeight;

	void Start () {
		// Make sure default camera is assigned
		if (cam == null)
			cam = Camera.main;
		
		// Camera bounds
		Vector3 upperCorner = new Vector3 (Screen.width, Screen.height, 0);
		Vector3 targetHeight = cam.ScreenToWorldPoint (upperCorner);

		// Get the corgi's renderer
		Renderer corgiRend = corgi.GetComponent<Renderer>();

		// Corgi bounds
		corgiHeight = corgiRend.bounds.extents.y;
		maxHeight = targetHeight.y - corgiHeight;

		DisplayTime();
		StartCoroutine (Spawn ());
	}

	void FixedUpdate () {
		if (timeLeft > 0)
			timeLeft -= Time.deltaTime;
		DisplayTime();
	}

	void DisplayTime () {
		timerText.text = "Time Left: " + Mathf.RoundToInt(timeLeft);
	}
	
	IEnumerator Spawn () {
		yield return new WaitForSeconds (2.0f);
		while (timeLeft > 0) {
			// Spawn in bounds of camera
			Vector3 spawnPosition = new Vector3 (
				transform.position.x, 
				Random.Range (-maxHeight, maxHeight - corgiHeight * 2.5f), 
				0.0f);

			// No rotation
			Quaternion spawnRotation = Quaternion.identity;

			Instantiate (corgi, spawnPosition, spawnRotation);

			yield return new WaitForSeconds (Random.Range (lowDelay, highDelay));
		}

		// Save score at end
		PlayerPrefs.SetInt("Score", player.GetComponent<ScoreScript>().getScore());
		// Load game over screen
		SceneManager.LoadScene (GAME_OVER);
	}
}
