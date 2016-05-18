using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

	public float speed = 50f;
	public float jumpPower = 150f;
	public float maxSpeed = 3f;
	public float livesLeft = 3f;
	public float timer = 0f;
	public float sizeScalar = .193f;
	public Vector3 startPosition;
	public Text lifeCounter;


	public bool grounded;
	public bool canDoubleJump;
	public bool airborne;

	public bool onTree = false;


	public float currentHealth;
	public float maxHealth = 2f; //1 repersents full, 90% health = .9

	private Rigidbody2D rb2d;
	private Animator anim;
	public GUIBarScript GBS;


	//ALL OF THE BELOW ARE FOR CLIMBING ONLY
	public LayerMask treeLayer;
	public Transform treeCheck;
	public float treeCheckRadius = 2f;
	public float originalGravity;
	public float upSpeed = 0;

	public bool canClimb;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		anim = gameObject.GetComponent<Animator>();
		currentHealth = maxHealth;
		canDoubleJump = false; 
		airborne = false;
		originalGravity = rb2d.gravityScale;
		canClimb = false;
	}

	// Update is called once per frame
	void Update () {
		if (currentHealth > maxHealth) {
			currentHealth = maxHealth;
		}
		timer -= Time.deltaTime;

		if (currentHealth <= 0) {
			Die ();
		}

		lifeCounter.text = "Attempts Left: " + livesLeft;

		GBS.Value = currentHealth;

		if (Input.GetKeyDown (KeyCode.LeftShift) || Input.GetKeyDown (KeyCode.RightShift)) {
			rb2d.gravityScale = 1f;
			if (canClimb) {
				canClimb = !canClimb;
			}
		}

		//anim.setBool("Grounded", grounded);
		//anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x);

		handleGoingOffScreen();

		//To flip sprite based on direction 
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, (transform.position.z - Camera.main.transform.position.z)));

		/*Ray myMouse = Camera.main.ScreenPointToRay (Input.mousePosition);
		Debug.DrawRay(myMouse.origin, myMouse.direction * 10, Color.yellow);*/

		//To flip sprite based on direction 
		if (mousePosition.x < transform.position.x) {
			transform.localScale = new Vector3 (-sizeScalar, sizeScalar, 1);
		}
		else if (mousePosition.x > transform.position.x) {
			transform.localScale = new Vector3 (sizeScalar, sizeScalar, 1);
		}

		if (Input.GetButtonDown ("Jump") || Input.GetKeyDown("w")) {
			if (grounded) {
				rb2d.AddForce (Vector2.up * jumpPower * 1.5f);
				airborne = true;
			}
			else {
				if (canDoubleJump && airborne) {
					airborne = false;
					rb2d.velocity = new Vector2 (rb2d.velocity.x, 0);
					rb2d.AddForce (Vector2.up * jumpPower * 1.5f);
				}
			}
		}



		//change animation state based on velocity
		if ( Math.Abs(rb2d.velocity.x) > 0) {
			anim.SetBool("Running", true);
		}
		else {
			anim.SetBool("Running", false);
		}

		//change animation state based on firing
		if (Input.GetButton("Fire1")) {
			anim.SetBool("Firing", true);
		}
		else {
			anim.SetBool("Firing", false);
		}
	}

	void FixedUpdate() {
		Vector3 easeVelocity = rb2d.velocity; 	//setting up for fake friction
		//easeVelocity.y = rb2d.velocity.y; 	//want to keep y component the same
		easeVelocity.z = 0.0f;
		easeVelocity.x *= 0.75f;				//reducing force by 25%


		float h = Input.GetAxis ("Horizontal");
		//change animation state based on velocity
		if (Math.Abs(h) > 0) {
			anim.SetBool("Running", true);
		}
		else {
			anim.SetBool("Running", false);
		}

		//fake friction to slow down speed of player
		if (grounded) {
			rb2d.velocity = easeVelocity;
		}

		if (canClimb) {
			rb2d.AddForce (Vector2.up * 500f * Input.GetAxis ("Vertical"));
			rb2d.AddForce (Vector2.right * 500f * Input.GetAxis ("Horizontal"));
		}

		//moving the player
		rb2d.AddForce (Vector2.right * speed * h);


		if (rb2d.velocity.x > maxSpeed) {
			rb2d.velocity = new Vector2 (maxSpeed, rb2d.velocity.y);
		}

		if (rb2d.velocity.x < -maxSpeed) {
			rb2d.velocity = new Vector2 (-maxSpeed, rb2d.velocity.y);
		}
	}

	void handleGoingOffScreen() {
		Vector2 viewportPosition = Camera.main.WorldToViewportPoint(gameObject.transform.position);
		if (viewportPosition.x < 0 || viewportPosition.y < 0 || viewportPosition.x > 1 || viewportPosition.y > 1) {
			//Debug.Log("Player offscreen");
			Die();
		}
	}

	void Die() {
		transform.position = startPosition;
		currentHealth = maxHealth;
		if (timer <= 0) {
			livesLeft -= 1f;
			timer = 3f;
		}
		if (livesLeft <= 0) {
			SceneManager.LoadScene ("LoseScreen");
		}
		//SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}

	void decreaseHealth(float damage) {
		//do error checks
		currentHealth -= damage;
	}

	void activateDoubleJump() {
		canDoubleJump = true;
	}

	void OnGUI() {
		GUI.contentColor = Color.green;
		GUI.Label (new Rect (0, 0, 100, 200), "Health " + (int)Mathf.Ceil (currentHealth * 100));
	}

	void OnCollisionStay2D(Collision2D other) {
		if (other.gameObject.tag == "Platform" || other.gameObject.tag == "Set1" || other.gameObject.tag == "Set2") {
			transform.parent = other.transform;
		}
	}

	void OnCollisionExit2D(Collision2D other) {
		if (other.gameObject.tag == "Platform" || other.gameObject.tag == "Set1" || other.gameObject.tag == "Set2") {
			transform.parent = null;
		}			
	}

	void OnTriggerStay2D(Collider2D other) {
		if (other.gameObject.layer == LayerMask.NameToLayer ("Climbable")) {
			canClimb = true;
			rb2d.velocity = new Vector2 (0f, 0f);
			rb2d.gravityScale = .5f;
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.layer == LayerMask.NameToLayer ("Climbable")) {
			canClimb = false;
			rb2d.gravityScale = 1f;
		}
	}

}	

