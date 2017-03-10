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
	void Start () {
		// Character's rigid body
		rigid = GetComponent<Rigidbody2D>();
		// Character's animator
		anim = GetComponent<Animator>();

		// Set default camera to main camera
		if (cam == null) 
			cam = Camera.main;

		// Bounds checking
		//Vector3 upper = new Vector3 (Screen.width, Screen.height, 0);
		//Vector3 targetHeight = cam.ScreenToWorldPoint (upper);
		//maxHeight = targetHeight.y;
	}

/* 
	void FixedUpdate() {
		Vector3 rawPosition = cam.ScreenToWorldPoint (Input.mousePosition);
		Vector3 targetPosition = new Vector3 (0, rawPosition.y, 0);
		rigid.MovePosition (targetPosition);
	}
*/	

	// Update is called once per frame
	void Update () {
		// Up and down arrows input
		float move = Input.GetAxis("Vertical");

		// Update animator speed
		anim.SetFloat("speed", move);

		// Update character velocity
		rigid.velocity = new Vector2(rigid.velocity.x, move * maxSpeed);
	}
}
