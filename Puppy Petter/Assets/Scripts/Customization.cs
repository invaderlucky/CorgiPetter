using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customization : MonoBehaviour {
	public Image[] reps;
	private int idx;

	void Start () {
		idx = 0;
        // Disable all but first image on start
		for (int i = 1; i < reps.Length; i++)
			reps[i].enabled = false;
	}

	public void DisplayNextImage () {
		// Go to next image
		idx++;
		// Handle wrap around
		if (idx > reps.Length - 1)
			idx = 0;

		// Enable current image
		reps[idx].enabled = true;

		// Disabled other images
		for (int i = 0; i < reps.Length; i++) {
			if (i != idx)
				reps[i].enabled = false;
		}
	}

	public void DisplayPreviousImage () {
		// Go to previous image
		idx--;
		// Handle wrap around
		if (idx < 0)
			idx = reps.Length - 1;

		// Enable current image
		reps[idx].enabled = true;

		// Disabled other images
		for (int i = 0; i < reps.Length; i++) {
			if (i != idx)
				reps[i].enabled = false;
		}
	}

	public void SaveSelection () {
		PlayerPrefs.SetInt("CharacterRep", idx);
	}
}
