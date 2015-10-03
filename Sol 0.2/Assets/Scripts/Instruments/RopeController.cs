using UnityEngine;
using System.Collections;

public class RopeController : MonoBehaviour {

	private HarpoonController harpoon;
	private PlayerController player;
	private bool moving;
	private GameObject hpBase;

	public GameObject harpoonBase;

	// Use this for initialization
	void Start () {
		moving = true;
		harpoon = FindObjectOfType<HarpoonController> ();
		player = FindObjectOfType<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (moving) {
			if (harpoon.GetComponent<Rigidbody2D> ().velocity.x > 1 || harpoon.GetComponent<Rigidbody2D> ().velocity.x < -1) {
				transform.position = player.transform.position + (harpoon.transform.position - player.transform.position) / 2;
				transform.position = new Vector3 (transform.position.x, transform.position.y - 0.025f, 0);
				transform.localScale = new Vector3 (Vector3.Distance (player.transform.position, harpoon.transform.position) / 2.5f, 1, 1);
				transform.eulerAngles = new Vector3 (0, 0, Mathf.Atan2 (player.transform.position.y - harpoon.transform.position.y, player.transform.position.x - harpoon.transform.position.x) * 180 / Mathf.PI);
			} else {
				moving = false;
				hpBase = Instantiate (harpoonBase);
				hpBase.transform.position = player.transform.position;
				hpBase.GetComponent<Rigidbody2D>().velocity = new Vector2 (0, -5);
			}
		} else {		
			transform.position = hpBase.transform.position + (harpoon.transform.position - hpBase.transform.position)/2;
			transform.eulerAngles = new Vector3(0,0,Mathf.Atan2(harpoon.transform.position.y -  hpBase.transform.position.y, harpoon.transform.position.x - hpBase.transform.position.x)*180 / Mathf.PI);
			transform.localScale = new Vector3 (Vector3.Distance (hpBase.transform.position, harpoon.transform.position) / 2.5f, 1, 1);
		}
		if (!moving && hpBase.GetComponent<Rigidbody2D> ().velocity.y == 0) {
			gameObject.layer = 11;
			gameObject.tag = "Ground";
		}
	}

}
