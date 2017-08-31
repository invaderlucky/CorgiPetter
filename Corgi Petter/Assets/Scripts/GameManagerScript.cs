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
	private float minHeight;
	private float spawnOffset = 4.5f;
	private float maxBound = 3.0f;
	private float minBound = -4.25f;

	public bool paused;
	GUIStyle style;

    private enum characterCodes {
        BLUE,
        PINK,
        PURPLE,
        RED,
        GREEN,
        ORANGE
    }

	void Start () {
		// Make sure default camera is assigned
		if (cam == null)
			cam = Camera.main;
		
		paused = false;

        GameObject.Find("BlueCharacter").SetActive(PlayerPrefs.GetInt("CharacterRep") == (int) characterCodes.BLUE);
        GameObject.Find("PinkCharacter").SetActive(PlayerPrefs.GetInt("CharacterRep") == (int) characterCodes.PINK);
        GameObject.Find("PurpleCharacter").SetActive(PlayerPrefs.GetInt("CharacterRep") == (int) characterCodes.PURPLE);
        GameObject.Find("RedCharacter").SetActive(PlayerPrefs.GetInt("CharacterRep") == (int) characterCodes.RED);
        GameObject.Find("GreenCharacter").SetActive(PlayerPrefs.GetInt("CharacterRep") == (int) characterCodes.GREEN);
        GameObject.Find("OrangeCharacter").SetActive(PlayerPrefs.GetInt("CharacterRep") == (int) characterCodes.ORANGE);

		if (PlayerPrefs.GetInt("CharacterRep") == (int) characterCodes.BLUE)
			player = GameObject.Find("BlueCharacter");
		else if (PlayerPrefs.GetInt("CharacterRep") == (int) characterCodes.PINK)
			player = GameObject.Find("PinkCharacter");
		else if (PlayerPrefs.GetInt("CharacterRep") == (int) characterCodes.PURPLE)
			player = GameObject.Find("PurpleCharacter");
		else if (PlayerPrefs.GetInt("CharacterRep") == (int) characterCodes.RED)
			player = GameObject.Find("RedCharacter");
		else if (PlayerPrefs.GetInt("CharacterRep") == (int) characterCodes.GREEN)
			player = GameObject.Find("GreenCharacter");
		else if (PlayerPrefs.GetInt("CharacterRep") == (int) characterCodes.ORANGE)
			player = GameObject.Find("OrangeCharacter");

		// Get the corgi's renderer
		//Renderer corgiRend = corgi.GetComponent<Renderer>();

		// Corgi bounds
		maxHeight = maxBound;
		minHeight = minBound;
		style = new GUIStyle ();
		style.normal.textColor = Color.black;

		DisplayTime();
		DisplayLives();
		StartCoroutine (Spawn ());
	}
		
	void OnGUI () {
		//GUI.Label(new Rect(130, 300, 200, 100), "max = " + maxHeight, style );  
		//GUI.Label(new Rect(130, 400, 200, 100), "min = " + minHeight, style ); 
	}

	public void TogglePauseMenu (bool pause) {
		if (pause)
			Time.timeScale = 0.0f;
		else
			Time.timeScale = 1.0f;
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

	void FixedUpdate () {
		// Make sure corgis only spawn within certain units of the player
		maxHeight = player.gameObject.GetComponent<Rigidbody2D> ().position.y + spawnOffset;
		minHeight = player.gameObject.GetComponent<Rigidbody2D> ().position.y - spawnOffset;
		maxHeight = Mathf.Clamp (maxHeight, minBound, maxBound);
		minHeight = Mathf.Clamp (minHeight, minBound, maxBound); 

		if (PlayerPrefs.GetInt("GameMode") == 0) {
			if (timeLeft > 0)
				timeLeft -= Time.deltaTime;
			else
				EndGame ();
			DisplayTime();
		}
		else if (PlayerPrefs.GetInt("GameMode") == 1) {
			if (lives > 0)
				DisplayLives ();
			else
				EndGame ();
		}
	}

	void EndGame () {
		// Save score at end
		PlayerPrefs.SetInt("Score", player.GetComponent<ScoreScript>().getScore());
		// Load game over screen
		SceneManager.LoadScene (GAME_OVER);
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
							Random.Range (minHeight, maxHeight), 
							0.0f);

						// No rotation
						Quaternion spawnRotation = Quaternion.identity;

						Instantiate (corgi, spawnPosition, spawnRotation);

						yield return new WaitForSeconds (Random.Range (lowDelay, highDelay));
					}
				}
			}
			else if (PlayerPrefs.GetInt("GameMode") == 1) {
				while (lives > 0) {
					if (Time.timeScale != 0) {
						// Spawn in bounds of camera
						Vector3 spawnPosition = new Vector3 (
							transform.position.x, 
							Random.Range (minHeight, maxHeight), 
							0.0f);

						// No rotation
						Quaternion spawnRotation = Quaternion.identity;

						Instantiate (corgi, spawnPosition, spawnRotation);

						yield return new WaitForSeconds (Random.Range (lowDelay, highDelay));
					}
				}
			}
		}
	}
}
