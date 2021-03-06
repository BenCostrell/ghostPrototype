﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectProperties : MonoBehaviour {

	public int ownerNum;
	public bool possessed;
	public bool onCoolDown;

	public Sprite regular;
	public Sprite blue;
	public Sprite pink;
	public GameObject blueGhost;
	public GameObject pinkGhost;
	public GameObject spoopRing;
	public Room currentRoom;
	public int coolDownTimer = 0;
	public int coolDownLimit = 100;

	public string objectName;

	public float speed;

	GameObject objectManager;
	ObjectManager om;

	SpriteRenderer sr;
	Rigidbody2D rb;


	// Use this for initialization
	void Start () {

		sr = GetComponent<SpriteRenderer> ();
		objectManager = GameObject.Find ("ObjectManager");
		om = objectManager.GetComponent<ObjectManager> ();
		rb = GetComponent<Rigidbody2D> ();
		ownerNum = 0;
		possessed = false;
	}
	
	// Update is called once per frame
	void Update () {
		determineSprite ();
		possessedControls ();
		getSpooped ();
		coolDownRefresh ();
		
	}

	void determineSprite ()
	{
		if (possessed == true) 
		{
			if (ownerNum == 1) 
			{
				sr.sprite = blue;
			} 
			if (ownerNum == 2) 
			{
				sr.sprite = pink; 
			}
		} 
		else 
		{
			sr.sprite = regular; 
		}

		if (sr.sprite == regular && onCoolDown == false) {
			if (ownerNum == 1) {
				sr.color = om.blueHightLight;
			} else if (ownerNum == 2) {
				sr.color = om.pinkHightLight; 
			} else {
				sr.color = om.white; 
			}
		}

		else 
		{
			sr.color = om.white;
		} 

		if (onCoolDown == true) 
		{
			sr.color = om.used;
		}

	}

	void possessedControls ()
	{
		if (possessed == true) 
		{
			Vector2 direction = new Vector2 (Input.GetAxis ("Horizontal_P" + ownerNum), Input.GetAxis ("Vertical_P" + ownerNum));

			rb.velocity = speed * direction;

			if (direction.x > 0) 
			{
				sr.flipX = true;
			} 

			if (direction.x < 0) 
			{
				sr.flipX = false;
			}

			if (Input.GetButtonDown ("Button1_P" + ownerNum)) 
			{
				if (ownerNum == 1)
				{ 
					possessed = false;
					rb.velocity = new Vector2 (0,0);
					Instantiate (blueGhost, transform.position, Quaternion.identity);
				}

				if (ownerNum == 2) 
				{
					possessed = false;
					rb.velocity = new Vector2 (0,0);
					Instantiate (pinkGhost, transform.position, Quaternion.identity); 
				}
			}

			if (Input.GetButtonDown ("Button2_P" + ownerNum) && onCoolDown == false) 
			{
				Instantiate (spoopRing, transform.position, Quaternion.identity);
				currentRoom.getSpoopy (ownerNum);
				onCoolDown = true;
			}

		}
	}

	void getSpooped () 
	{
		if (currentRoom != null) {
			if (currentRoom.isSpoopyforBlue == true && ownerNum == 1 && possessed == true) { 
				possessed = false;
				rb.velocity = new Vector2 (0, 0);
				Instantiate (blueGhost, transform.position, Quaternion.identity);
			}

			if (currentRoom.isSpoopyforPink && ownerNum == 2 && possessed == true) {
				possessed = false;
				rb.velocity = new Vector2 (0, 0);
				Instantiate (pinkGhost, transform.position, Quaternion.identity); 
			}
		}
	}

	void coolDownRefresh ()
	{
		if (onCoolDown == true) 
		{
			coolDownTimer++;
		}

		if (coolDownTimer >= coolDownLimit) 
		{
			coolDownTimer = 0;
			onCoolDown = false; 
		}

	}
		
}
