using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	private Rigidbody2D rb;
	public float speed;
	public int playerNum;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
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

		float angle = Mathf.Atan2 (direction.y, direction.x);
		if (direction.magnitude > 0.01f) {
			transform.rotation = Quaternion.Euler (0, 0, angle * Mathf.Rad2Deg + 90);
		} 
	}
}
