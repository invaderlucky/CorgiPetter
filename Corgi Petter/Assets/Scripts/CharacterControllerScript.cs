using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour {
	public float maxSpeed = 0f;
	public float offset = 0f;

	private float maxHeight;
	private float xLocked;
	private Vector2 currentTarget;
	private Vector3 touchToWorld;

	GUIStyle style;

	Animator anim;
	Rigidbody2D rigid;
	public Camera cam;

	// Use this for initialization
	void Start () {
		// Character's rigid body
		rigid = GetComponent<Rigidbody2D>();
		xLocked = rigid.position.x;
		// Character's animator
		anim = GetComponent<Animator>();
		// Set default camera to main camera
		if (cam == null) 
			cam = Camera.main;
		style = new GUIStyle ();
		style.normal.textColor = Color.black;
		touchToWorld = rigid.position;
	}
		
	void Update() {

	}

	void OnGUI () {			
		if (Time.timeScale != 0) {	
			foreach (Touch touch in Input.touches) {
				// Need to save the last finger used
				if (touch.phase == TouchPhase.Began) {
					currentTarget = touch.position; 
					touchToWorld = Camera.main.ScreenToWorldPoint (currentTarget);
				}
			}

			//GUI.Label(new Rect(130, 200, 200, 100), "Current position = " + rigid.position.x + ", " + rigid.position.y, style ); 
			//GUI.Label(new Rect(130, 300, 200, 100), "Current target = " + currentTarget.x + ", " + currentTarget.y, style );  
			//GUI.Label(new Rect(130, 500, 200, 100), "Touch to world = " + touchToWorld.x + ", " + touchToWorld.y, style ); 

			float move = 0f;
			// Ignore pause button tap
			if (touchToWorld.y > 3.5f && touchToWorld.x > 6.5f) {
				// Stop moving
				DownAnimations(false);
				UpAnimations(false);
			}
			else if (rigid.position.y > touchToWorld.y + offset) {
				// Move down
				move = -1.0f;				
				DownAnimations(true);
				UpAnimations(false);
			}
			else if (rigid.position.y < touchToWorld.y - offset) {
				// Move up
				move = 1.0f;		
				DownAnimations(false);
				UpAnimations(true);
			}
			else {
				// Stop moving
				DownAnimations(false);
				UpAnimations(false);
			}

			// Update character velocity
			rigid.velocity = new Vector2(0, move * maxSpeed);

			if (rigid.position.x != xLocked) {
				rigid.position = new Vector2 (xLocked, rigid.position.y);
			}
		}
	}

	// param = play: start the animation or not
	void UpAnimations( bool play ) {
		anim.SetBool("upPressed", play);
		anim.SetBool("upReleased", !play);
	}

	// param = play: start the animation or not
	void DownAnimations( bool play ) {
		anim.SetBool("downPressed", play);
		anim.SetBool("downReleased", !play);
	}
}
