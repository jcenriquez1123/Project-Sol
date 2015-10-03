using UnityEngine;
using System.Collections;

public class EchoController : MonoBehaviour {

	public float vanishingSpeed;
	private float alpha;
	private SpriteRenderer spRd;

	// Use this for initialization
	void Start () {
		spRd = GetComponent<SpriteRenderer> ();
		alpha = 1;
	}
	
	// Update is called once per frame
	void Update () {
		alpha -= Time.deltaTime*vanishingSpeed;
		spRd.color = new Color(spRd.color.r,spRd.color.g,spRd.color.b,alpha);
		if (alpha <= 0.1) {
			Destroy(gameObject);
		}
	}

	void OnTriggerStay2D(Collider2D otherObj){
		if (otherObj.tag == "Player" && !otherObj.GetComponentInChildren<GroundCheckController>().touchingGround) {
			GetComponent<BoxCollider2D>().isTrigger = false;
			gameObject.tag = "Ground";
		}
	}

	void OnTriggerExit2D(Collider2D otherObj){
		if (otherObj.tag == "Player" && GetComponent<BoxCollider2D>().isTrigger) {
			GetComponent<BoxCollider2D>().isTrigger = false;
			gameObject.tag = "Ground";
		}
	}
}
