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
	public GameObject player;
	public bool jumped;
	public bool isBurrowed;
	public Sprite foxSprite;
	public Sprite burrowedSprite;
	public float lineScalar = 1.5f;

	public float speed;

	// Use this for initialization
	void Start () {

		myRB = gameObject.GetComponent<Rigidbody2D> ();
		sight = gameObject.GetComponent<SightScript> ();
		curState = State.Patrol;
		myWidth = this.GetComponent<SpriteRenderer> ().bounds.extents.x;
		player = GameObject.FindWithTag("Player");
		jumped = false;
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

   void OnCollisionEnter2D(Collision2D collisionInfo) {
      if (collisionInfo.collider.gameObject.tag == "Player") {
         Vector3 currRot = gameObject.transform.eulerAngles;
         currRot.y += 180;
         gameObject.transform.eulerAngles = currRot;
      }
   }

   void OnTriggerEnter2D(Collider2D other)
	{			
		if (other.gameObject.tag == "Wall" || other.gameObject.tag == "Animal") {
			Vector3 currRot = gameObject.transform.eulerAngles;
			currRot.y += 180;
			gameObject.transform.eulerAngles = currRot;
		}
	}
}

