using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour {

	public Color blueHightLight; 
	public Color pinkHightLight;
	public Color used; 
	public Color white;

	public GameObject bookPrefab;
	public GameObject lampPrefab;
	public GameObject laptopPrefab;
	public GameObject palettePrefab;
	public GameObject synthPrefab;
	public GameObject teddyPrefab;

	private List<GameObject> bookList;
	private List<GameObject> lampList;
	private List<GameObject> laptopList;
	private List<GameObject> paletteList;
	private List<GameObject> synthList;
	private List<GameObject> teddyList;

	private GameObject claimableRooms;

	private Room[] roomArray;

	// Use this for initialization
	void Start () {
		claimableRooms = GameObject.FindGameObjectWithTag ("ClaimableRoomsObj");
		InitializeArraysAndLists ();
		AssignObjects ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void InitializeArraysAndLists(){
		roomArray = claimableRooms.GetComponentsInChildren<Room> ();
		foreach (Room room in roomArray) {
			room.numObjectsAssigned = 0;
		}
		bookList = new List<GameObject> ();
		lampList = new List<GameObject> ();
		laptopList = new List<GameObject> ();
		paletteList = new List<GameObject> ();
		synthList = new List<GameObject> ();
		teddyList = new List<GameObject> ();
	}

	void AssignObjects(){
		AssignObjectsOfType (bookPrefab, bookList, 2);
		AssignObjectsOfType (lampPrefab, lampList, 2);
		AssignObjectsOfType (laptopPrefab, laptopList, 2);
		AssignObjectsOfType (palettePrefab, paletteList, 2);
		AssignObjectsOfType (synthPrefab, synthList, 2);
		AssignObjectsOfType (teddyPrefab, teddyList, 2);
	}

	Room GenerateRandomRoom(){
		int roomIndex = Random.Range (0, roomArray.Length);
		return roomArray [roomIndex];
	}

	bool ValidateRoom(Room room){
		if (room.numObjectsAssigned < 2) {
			return true;
		} else {
			return false;
		}
	}

	void AssignObjectsOfType(GameObject objectPrefab, List<GameObject> objectList, int numObjects){
		for (int i = 0; i < numObjects; i++) {
			Room room = GenerateRandomRoom ();
			bool isValid = ValidateRoom (room);
			while (!isValid) {
				room = GenerateRandomRoom ();
				isValid = ValidateRoom (room);
			}
			Vector3 location;
			if (room.numObjectsAssigned == 0) {
				location = room.GetComponentsInChildren<Transform> () [1].position;
			} else {
				location = room.GetComponentsInChildren<Transform> () [2].position;
			}

			GameObject obj = Instantiate (objectPrefab, location, Quaternion.identity) as GameObject;
			objectList.Add (obj);
			room.numObjectsAssigned += 1;
		}
	}

	public void ClaimObjects(string objectType, int playerNum){
		List <GameObject> objectsToClaim = new List<GameObject> ();
		if (objectType == "Book") {
			objectsToClaim = bookList;
		} else if (objectType == "Lamp") {
			objectsToClaim = lampList;
		} else if (objectType == "Laptop") {
			objectsToClaim = laptopList;
		} else if (objectType == "Palette") {
			objectsToClaim = paletteList;
		} else if (objectType == "Synth") {
			objectsToClaim = synthList;
		} else if (objectType == "Teddy") {
			objectsToClaim = teddyList;
		}

		foreach (GameObject obj in objectsToClaim) {
			obj.GetComponent<ObjectProperties> ().ownerNum = playerNum;
		}
	}
}
