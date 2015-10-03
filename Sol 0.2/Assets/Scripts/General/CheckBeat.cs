using UnityEngine;
using System.Collections;

public class CheckBeat : MonoBehaviour {
	
	//Parameter
	public NoteLength minNoteLengthRequiered;
	
	//Calculation Variables
	private float timeSinceLastNote;
	private float targetTimeInterval;

	//Output
	public bool onBeat;
	
	void Awake(){
		
	}
	
	// Use this for initialization
	void Start () {
		float bps = FindObjectOfType<LevelData>().bpm/60.0f; // BPS
		float wps = bps / 4;
		float timeBetweenWholes = 1 / wps;
		targetTimeInterval = timeBetweenWholes / Mathf.Pow(2,(float)minNoteLengthRequiered);
		timeSinceLastNote = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate(){
		timeSinceLastNote += Time.fixedDeltaTime;
		if (timeSinceLastNote > targetTimeInterval - 0.01) {
			onBeat = true;
			timeSinceLastNote = timeSinceLastNote - targetTimeInterval;
		} else {
			onBeat = false;
		}
	}
}