using UnityEngine;
using System.Collections;

public class HarpoonController : MonoBehaviour {

//	private PlayerController player;

	// Use this for initialization
	void Start () {
	//	player = FindObjectOfType<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.DrawLine (transform.position, player.transform.position, Color.red);
	}

	void OnTriggerEnter2D(Collider2D otherObj){
		if (otherObj.name != "Player") {
			GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
		}
	}
}
