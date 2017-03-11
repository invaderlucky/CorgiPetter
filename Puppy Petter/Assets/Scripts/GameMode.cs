using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour {
	public void SaveGameMode (int mode) {
		PlayerPrefs.SetInt("GameMode", mode);
	}
}
