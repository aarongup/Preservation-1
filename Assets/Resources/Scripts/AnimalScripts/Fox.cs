using UnityEngine;
using System.Collections;

public class Fox : MonoBehaviour {
	public enum State {
		Patrol,
		Charge,
		Burrowed
	}

	public LayerMask foxMask;
	public SightScript sight;
	private State curState;
	private Rigidbody2D myRB;
	private float myWidth;
	private Vector3 returnPoint;
	public GameObject player;
	public bool jumped;
	public bool isBurrowed;
	public Sprite foxSprite;
	public Sprite burrowedSprite;
	private SpriteRenderer myRenderer;
	private Animator anim;
	public bool isGrounded;

	public float lineScalar = 1.5f;
	private float timer = 0f;

	public float speed;

	// Use this for initialization
	void Start () {
	
		speed = 4f;
		anim = gameObject.GetComponent<Animator>();
		myRB = gameObject.GetComponent<Rigidbody2D> ();
		sight = gameObject.GetComponent<SightScript> ();
		curState = State.Patrol;
		myWidth = this.GetComponent<SpriteRenderer> ().bounds.extents.x;
		player = GameObject.FindWithTag("Player");
		jumped = false;
		myRenderer = gameObject.GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
	
		Vector2 lineCastPos = gameObject.transform.position - gameObject.transform.right * myWidth;
		Debug.DrawLine (lineCastPos, lineCastPos + Vector2.down * lineScalar);
		isGrounded = Physics2D.Linecast (lineCastPos, lineCastPos + Vector2.down * lineScalar, foxMask);

		switch (curState) {
		case State.Patrol:
			updateFromPatrol ();
			break;
		case State.Charge:
			updateFromCharge ();
			break;
		case State.Burrowed:
			updateFromBurrowed ();
			break;
		}
	}

	private void updateFromPatrol() {




		if (!isGrounded) {
			Vector3 currRot = gameObject.transform.eulerAngles;
			currRot.y += 180;
			gameObject.transform.eulerAngles = currRot;
		}

		Vector2 myVel = myRB.velocity;
		myVel.x = -gameObject.transform.right.x * speed;
		myRB.velocity = myVel;

		if (sight.playerSpotted) {
			curState = State.Charge;
			returnPoint = gameObject.transform.position;
		}
	}

	private void updateFromCharge() {

		if (!isGrounded) {
			Vector3 currRot = gameObject.transform.eulerAngles;
			currRot.y += 180;
			gameObject.transform.eulerAngles = currRot;
		}



		if (Vector3.Distance (player.transform.position, transform.position) < 3 && !jumped) {
			Vector2 myVel = myRB.velocity;
			myVel.x = -gameObject.transform.right.x * (speed * 2);
			myVel.y = 6;
			myRB.velocity = myVel;
			jumped = true;
		} else {
			Vector2 myVel = myRB.velocity;
			myVel.x = -gameObject.transform.right.x * (speed * 2);
			myRB.velocity = myVel;
		}

		if (!sight.playerSpotted) {
			curState = State.Patrol;
		}
	}

	private void updateFromBurrowed () {
		timer -= Time.deltaTime;

		if (!isGrounded) {
			Vector3 currRot = gameObject.transform.eulerAngles;
			currRot.y += 180;
			gameObject.transform.eulerAngles = currRot;
		}

		Vector2 myVel = myRB.velocity;
		myVel.x = -gameObject.transform.right.x * speed;
		myRB.velocity = myVel;

		if (timer <= 0) {
			curState = State.Patrol;
			anim.enabled = true;
			isBurrowed = false;
			myRenderer.sprite = foxSprite;
			gameObject.transform.localScale = new Vector3 (1.25f, 1.25f, 1.25f);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{		

		if (other.gameObject.layer == LayerMask.NameToLayer("Player") && !isGrounded) {
			Vector3 currRot = gameObject.transform.eulerAngles;
			currRot.y += 180;
			gameObject.transform.eulerAngles = currRot;
		}

		if (other.gameObject.layer == LayerMask.NameToLayer("Player") && !isBurrowed) {
			Vector3 currRot = gameObject.transform.eulerAngles;
			currRot.y += 180;
			gameObject.transform.eulerAngles = currRot;
		}

		if (other.gameObject.layer == LayerMask.NameToLayer("Ground") && jumped == true) {
			anim.enabled = false;
			jumped = false;
			curState = State.Burrowed;
			isBurrowed = true;
			myRenderer.sprite = burrowedSprite;
			timer = 10f;
			gameObject.transform.localScale = new Vector3 (1f, .5f, 1f);
		}

		if (other.gameObject.tag == "Wall" || other.gameObject.tag == "Animal") {
			Vector3 currRot = gameObject.transform.eulerAngles;
			currRot.y += 180;
			gameObject.transform.eulerAngles = currRot;
		}
	}
}
