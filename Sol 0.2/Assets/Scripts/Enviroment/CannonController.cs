using UnityEngine;
using System.Collections;

public class CannonController : MonoBehaviour {

	public GameObject projectile;
	public float inpulse;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate () {
		if (GetComponent<CheckBeat> ().onBeat) {
			GameObject fired = Instantiate(projectile);
			fired.transform.position = transform.position + new Vector3(-Mathf.Sin(Mathf.Deg2Rad*transform.rotation.eulerAngles.z)/2, Mathf.Cos(Mathf.Deg2Rad*transform.rotation.eulerAngles.z)/2,0);
			fired.transform.rotation = transform.rotation;
			fired.transform.localScale = transform.localScale;
			fired.GetComponent<Rigidbody2D>().velocity = new Vector2(-Mathf.Sin(Mathf.Deg2Rad*transform.rotation.eulerAngles.z), Mathf.Cos(Mathf.Deg2Rad*transform.rotation.eulerAngles.z)) * inpulse;
		}
	}
}
