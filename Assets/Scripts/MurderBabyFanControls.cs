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
	public GameObject ghostP1;
	public GameObject ghostP2;
	public float launchSpeed;
	public float launchStun;
	private Room currentRoom;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (ownerNum != 0) {
			ProcessInput ();
			CheckForSpoop ();
		}
		CheckForFill ();
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

	void ProcessInput(){
		if (Input.GetButtonDown ("Button1_P" + ownerNum)) {
			UnPossess (false);
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

	void UnPossess(bool withStun){
		if (ownerNum != 0) {
			GameObject ghost = null;
			if (ownerNum == 1) {
				ghost = Instantiate (ghostP1, transform.position, Quaternion.identity) as GameObject;
			} else if (ownerNum == 2) {
				ghost = Instantiate (ghostP2, transform.position, Quaternion.identity) as GameObject;
			}
			if (withStun) {
				float xLaunch = Random.Range (-1, 1);
				float yLaunch = Random.Range (-1, 1);
				Vector3 launchDirection = new Vector3 (xLaunch, yLaunch, 0);
				ghost.GetComponent<Rigidbody2D> ().velocity = launchDirection.normalized * launchSpeed;
				ghost.GetComponent<GhostControls> ().hitstunRemaining = launchStun;
			} else {
				ghost.GetComponent<Rigidbody2D> ().velocity = Vector3.zero;
			}
			rb.velocity = Vector3.zero;
			SetPossessionStatus (0);
		}
	}

	void CheckForSpoop(){
		if ((currentRoom.isSpoopyforBlue && (ownerNum == 1)) || (currentRoom.isSpoopyforPink && (ownerNum == 2))){
			UnPossess (true);
		}
	}

	void CheckForFill(){
		if (currentRoom != null) {
			if ((currentRoom.ownerNum == ownerNum) && (currentRoom.fillProportion < 1)){
				currentRoom.IncrementFillProportion ();
			}
		}
	}

	void OnTriggerEnter2D(Collider2D collider){
		if (collider.gameObject.tag == "Room") {
			currentRoom = collider.gameObject.GetComponent<Room> ();
		} 
	}
}
