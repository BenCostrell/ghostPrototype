using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MurderBabyFanControls : MonoBehaviour {

	public int ownerNum;
	private Rigidbody2D rb;
	public float speed;
	public Sprite neutral;
	public Sprite posP1;
	public Sprite posP2;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate(){
		if (ownerNum != 0) {
			Move ();
		}
	}

	void Move(){
		Vector2 direction = new Vector2 (Input.GetAxis ("Horizontal_P" + ownerNum), Input.GetAxis ("Vertical_P" + ownerNum));

		rb.velocity = speed * direction;

		float angle = Mathf.Atan2 (direction.y, direction.x);
		if (direction.magnitude > 0.01f) {
			transform.rotation = Quaternion.Euler (0, 0, angle * Mathf.Rad2Deg + 90);
		} 
	}

	public void SetPossessionStatus(int playerNum){
		ownerNum = playerNum;
		if (playerNum == 0) {
			GetComponent<SpriteRenderer> ().sprite = neutral;
		} else if (playerNum == 1) {
			GetComponent<SpriteRenderer> ().sprite = posP1;
		} else if (playerNum == 2) {
			GetComponent<SpriteRenderer> ().sprite = posP2;
		}
	}
}
