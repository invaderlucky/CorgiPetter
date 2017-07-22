using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalScoreScript : MonoBehaviour {
	public Text scoreText;
    Image endImageComponent;
    public Sprite [] endImages; // MUST match enum below

    private enum characterCodes {
        BLUE,
        PINK,
        PURPLE,
        RED,
        GREEN,
        ORANGE
    }

	// Use this for initialization
	void Start () {
		int score = PlayerPrefs.GetInt("Score");
		scoreText.text = "Score: " + score;
        endImageComponent = GetComponent<Image>();
        endImageComponent.sprite = endImages[PlayerPrefs.GetInt("CharacterRep")];
	}
}
