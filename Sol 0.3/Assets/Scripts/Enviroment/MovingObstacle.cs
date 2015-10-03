using UnityEngine;
using System.Collections;

public class MovingObstacle : MonoBehaviour {

	private CheckBeat cB;
	private int beatCounter;
	private float failSafeTimer;
	public bool pushRight;
	public bool opensUp;
	public bool invertPush;
	public int beatOffset;
	public float verticalMovement;
	public NoteLength interval;

	// Use this for initialization
	void Start () {
	
		//Autohook
		cB = GetComponent<CheckBeat> ();

		beatCounter = beatOffset;
	}

	void FixedUpdate(){
		failSafeTimer += Time.fixedDeltaTime;
		
		if (cB.onBeat && failSafeTimer > 0.05 ) {
			
			failSafeTimer = 0;
			
			if(beatCounter == 0){
				if(opensUp){
					transform.position += new Vector3(0,verticalMovement,0);
				}else{
					transform.position -= new Vector3(0,verticalMovement,0);
				}
				if(invertPush){
					pushRight = !pushRight;
				}
			}
			else if(beatCounter == 1){
				if(!opensUp){
					transform.position += new Vector3(0,verticalMovement,0);
				}else{
					transform.position -= new Vector3(0,verticalMovement,0);
				}
				if(invertPush){
					pushRight = !pushRight;
				}
			}

			if(beatCounter == (8 / Mathf.Pow (2, (float)interval))-1 ){
				beatCounter = -1;
			}			
			++beatCounter;
		}
	}

	// Update is called once per frame
	void Update () {

	}

	void OnCollisionStay2D(Collision2D otherObj){
		if (otherObj.gameObject.tag == "Player") {
			otherObj.gameObject.GetComponent<PlayerController>().PushPlayer();
			if(pushRight){
				otherObj.rigidbody.velocity = new Vector2(15,5);
			}else{
				otherObj.rigidbody.velocity = new Vector2(-15,5);
			}
		}
	}
}