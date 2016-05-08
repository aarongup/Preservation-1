using UnityEngine;
using System.Collections;

public class EggBehavior : MonoBehaviour {

	private float speed = 10f;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		transform.position += (speed * Time.smoothDeltaTime) * transform.up;

		//Does not work for some reason
		//GlobalBehavior globalBehavior = GameObject.Find ("GameManager").GetComponent<GlobalBehavior>();
		/*
		GlobalBehavior.WorldBoundStatus status =
			globalBehavior.ObjectTransformCollideWorldBound(transform);
		print (status);
		if (status != GlobalBehavior.WorldBoundStatus.Inside) {
			Destroy (this.gameObject);
		}*/

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
		}
	}


}
