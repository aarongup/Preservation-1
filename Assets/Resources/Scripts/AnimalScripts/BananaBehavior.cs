﻿using UnityEngine;
using System.Collections;

public class BananaBehavior : MonoBehaviour {

	private float speed = 10f;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		transform.position += (speed * Time.smoothDeltaTime) * transform.up;

		if (!GetComponent<SpriteRenderer>().isVisible) {
			Destroy (gameObject);
		}
	}

	public void SetForwardDirection(Vector3 f) {
		transform.up = f;
	}

	void OnTriggerEnter2D(Collider2D other) {
		// Only care if hitting an Egg (vs. hitting another Enemy!
		if (other.gameObject.name == "Player") {
			Destroy(this.gameObject);
			GameObject p =  GameObject.Find ("Player");
			p.SendMessage ("decreaseHealth", .10f);
		} else if(other.gameObject.layer == LayerMask.NameToLayer("Ground")) {
			Destroy(this.gameObject);
		}
		/*else if (other.gameObject.layer == 8) {
         Destroy(gameObject);
      }*/
	}


}
