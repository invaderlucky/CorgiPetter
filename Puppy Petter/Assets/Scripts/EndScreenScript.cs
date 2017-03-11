using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreenScript : MonoBehaviour {
	const int blue = 0, pink = 1;
	public GameObject bluePanel;
	public GameObject pinkPanel;

	void Start () {
		if (PlayerPrefs.GetInt("CharacterRep") == blue) {
			bluePanel.SetActive(true);
			pinkPanel.SetActive(false);
		}
			
		else if (PlayerPrefs.GetInt("CharacterRep") == pink) {
			bluePanel.SetActive(false);
			pinkPanel.SetActive(true);
		}
	}
}
