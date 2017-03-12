using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorgiRunScript : MonoBehaviour {
	public float speed = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeScale != 0)
			transform.Translate(Vector2.right * speed);
	}
}
