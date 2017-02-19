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
	public int spoopTimer; 
	public int spoopLimit = 10; 
	public int whoSpooped; 
	public int numObjectsAssigned;
	public int ownerNum;
	public float fillProportion;
	public float fillRate;

	public Color blueDark; 
	public Color blueBright; 
	public Color blueLerp; 
	public Color pinkDark; 
	public Color pinkBright;
	public Color pinkLerp; 

	// Use this for initialization
	void Start () {
		ownerNum = 0;
		fillProportion = 0;
	}
	
	// Update is called once per frame
	void Update () {

		spoopEnder ();
		
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
		if (fillProportion >= 1) {
			Debug.Log ("finished claiming for player " + ownerNum);
		}
	}


}