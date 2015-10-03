using UnityEngine;
using System.Collections;

public class SliderEffect : MonoBehaviour {

	public float downwardInpulse;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//void OnCollisionEnxit2D(Collision2D otherObj){
	//	if (otherObj.gameObject.tag == "Player") { 
	//		GetComponent<AudioSource>().Stop();
	//	}
	//}

	void OnCollisionStay2D(Collision2D otherObj){
		if(otherObj.gameObject.tag == "Player"){ 
			//if(!GetComponent<AudioSource>().isPlaying){
			//	GetComponent<AudioSource>().Play();
			//}
			PlayerController player = otherObj.gameObject.GetComponent<PlayerController>();
			GroundCheckController groundCheck = player.GetComponentInChildren<GroundCheckController>();
			bool isGrounded = groundCheck.touchingGround;
			if(!isGrounded){ 
				otherObj.rigidbody.velocity = new Vector2(otherObj.rigidbody.velocity.x,-downwardInpulse);
			}
		}
	}
}
