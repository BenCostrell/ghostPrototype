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

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		sr = GetComponent<SpriteRenderer> ();
	}

	// Update is called once per frame
	void Update () {

		possessObject ();

	}

	void FixedUpdate(){
		Move ();
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
		}

		if (op.ownerNum == playerNum && objectInRange == null) 
		{
			objectInRange = coll.gameObject;  
		}
	}

	void OnTriggerExit2D (Collider2D coll)
	{
		if (coll.gameObject.tag == "HouseObjects") 
		{
			op = coll.GetComponent<ObjectProperties> ();
		}

		if (op.ownerNum == playerNum) 
		{
			objectInRange = null;
		}
	}

	void possessObject ()
	{
		if (objectInRange != null) 
		{
			op = objectInRange.GetComponent<ObjectProperties> ();
		} 

		if (Input.GetButtonDown ("Button1_P" + playerNum) && objectInRange != null) 
		{ 
			op.possessed = true;
			Destroy (this.gameObject);
			Debug.Log ("This is working?");
		}
	}
		
}
