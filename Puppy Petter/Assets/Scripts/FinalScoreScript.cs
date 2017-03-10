using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalScoreScript : MonoBehaviour {
	public Text scoreText;

	// Use this for initialization
	void Start () {
		int score = PlayerPrefs.GetInt("Score");
		scoreText.text = "Score: " + score;
	}
}
