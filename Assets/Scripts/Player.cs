using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	private Rigidbody2D rb;
	public float speed;
	public int playerNum;
	private List<int> roomsClaimed;
	public Room currentRoom;
	public int maxRoomClaims;
	public Sprite neutralSprite;
	public Sprite boxHoldingSprite;
	private bool holdingBox;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		InitializeLists ();
		holdingBox = false;
	}
	
	// Update is called once per frame
	void Update () {
		ProcessInput ();
	}

	void FixedUpdate(){
		Move ();
	}

	void InitializeLists(){
		roomsClaimed = new List<int> ();
	}

	void Move(){
		Vector2 direction = new Vector2 (Input.GetAxis ("Horizontal_P" + playerNum), Input.GetAxis ("Vertical_P" + playerNum));

		rb.velocity = speed * direction;

		float angle = Mathf.Atan2 (direction.y, direction.x);
		if (direction.magnitude > 0.01f) {
			transform.rotation = Quaternion.Euler (0, 0, angle * Mathf.Rad2Deg + 90);
		} 
	}

	void ProcessInput(){
		if (Input.GetButtonDown ("Button1_P" + playerNum)) {
			if (!holdingBox && (GetComponentInChildren<ObjectGrabber>().objectInRange != null)) {
				GrabBox ();
			}
			else if (holdingBox && (currentRoom != null)) {
				if ((roomsClaimed.Count < maxRoomClaims) && !currentRoom.isClaimed && currentRoom.isClaimable) {
					ClaimRoom ();
				}
			}
		}
	}

	void GrabBox(){
		GameObject box = GetComponentInChildren<ObjectGrabber> ().objectInRange;
		holdingBox = true;
		GetComponent<SpriteRenderer> ().sprite = boxHoldingSprite;
		Destroy (box);
	}

	void ClaimRoom(){
		holdingBox = false;
		GetComponent<SpriteRenderer> ().sprite = neutralSprite;
		roomsClaimed.Add (currentRoom.id);
		currentRoom.GetClaimed (playerNum);
	}

	void OnTriggerEnter2D(Collider2D collider){
		if (collider.gameObject.tag == "Room") {
			currentRoom = collider.gameObject.GetComponent<Room> ();
		} 
	}
}
