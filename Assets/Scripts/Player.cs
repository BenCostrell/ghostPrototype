using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	private Rigidbody2D rb;
	public float speed;
	public int playerNum;
	private List<int> roomsClaimed;
	private List<GameObject> objectsClaimed;
	public Room currentRoom;
	public int maxRoomClaims;
	public Sprite neutralSprite;
	public Sprite boxHoldingSprite;
	private bool holdingBox;
	private ObjectManager objectManager;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		InitializeLists ();
		holdingBox = false;
		objectManager = GameObject.FindGameObjectWithTag ("ObjectManager").GetComponent<ObjectManager>();
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
		objectsClaimed = new List<GameObject> ();
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
			if (!holdingBox && (GetComponentInChildren<ObjectGrabber>().objectInRange != null) && (roomsClaimed.Count < maxRoomClaims)) {
				GameObject objInRange = GetComponentInChildren<ObjectGrabber> ().objectInRange;
				if (objInRange.tag == "Box") {
					GrabBox (objInRange);
				} else {
					if ((objectsClaimed.Count < 3) && (objInRange.GetComponent<ObjectProperties>().ownerNum == 0)) {
						ClaimObject (objInRange);
					}
				}
			}
			else if (holdingBox && (currentRoom != null)) {
				if ((roomsClaimed.Count < maxRoomClaims) && !currentRoom.isClaimed && currentRoom.isClaimable) {
					ClaimRoom ();
				}
			}
		}
	}

	void GrabBox(GameObject box){
		holdingBox = true;
		GetComponent<SpriteRenderer> ().sprite = boxHoldingSprite;
		Destroy (box);
	}

	void ClaimObject(GameObject obj){
		objectsClaimed.Add (obj);
		objectManager.ClaimObjects (obj.GetComponent<ObjectProperties> ().objectName, playerNum);
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
