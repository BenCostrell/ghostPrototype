using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomColorManager : MonoBehaviour {
	public Color blueDark; 
	public Color blueBright; 
	public Color blueLerp; 
	public Color pinkDark; 
	public Color pinkBright;
	public Color pinkLerp; 
	public Color white;
	public float lerpSpeed;
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {

		blueLerp = Color.Lerp (blueBright, blueDark, Mathf.PingPong (Time.time * lerpSpeed, 1));
		pinkLerp = Color.Lerp (pinkBright, pinkDark, Mathf.PingPong (Time.time * lerpSpeed, 1));
		
	}
}
