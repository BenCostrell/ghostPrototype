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
	public bool isSpoopyforPink;
	public bool isSpoopyforBlue;
	public int spoopTimer; 
	public int spoopLimit = 10; 
	public int whoSpooped; 
	public int numObjectsAssigned;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		spoopEnder ();
		
	}

	public void GetClaimed(int playerNum){
		isClaimed = true;
		if (playerNum == 1) {
			GetComponent<SpriteRenderer> ().sprite = claimedByP1;
		} else if (playerNum == 2) {
			GetComponent<SpriteRenderer> ().sprite = claimedByP2;
		}
	}

	public void getSpoopy (int ownerNum) {
		if (ownerNum == 1) 
		{
			isSpoopyforPink = true;
		}

		if (ownerNum == 2) 
		{
			isSpoopyforBlue = true;
		}
	}

	void spoopEnder () 
	{
		if (isSpoopyforPink == true || isSpoopyforBlue == true) 
		{
			spoopTimer++;
		}

		if (spoopTimer >= spoopLimit) 
		{
			spoopTimer = 0;
			isSpoopyforBlue = false;
			isSpoopyforPink = false; 
		}
		
	}
}
