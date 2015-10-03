using UnityEngine;
using System.Collections;

public enum Instruments{None, Parachute, Boots, Harpoon, Echo };

public class PlayerController : MonoBehaviour {

	//General
	private Rigidbody2D rb2d;
	public float moveAcceleration;
	private Animator anim;
	private GroundCheckController gCheck;
	public Instruments playerInstrument;
	private bool inputEnabled;
	public float topXSpeed;
	public float topYSpeed;

	//Sounds
	private AudioSource[] sounds;
	//Flutes 0 to 3
	//Power chords 4 to 6
	//Trumpet 7

	//Parachute
	public float jumpSpeed;
	private float jumpTimer;
	public float jumpCoolDown;
	private int jumpCount;
	private bool justJumped;

	//Boots
	public float dashSpeed;
	private float dashTimer;
	public float dashCoolDown;
	private bool isDashing;
	public float dashDuration;

	//Harpoon
	public float harpoonSpeed;
	private float harpoonTimer;
	public float harpoonCoolDown;
	public GameObject harpoonProj;
	public GameObject harpoonRope;

	//Echo
	private float echoTimer;
	public float echoCoolDown;
	public GameObject echoPrefab;

	//Enviroment
	private float pushedTimer;
	private bool beingPushed;
	public float pushedDuration;


	// Use this for initialization
	void Start () {
	
		//General
		inputEnabled = true;

		//Autohook
		anim = GetComponent<Animator>();
		rb2d = GetComponent<Rigidbody2D> ();
		gCheck = GetComponentInChildren<GroundCheckController> ();
		sounds = GetComponents<AudioSource> ();

		// Initialize Ability Timers
		jumpTimer = jumpCoolDown;
		dashTimer = dashCoolDown;
		harpoonTimer = harpoonCoolDown;
		echoTimer = echoCoolDown;
	}

	//Physics calculation
	void FixedUpdate(){

		//General Movement
		if (inputEnabled) {

			//If velocity isnt getting increased by something else
			if(Mathf.Abs(rb2d.velocity.x) <= moveAcceleration){
				float hAxis = Input.GetAxis ("Horizontal");	
				rb2d.velocity = new Vector2 (moveAcceleration * hAxis, rb2d.velocity.y);

				if (rb2d.velocity.x > 0) {
					transform.localScale = new Vector3 (1, 1, 1);
				} else if (rb2d.velocity.x < 0) {
					transform.localScale = new Vector3 (-1, 1, 1);
				}
			}
		}
		//Vertical Speed Cap
		if(rb2d.velocity.y > topYSpeed){
			rb2d.velocity = new Vector2(rb2d.velocity.x,topYSpeed);
		}else if (rb2d.velocity.y < -topYSpeed){
			rb2d.velocity = new Vector2(rb2d.velocity.x,-topYSpeed);
		}

		//One Way Platforms Code
		Physics2D.IgnoreLayerCollision(8,11,(rb2d.velocity.y > 1 && Mathf.Abs(rb2d.velocity.x) < 5 ) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S));
	}
	
	// Update is called once per frame
	void Update () {

		//General Animations
		anim.SetFloat ("Speed", Mathf.Abs (rb2d.velocity.x));
		anim.SetBool ("IsGrounded", gCheck.touchingGround);

		//Play Instrument
		if(Input.GetKeyDown(KeyCode.L)){
			UseInstrument();
		}

		if(Input.GetKeyDown(KeyCode.Space)){
			if(gCheck.touchingGround){
				RegularJump();
			}
		}

		//Instrument's Updates
		switch(playerInstrument){

		case Instruments.Parachute:

			jumpTimer += Time.deltaTime;
			if(gCheck.touchingGround && jumpTimer > 0.05f){
				jumpCount = 0;
			}
			break;

		case Instruments.Boots:

			dashTimer += Time.deltaTime;

			if(isDashing){
				if(dashTimer > dashDuration){
					StopDashing();
				}
				else{
					if(transform.localScale.x > 0){
						rb2d.velocity = new Vector2 (dashSpeed,0);
					}
					else{
						rb2d.velocity = new Vector2 (-dashSpeed,0);
					}
				}
			}
			break;

		case Instruments.Harpoon:
			harpoonTimer += Time.deltaTime;
			break;

		case Instruments.Echo:
			echoTimer += Time.deltaTime; 
			break;

		}

		//Enviroment Updates
		if (beingPushed) {
			pushedTimer += Time.deltaTime;
			if(pushedTimer > pushedDuration){
				StopPushing();
			}
		}
	}

	void RegularJump(){
		rb2d.velocity = new Vector2(rb2d.velocity.x,jumpSpeed);
		sounds [7].Play ();
	}

	void UseInstrument(){
		switch(playerInstrument){
		case Instruments.Parachute:
			if(jumpCount < 4 && jumpTimer > jumpCoolDown){
				anim.Play("PlayerJumping", -1, 0f);
				ParachuteJump();
			}
			break;
		case Instruments.Boots:
			if(dashTimer > dashCoolDown){
				BootsDash();
			}
			break;
		case Instruments.Harpoon:
			if(harpoonTimer > harpoonCoolDown){
				FireHarpoon();
			}
			break;
		case Instruments.Echo:
			if(echoTimer > echoCoolDown){
				CreateEcho();
			}
			break;
		}
	}

	void ParachuteJump(){
		rb2d.velocity = new Vector2(0,jumpSpeed);
		sounds[jumpCount].Play();
		jumpCount++;
		jumpTimer = 0;
	}

	void BootsDash(){
		dashTimer = 0;
		isDashing = true;
		inputEnabled = false;
		sounds [4+ Random.Range(0,3)].Play ();
	}
	void StopDashing(){
		isDashing = false;
		inputEnabled = true;
		rb2d.velocity = Vector2.zero;
	}

	void FireHarpoon(){
		if (FindObjectOfType<HarpoonController> ()) {
			Destroy(FindObjectOfType<HarpoonController> ().gameObject);
			Destroy(FindObjectOfType<RopeController> ().gameObject);
			if(FindObjectOfType<HarpoonBaseController>()){
				Destroy(FindObjectOfType<HarpoonBaseController>().gameObject);
			}
		}

		harpoonTimer = 0;
		GameObject oNSceneHarpoon = Instantiate (harpoonProj);
		GameObject oNSceneRope = Instantiate (harpoonRope);
		Rigidbody2D harpRB2D = oNSceneHarpoon.GetComponent<Rigidbody2D> ();
		oNSceneHarpoon.transform.position = transform.position;
		oNSceneRope.transform.position = transform.position;
		if(transform.localScale.x > 0){
			harpRB2D.velocity = new Vector2 (harpoonSpeed,0);
		}
		else{
			harpRB2D.velocity = new Vector2 (-harpoonSpeed,0);
		}
	}

	void CreateEcho(){
		echoTimer = 0;
		GameObject actualEcho = Instantiate (echoPrefab);
		actualEcho.transform.position = transform.position;
		actualEcho.GetComponent<SpriteRenderer> ().sprite = GetComponent<SpriteRenderer> ().sprite;
	}

	void OnCollisionStay2D(Collision2D otherObj){
		if (Mathf.Abs(rb2d.velocity.x) < 1 && Mathf.Abs(rb2d.velocity.y) < 1) {
			if(isDashing){
				StopDashing();
			}
			if(beingPushed){
				StopPushing();
			}
		}

	}

	void StopPushing(){
		beingPushed = false;
		inputEnabled = true;
	}

	//Public Functions
	public void PushPlayer(){
		//beingPushed = true;
		//inputEnabled = false;
		//pushedTimer = 0;
	}
}