using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostControls : MonoBehaviour {

	private Rigidbody2D rb;
	private SpriteRenderer sr;
	public float speed;
	public int playerNum;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		sr = GetComponent<SpriteRenderer> ();
	}

	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate(){
		Move ();
	}

	void Move(){
		Vector2 direction = new Vector2 (Input.GetAxis ("Horizontal_P" + playerNum), Input.GetAxis ("Vertical_P" + playerNum));

		rb.velocity = speed * direction;

		if (direction.x >= 0) {
			sr.flipX = true;
		} else {
			sr.flipX = false;
		}
	}
}
