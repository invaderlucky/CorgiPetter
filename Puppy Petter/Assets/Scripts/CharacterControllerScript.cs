using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour {
	public float maxSpeed = 0f;
	private float maxHeight;

	Animator anim;
	Rigidbody2D rigid;
	public Camera cam;

	// Use this for initialization
	void Start () {// Character's rigid body
		rigid = GetComponent<Rigidbody2D>();
		// Character's animator
		anim = GetComponent<Animator>();
		// Set default camera to main camera
		if (cam == null) 
			cam = Camera.main;
	}

	// Update is called once per frame
/*	void Update () {
		// Up and down arrows input
		float move = Input.GetAxis("Vertical");

		// Update animator speed
		anim.SetFloat("speed", move);

		// Update character velocity
		rigid.velocity = new Vector2(0, move * maxSpeed);
	}
*/
	void Update() {
		if (Time.timeScale != 0) {
			float move = Input.GetAxis("Vertical");

			if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) {
				anim.SetBool("upPressed", true);
				anim.SetBool("upReleased", false);
			}
			if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) {
				anim.SetBool("downPressed", true);
				anim.SetBool("downReleased", false);
			}
			if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W)) {
				anim.SetBool("upReleased", true);
				anim.SetBool("upPressed", false);
			}
			if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S)) {
				anim.SetBool("downReleased", true);
				anim.SetBool("downPressed", false);
			}
			
			// Update character velocity
			rigid.velocity = new Vector2(0, move * maxSpeed);
		}
	}
}
