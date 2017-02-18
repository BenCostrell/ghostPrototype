using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrabber : MonoBehaviour {

	public GameObject objectInRange;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D collider){
		if ((collider.gameObject.tag == "Box") || (collider.gameObject.tag == "HouseObjects")) {
			objectInRange = collider.gameObject;
		}
	}

	void OnTriggerExit2D(Collider2D collider){
		if ((collider.gameObject.tag == "Box") || (collider.gameObject.tag == "HouseObjects")) {
			objectInRange = null;
		}
	}
}
