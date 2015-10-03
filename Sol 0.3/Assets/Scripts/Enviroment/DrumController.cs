using UnityEngine;
using System.Collections;

public class DrumController : MonoBehaviour {

	private CheckBeat cB;
	//private float failSafeTimer;

	//Animation Variables
	private Animator anim;
	private float animTimer;
	public float inpulse;

	// Use this for initialization
	void Start () {

		//Autohook
		cB = GetComponent<CheckBeat> ();
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

		//Animation Updates
		animTimer += Time.deltaTime;
		//failSafeTimer += Time.fixedDeltaTime;
		
		if (cB.onBeat && animTimer > 0.05 ) {
			
			//failSafeTimer = 0;
			animTimer = 0;
			anim.Play("DrumAnim",-1,0.0f);
		}
		anim.SetBool ("OnBeat", cB.onBeat);
		anim.SetFloat ("TimePlaying", animTimer);
	}

	void OnTriggerStay2D(Collider2D otherObj){

	
		//Effect On Player
		if (otherObj.gameObject.tag == "Player" && cB.onBeat) {
			GetComponent<AudioSource>().Play();
			otherObj.GetComponent<PlayerController>().PushPlayer();
			otherObj.attachedRigidbody.velocity = new Vector2(-Mathf.Sin(Mathf.Deg2Rad*transform.rotation.eulerAngles.z), Mathf.Cos(Mathf.Deg2Rad*transform.rotation.eulerAngles.z)) * inpulse;
		}
	}
}