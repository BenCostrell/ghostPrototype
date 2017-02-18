using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectProperties : MonoBehaviour {

	public bool ownedByBlue;
	public bool owndedByPink;
	public bool possessedByBlue;
	public bool possessedByPink;

	public Sprite regular;
	public Sprite blue;
	public Sprite pink;

	public string objectName;

	GameObject objectManager;
	ObjectManager om;

	SpriteRenderer sr;


	// Use this for initialization
	void Start () {

		sr = GetComponent<SpriteRenderer> ();
		objectManager = GameObject.Find ("ObjectManager");
		om = objectManager.GetComponent<ObjectManager> ();

		
	}
	
	// Update is called once per frame
	void Update () {
		determineSprite ();
		
	}

	void determineSprite (){

		if (possessedByBlue == true) {
			sr.sprite = blue;
		} else if (possessedByPink == true) {
			sr.sprite = pink; 
		} else {
			sr.sprite = regular;
		}

		if (sr.sprite == regular) {
			if (ownedByBlue == true) {
				sr.color = om.blueHightLight;
			} else if (owndedByPink == true) {
				sr.color = om.pinkHightLight;
			} else {
				sr.color = om.white;
			}

		}
		else {
			sr.color = om.white;
		} 

	}
}
