using UnityEngine;
using System.Collections;

public class SolController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay2D(Collider2D otherObj){
		if (otherObj.name == "Player" && GetComponent<CheckBeat>().onBeat) {
			Application.LoadLevel(Application.loadedLevel+1);
		}
	}
}
