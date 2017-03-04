using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tweet : MonoBehaviour {

	public float lifetime;
	private float timeElapsed;
	private SpriteRenderer sr;

	// Use this for initialization
	void Start () {
		Destroy (gameObject, lifetime);
		sr = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, Mathf.Lerp (1, 0, timeElapsed / lifetime));
		transform.localScale = Vector3.one * Mathf.Lerp (2, 0.5f, timeElapsed / lifetime);
		timeElapsed += Time.deltaTime;
	}
}
