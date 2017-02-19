using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostControls : MonoBehaviour {

	private Rigidbody2D rb;
	private SpriteRenderer sr;
	public float speed;
	public int playerNum;
	public GameObject objectInRange; 
	ObjectProperties op; 
	private bool playerInRange;
	public float hitstunRemaining;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		sr = GetComponent<SpriteRenderer> ();
		playerInRange = false;
	}

	// Update is called once per frame
	void Update () {
		if (hitstunRemaining > 0) {
			hitstunRemaining -= Time.deltaTime;
		} else {
			ProcessInput ();
		}
	}

	void FixedUpdate(){
		if (hitstunRemaining <= 0) {
			Move ();
		}
	}

	void Move(){
		Vector2 direction = new Vector2 (Input.GetAxis ("Horizontal_P" + playerNum), Input.GetAxis ("Vertical_P" + playerNum));

		rb.velocity = speed * direction;

		if (direction.x > 0) 
		{
			sr.flipX = true;
		} 

		if (direction.x < 0) 
		{
			sr.flipX = false;
		}
	}

	void OnTriggerEnter2D (Collider2D coll)
	{
		if (coll.gameObject.tag == "HouseObjects") 
		{
			op = coll.GetComponent<ObjectProperties> ();

			if (op.ownerNum == playerNum && objectInRange == null && coll.gameObject.tag == "HouseObjects") 
			{
				objectInRange = coll.gameObject;  
			}
		}
		if (coll.gameObject.tag == "MurderBabyFan") {
			playerInRange = true;
		}


	}

	void OnTriggerExit2D (Collider2D coll)
	{
		if (coll.gameObject.tag == "HouseObjects") 
		{
			op = coll.GetComponent<ObjectProperties> ();

			if (op.ownerNum == playerNum) 
			{
				objectInRange = null;
			}
		}
		if (coll.gameObject.tag == "MurderBabyFan") {
			playerInRange = false;
		}

	
	}

	void ProcessInput(){
		if (Input.GetButtonDown ("Button1_P" + playerNum)) {
			if (playerInRange) {
				PossessPlayer ();
			}
			else if (objectInRange != null) {
				PossessObject ();
			}
		}
	}

	void PossessObject ()
	{
		op = objectInRange.GetComponent<ObjectProperties> ();
		op.possessed = true;
		Destroy (gameObject);
	}

	void PossessPlayer(){
		MurderBabyFanControls murderBabyFan = GameObject.FindGameObjectWithTag ("MurderBabyFan").GetComponent<MurderBabyFanControls> ();
		if (murderBabyFan.ownerNum == 0) {
			murderBabyFan.SetPossessionStatus (playerNum);
			Destroy (gameObject);
		}
	}
		
}
