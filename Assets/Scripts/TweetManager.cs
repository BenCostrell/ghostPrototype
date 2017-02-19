using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweetManager : MonoBehaviour {

	private SpriteRenderer[] tweetSlotArray;

	// Use this for initialization
	void Start () {
		InitializeTweetSlots ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void InitializeTweetSlots(){
		tweetSlotArray = GetComponentsInChildren<SpriteRenderer> ();
		tweetSlotArray [0].sprite = null;
		tweetSlotArray [1].sprite = null;
		tweetSlotArray [2].sprite = null;
		tweetSlotArray [3].sprite = null;

	}

	public void Tweet(Sprite newTweet){
		if (tweetSlotArray [2].sprite != null) {
			tweetSlotArray [3].sprite = tweetSlotArray [2].sprite;
		}
		if (tweetSlotArray [1].sprite != null) {
			tweetSlotArray [2].sprite = tweetSlotArray [1].sprite;
		}
		if (tweetSlotArray [0].sprite != null) {
			tweetSlotArray [1].sprite = tweetSlotArray [0].sprite;
		}
		tweetSlotArray [0].sprite = newTweet;
	}
}
