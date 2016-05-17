using UnityEngine;
using System.Collections;

public class Owl : MonoBehaviour {
	public enum State {
		Patrol, Charge
	}

	public GameObject projectile = null;
	public float projectileInterval = 1f;

	public LayerMask foxMask;
	public SightScript sight;
	private State curState;
	private float myWidth;
	private Vector3 returnPoint;
	public GameObject player;
	public float lineScalar;

	public float speed;
	public float distance;
	public bool flip;

	private float prevProj;


	// Use this for initialization
	void Start () {
		prevProj = Time.realtimeSinceStartup;
		flip = false;
		speed = 2f;
		sight = gameObject.GetComponent<SightScript> ();
		curState = State.Patrol;
		myWidth = this.GetComponent<SpriteRenderer> ().bounds.extents.x;
		player = GameObject.FindWithTag("Player");
		if (null == projectile) {
			projectile = Resources.Load ("Prefabs/Egg") as GameObject;
		}
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (curState);
		distance--;
		if (!flip) {
			gameObject.transform.Translate (Vector3.right * speed * Time.deltaTime);
			gameObject.transform.localScale = new Vector3 (1f, 1f, 1f);
		} else {
			gameObject.transform.Translate (Vector3.left * speed * Time.deltaTime);
			gameObject.transform.localScale = new Vector3 (-1f, 1f, 1f);
		}
		if (distance <= 0) {
			distance = 50;
			flip = !flip;
		}

		switch (curState) {
		case State.Charge:
			updateFromCharge ();
			break;
		case State.Patrol:
			updateFromPatrol();
			break;
		}
	}

	void updateFromCharge() {
		float currentTime = Time.realtimeSinceStartup;
		if (currentTime - prevProj > projectileInterval) {
			prevProj = currentTime;
			Vector2 lineCastPos = (gameObject.transform.position - player.transform.position).normalized;
			GameObject e = Instantiate (projectile) as GameObject;
			RockBehavior egg = e.GetComponent<RockBehavior> ();
			if (null != egg) {
				e.transform.position = transform.position;
				egg.SetForwardDirection (lineCastPos);
			}
		}
		curState = State.Patrol;
	}

	void updateFromPatrol() {
		//Debug.Log ("in patrol");
		Vector2 lineCastPos = gameObject.transform.position - gameObject.transform.right * myWidth;
		Debug.DrawLine (lineCastPos, lineCastPos + Vector2.down * lineScalar * 3);
		bool playerSpotted = Physics2D.Linecast (lineCastPos, lineCastPos + Vector2.down * lineScalar * 3, foxMask);
		//Debug.Log (playerSpotted);
		if (playerSpotted) {
			curState = State.Charge;
		}
	}


	void OnTriggerEnter2D(Collider2D other)
	{		
		if (other.gameObject.name == "Player") {
			GameObject p =  GameObject.Find ("Player");
			p.SendMessage ("decreaseHealth", .15f);
		}
	}

}
