using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreenScript : MonoBehaviour {
	const int blue = 0, pink = 1, purple = 2;
	public GameObject bluePanel;
	public GameObject pinkPanel;
	public GameObject purplePanel;

	void Start () {
		if (PlayerPrefs.GetInt("CharacterRep") == blue) {
			bluePanel.SetActive(true);
			pinkPanel.SetActive(false);
			purplePanel.SetActive(false);
		}
			
		else if (PlayerPrefs.GetInt("CharacterRep") == pink) {
			bluePanel.SetActive(false);
			purplePanel.SetActive(false);
			pinkPanel.SetActive(true);
		}

		else if (PlayerPrefs.GetInt("CharacterRep") == purple) {
			bluePanel.SetActive(false);
			purplePanel.SetActive(true);
			pinkPanel.SetActive(false);
		}
	}
}
