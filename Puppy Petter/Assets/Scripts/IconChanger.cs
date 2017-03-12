using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconChanger : MonoBehaviour {
	public Sprite onIcon;
	public Sprite offIcon;

	public void ChangeIcon () {
		Button button = GetComponent<Button>();
		if (button.tag == "musicBtn") {
			if (SoundManager.instance.GetMusicPlaying())
				button.image.overrideSprite = onIcon;
			else
				button.image.overrideSprite = offIcon;
		}
		else if (button.tag == "efxBtn") {
			if (SoundManager.instance.GetEfxPlaying())
				button.image.overrideSprite = onIcon;
			else
				button.image.overrideSprite = offIcon;
		}
	}
}
