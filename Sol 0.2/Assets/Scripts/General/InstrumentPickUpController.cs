using UnityEngine;
using System.Collections;

public class InstrumentPickUpController : MonoBehaviour {

	public Instruments instrumentPowerUp;
	public GameObject effect;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D otherObj){
		if (otherObj.tag == "Player") {
			otherObj.GetComponent<PlayerController>().playerInstrument = instrumentPowerUp;
			Instantiate(effect).transform.position = transform.position;
		}
	}
}
