using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour {

	public Color blueHightLight; 
	public Color pinkHightLight;
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
		InitializeRoomArray ();
		AssignObjects ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void InitializeRoomArray(){
		roomArray = claimableRooms.GetComponentsInChildren<Room> ();
		foreach (Room room in roomArray) {
			room.numObjectsAssigned = 0;
		}
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
		objectList = new List<GameObject> ();
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
}
