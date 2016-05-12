﻿using UnityEngine;
using System.Collections;

public class Bear : MonoBehaviour {
	public enum State {
		Patrol,
		Charge
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
	public float lineScalar = 1.5f;

	public float speed;

	// Use this for initialization
	void Start () {

		speed = 3f;
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

		switch (curState) {
		case State.Patrol:
			updateFromPatrol ();
			break;
		case State.Charge:
			updateFromCharge ();
			break;
		}
	}

	private void updateFromPatrol() {

		Vector2 lineCastPos = gameObject.transform.position - gameObject.transform.right * myWidth;
		Debug.DrawLine (lineCastPos, lineCastPos + Vector2.down * lineScalar);
		bool isGrounded = Physics2D.Linecast (lineCastPos, lineCastPos + Vector2.down * lineScalar, foxMask);

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
		Vector2 lineCastPos = gameObject.transform.position - gameObject.transform.right * myWidth;
		Debug.DrawLine (lineCastPos, lineCastPos + (Vector2.down * lineScalar));
		bool isGrounded = Physics2D.Linecast (lineCastPos, lineCastPos + (Vector2.down * lineScalar), foxMask);

		if (!isGrounded) {
			Vector3 currRot = gameObject.transform.eulerAngles;
			currRot.y += 180;
			gameObject.transform.eulerAngles = currRot;
		}



		if (Vector3.Distance (player.transform.position, transform.position) < 3) {
			Vector2 myVel = myRB.velocity;
			myVel.x = (float) -gameObject.transform.right.x * (float)(speed * 2.5);
			myRB.velocity = myVel;
		} else {
			Vector2 myVel = myRB.velocity;
			myVel.x = (float)-gameObject.transform.right.x * (float)(speed * 2.5);
			myRB.velocity = myVel;
		}

		if (!sight.playerSpotted) {
			curState = State.Patrol;
		}
	}
			

	void OnTriggerEnter2D(Collider2D other)
	{		
		if (other.gameObject.name == "Player") {
			GameObject p =  GameObject.Find ("Player");
			p.SendMessage ("decreaseHealth", .2f);
		}
				

		if (other.gameObject.tag == "Wall") {
			Vector3 currRot = gameObject.transform.eulerAngles;
			currRot.y += 180;
			gameObject.transform.eulerAngles = currRot;
		}
	}
}
