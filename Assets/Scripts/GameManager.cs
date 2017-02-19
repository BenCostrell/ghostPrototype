using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public GameObject playerPrefab;
	public GameObject boxPrefab;
	public float boxSpawnX;
	public float boxSpawnLowestY;
	public float boxSpawnSpacing;
	public Vector3 spawnP1;
	public Vector3 spawnP2;
	public Sprite neutralSpriteP1;
	public Sprite neutralSpriteP2;
	public Sprite boxHoldingSpriteP1;
	public Sprite boxHoldingSpriteP2;
	private GameObject player1;
	private GameObject player2;
	public GameObject ghostPrefabP1;
	public GameObject ghostPrefabP2;
	public GameObject murderBabyFanPrefab;
	public Vector3 murderBabyFanSpawn;
	private bool player1Ready;
	private bool player2Ready;
	private bool ghostPhase;

	// Use this for initialization
	void Start () {
		InitializePlayers();
		InitializeBoxes ();
		player1Ready = false;
		player2Ready = false;
		ghostPhase = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Reset")) {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
		}
		if (player1Ready && player2Ready && !ghostPhase) {
			ItsMurderBabyTime ();
			ghostPhase = true;
		}
	}

	void InitializePlayers(){
		player1 = InitializePlayer (1, neutralSpriteP1, boxHoldingSpriteP1, spawnP1);
		player2 = InitializePlayer (2, neutralSpriteP2, boxHoldingSpriteP2, spawnP2);
	}

	GameObject InitializePlayer(int playerNum, Sprite neutral, Sprite boxHolding, Vector3 spawn){
		GameObject player = Instantiate (playerPrefab, spawn, Quaternion.identity) as GameObject;
		Player ps = player.GetComponent<Player> ();
		ps.playerNum = playerNum;
		ps.neutralSprite = neutral;
		ps.boxHoldingSprite = boxHolding;
		player.GetComponent<SpriteRenderer> ().sprite = neutral;
		return player;
	}

	void InitializeBoxes(){
		for (int i = 0; i < 12; i++) {
			Instantiate (boxPrefab, new Vector3 (boxSpawnX, boxSpawnLowestY + i * boxSpawnSpacing, 0), Quaternion.identity);
		}
	}

	public void PlayerReady(int playerNum){
		if (playerNum == 1) {
			player1Ready = true;
		} else if (playerNum == 2) {
			player2Ready = true;
		}
	}

	public void ItsMurderBabyTime(){
		Destroy (player1);
		Destroy (player2);
		InitializeGhosts ();
		InitializeMurderBabyFan ();
	}

	void InitializeGhosts(){
		Instantiate (ghostPrefabP1, spawnP1, Quaternion.identity);
		Instantiate (ghostPrefabP2, spawnP2, Quaternion.identity);
	}

	void InitializeMurderBabyFan(){
		Instantiate (murderBabyFanPrefab, murderBabyFanSpawn, Quaternion.identity);
	}
}
