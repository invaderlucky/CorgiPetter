using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour {
	public static int GAME_OVER = 3;

	public Camera cam;
	public GameObject corgi;
	GameObject player;

	public float lowDelay;
	public float highDelay;
	public float timeLeft;
	public float lives;
	public Text timerText;
	public Text livesText;

	private float maxHeight;
	private float corgiHeight;

	public bool paused;

	void Start () {
		// Make sure default camera is assigned
		if (cam == null)
			cam = Camera.main;
		
		paused = false;

		// Camera bounds
		Vector3 upperCorner = new Vector3 (Screen.width, Screen.height, 0);
		Vector3 targetHeight = cam.ScreenToWorldPoint (upperCorner);

		if (PlayerPrefs.GetInt("CharacterRep") == 0) {
			player = GameObject.Find("BlueCharacter");
			GameObject.Find("PinkCharacter").SetActive(false);
			GameObject.Find("PurpleCharacter").SetActive(false);
		}
		if (PlayerPrefs.GetInt("CharacterRep") == 1) {
			player = GameObject.Find("PinkCharacter");
			GameObject.Find("BlueCharacter").SetActive(false);
			GameObject.Find("PurpleCharacter").SetActive(false);
		}
		if (PlayerPrefs.GetInt("CharacterRep") == 2) {
			player = GameObject.Find("PurpleCharacter");
			GameObject.Find("BlueCharacter").SetActive(false);
			GameObject.Find("PinkCharacter").SetActive(false);
		}

		// Get the corgi's renderer
		Renderer corgiRend = corgi.GetComponent<Renderer>();

		// Corgi bounds
		corgiHeight = corgiRend.bounds.extents.y;
		maxHeight = targetHeight.y - corgiHeight;

		DisplayTime();
		DisplayLives();
		StartCoroutine (Spawn ());
	}

	public void TogglePauseMenu (bool pause) {
		if (pause)
			Time.timeScale = 0.0f;
		else
			Time.timeScale = 1.0f;
		/*
		paused = pause;
		if (pause)
			PlayerPrefs.SetInt("Pause", 1);
		else
			PlayerPrefs.SetInt("Pause", 0);
		*/
	}

	void OnTriggerEnter2D (Collider2D other) {
		// Get rid of corgi object
		if (Time.timeScale != 0) {
			if (other.gameObject.tag == "Corgi" ) {
				Destroy (other.gameObject);
				ScoreScript scoring = player.gameObject.GetComponent<ScoreScript>();
				// Only penalize the score if time based
				if (PlayerPrefs.GetInt("GameMode") == 0)
					scoring.DecScore ();
				else
					lives--;
			}
		}
	}

	/*void Update () {
		if (Input.GetKey(KeyCode.Escape)) {
			Time.timeScale = 0;
		}
	}*/

	void FixedUpdate () {
		//if (!paused) {
			if (PlayerPrefs.GetInt("GameMode") == 0) {
				if (timeLeft > 0)
					timeLeft -= Time.deltaTime;
				DisplayTime();
			}
			else if (PlayerPrefs.GetInt("GameMode") == 1) {
				DisplayLives();
			}
		//}
	}

	void DisplayTime () {
		if (PlayerPrefs.GetInt("GameMode") == 0)
			timerText.text = "Time Left: " + Mathf.RoundToInt(timeLeft);
	}

	void DisplayLives () {
		if (PlayerPrefs.GetInt("GameMode") == 1)
			livesText.text = "Lives: " + Mathf.RoundToInt(lives);
	}
	
	IEnumerator Spawn () {
		if (Time.timeScale != 0) {
		yield return new WaitForSeconds (2.0f);
		// Make corgi's until time's up
		if (PlayerPrefs.GetInt("GameMode") == 0) {
			while (timeLeft > 0) {
				if (Time.timeScale != 0) {
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
			}
		}
		else if (PlayerPrefs.GetInt("GameMode") == 1) {
			while (lives > 1) {
				if (Time.timeScale != 0) {
					// Spawn in bounds of camera
					Vector3 spawnPosition = new Vector3 (
						transform.position.x, 
						Random.Range (-maxHeight, maxHeight - corgiHeight * 3.0f), 
						0.0f);

					// No rotation
					Quaternion spawnRotation = Quaternion.identity;

					Instantiate (corgi, spawnPosition, spawnRotation);

					yield return new WaitForSeconds (Random.Range (lowDelay, highDelay));
				}
			}
		}
		// Save score at end
		PlayerPrefs.SetInt("Score", player.GetComponent<ScoreScript>().getScore());
		// Load game over screen
		SceneManager.LoadScene (GAME_OVER);
		}
	}
}
