﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public GameObject playerPrefab;
	public GameObject boxPrefab;
	public Vector3 spawnP1;
	public Vector3 spawnP2;
	public Sprite neutralSpriteP1;
	public Sprite neutralSpriteP2;
	public Sprite boxHoldingSpriteP1;
	public Sprite boxHoldingSpriteP2;

	// Use this for initialization
	void Start () {
		InitializePlayers();
		InitializeBoxes ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void InitializePlayers(){
		InitializePlayer (1, neutralSpriteP1, boxHoldingSpriteP1, spawnP1);
		InitializePlayer (2, neutralSpriteP2, boxHoldingSpriteP2, spawnP2);
	}

	void InitializePlayer(int playerNum, Sprite neutral, Sprite boxHolding, Vector3 spawn){
		GameObject player = Instantiate (playerPrefab, spawn, Quaternion.identity) as GameObject;
		Player ps = player.GetComponent<Player> ();
		ps.playerNum = playerNum;
		ps.neutralSprite = neutral;
		ps.boxHoldingSprite = boxHolding;
		player.GetComponent<SpriteRenderer> ().sprite = neutral;
	}

	void InitializeBoxes(){
		for (int i = 0; i < 6; i++) {
			Instantiate (boxPrefab, new Vector3 (6.5f, -1f + i * 0.6f, 0), Quaternion.identity);
			Instantiate (boxPrefab, new Vector3 (7f, -1f + i * 0.6f, 0), Quaternion.identity);
		}
	}
}