﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRoomDetection : MonoBehaviour {

	ObjectProperties op;

	// Use this for initialization
	void Start () {

		op = GetComponentInParent <ObjectProperties> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D collider)
	{ 
		if (collider.gameObject.tag == "Room") 
		{
			op.currentRoom = collider.gameObject.GetComponent<Room> (); 
		} 
	}
}
