using UnityEngine;
using System.Collections;

public class WindController : MonoBehaviour {

	public float inpulse;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D otherObj){
		if (otherObj.gameObject.tag == "Player") {
			otherObj.GetComponent<PlayerController> ().PushPlayer ();
			otherObj.attachedRigidbody.velocity = new Vector2 (-Mathf.Sin (Mathf.Deg2Rad * transform.rotation.eulerAngles.z), Mathf.Cos (Mathf.Deg2Rad * transform.rotation.eulerAngles.z)) * inpulse;
		}
	}

	public void DestroyObj(){
		Destroy (gameObject);
	}
}
