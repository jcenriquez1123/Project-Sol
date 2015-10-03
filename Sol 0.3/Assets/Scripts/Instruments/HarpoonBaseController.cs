﻿using UnityEngine;
using System.Collections;

public class HarpoonBaseController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D otherObj){
		if (otherObj.tag == "Ground") {
			GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		}
	}
}
