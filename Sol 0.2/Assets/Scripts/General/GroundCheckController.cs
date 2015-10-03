using UnityEngine;
using System.Collections;

public class GroundCheckController : MonoBehaviour {

	public bool touchingGround;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerStay2D(Collider2D otherObj){
		if (otherObj.tag == "Ground") {
		if(!touchingGround){
				GetComponent<AudioSource> ().Play();
			}
			touchingGround = true;

		}
	}
	void OnTriggerExit2D(Collider2D otherObj){
		if (otherObj.tag == "Ground") {
			touchingGround = false;
		}
	}
}
