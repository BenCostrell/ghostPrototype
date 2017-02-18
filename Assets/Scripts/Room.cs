using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

	public int id;
	public Sprite neutral;
	public Sprite claimedByP1;
	public Sprite claimedByP2;
	public bool isClaimed;
	public bool isClaimable;
	public int numObjectsAssigned;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void GetClaimed(int playerNum){
		isClaimed = true;
		if (playerNum == 1) {
			GetComponent<SpriteRenderer> ().sprite = claimedByP1;
		} else if (playerNum == 2) {
			GetComponent<SpriteRenderer> ().sprite = claimedByP2;
		}
	}
}
