using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

	public float speed = 50f;
	public float jumpPower = 150f;
	public float maxSpeed = 3f; 


	public bool grounded;
	public bool canDoubleJump;


	public float currentHealth;
	public float maxHealth = 1f; //1 repersents full, 90% health = .9

	private Rigidbody2D rb2d;
	private Animator anim;
	public GUIBarScript GBS;


	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		anim = gameObject.GetComponent<Animator> ();
		currentHealth = maxHealth;
	
	}
		
	// Update is called once per frame
	void Update () {
		if (currentHealth > maxHealth) {
			currentHealth = maxHealth;
		}

		if (currentHealth <= 0) {
			Die ();
		}

		GBS.Value = currentHealth;

      Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		//To flip sprite based on direction 
		if (mousePosition.x < transform.position.x) {
			transform.localScale = new Vector3 (-.193f, .193f, 1);
		}
		else if (mousePosition.x > transform.position.x) {
			transform.localScale = new Vector3 (.193f, .193f, 1);
		}

		if (Input.GetButtonDown ("Jump") || Input.GetKeyDown("w")) {
			if (grounded) {
				rb2d.AddForce (Vector2.up * jumpPower * 1.5f);
				canDoubleJump = true;
			}
			else {
				if (canDoubleJump) {
					canDoubleJump = false;
					rb2d.velocity = new Vector2 (rb2d.velocity.x, 0);
					rb2d.AddForce (Vector2.up * jumpPower * 1.5f);
				}
			}
		}
	}

	void FixedUpdate() {
		Vector3 easeVelocity = rb2d.velocity; 	//setting up for fake friction
		//easeVelocity.y = rb2d.velocity.y; 	//want to keep y component the same
		easeVelocity.z = 0.0f;
		easeVelocity.x *= 0.75f;				//reducing force by 25%


		float h = Input.GetAxis ("Horizontal");

		//fake friction to slow down speed of player
		if (grounded) {
			rb2d.velocity = easeVelocity;
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

	void Die() {
		//after dieing it resets to initial screen
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}

	void decreaseHealth(float damage) {
		//do error checks
		currentHealth -= damage;
	}
		
	void OnGUI() {
		GUI.Label (new Rect (0, 0, 100, 200), "Health " + (int)Mathf.Ceil (currentHealth * 100));
	}
}	

