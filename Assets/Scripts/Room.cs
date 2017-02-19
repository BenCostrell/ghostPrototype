using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

	public int id;
	public Sprite neutral;
	public Sprite claimedByP1;
	public Sprite claimedByP2;
	public bool isClaimable;
	public bool isSpoopyforPink;
	public bool isSpoopyforBlue; 
	public bool isEmpty = true;
	public int spoopTimer; 
	public int spoopLimit = 10; 
	public int whoSpooped; 
	public int numObjectsAssigned;
	public int ownerNum;
	public float fillProportion;
	public float fillRate;
	public Sprite tweet;
	SpriteRenderer sr;
	GameObject colorManager;
	RoomColorManager rcm;
	private TweetManager tweetManager;



	// Use this for initialization
	void Start () {
		ownerNum = 0;
		fillProportion = 0;
		sr = GetComponent<SpriteRenderer> ();

		colorManager = GameObject.Find ("RoomColorManager");
		rcm = colorManager.GetComponent<RoomColorManager> ();
		tweetManager = GameObject.FindGameObjectWithTag ("TweetManager").GetComponent<TweetManager>();
	}
	
	// Update is called once per frame
	void Update () { 

		spoopEnder ();
		stopFlash ();
		
	}

	public void GetClaimed(int playerNum){
		ownerNum = playerNum;
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

	public void IncrementFillProportion(){
		fillProportion += fillRate;
		isEmpty = false;
		if (ownerNum == 1) 
		{
			sr.color = rcm.blueLerp;
		}
		if (ownerNum == 2) 
		{
			sr.color = rcm.pinkLerp;
		}
		if (fillProportion >= 1) {
			Debug.Log ("finished claiming for player " + ownerNum);
			if (ownerNum == 1) 
			{
				sr.color = rcm.blueBright;
			}
			if (ownerNum == 2) 
			{
				sr.color = rcm.pinkBright;
			}
			tweetManager.Tweet (tweet);
		}
	}

	void OnTriggerExit2D(Collider2D coll)
	{
		if (isClaimable && coll.gameObject.tag == "MurderBabyFan") 
		{
			if (fillProportion < 1) 
			{
				isEmpty = true;
			}
		}
	}

	void stopFlash ()
	{
		if (isEmpty && isClaimable)
		{
			sr.color = rcm.white;
		}
	}
}