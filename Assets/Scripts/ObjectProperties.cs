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

	SpriteRenderer sr;


	// Use this for initialization
	void Start () {

		sr = GetComponent<SpriteRenderer> ();

		
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

	}
}
