using UnityEngine;
using System.Collections;

public class OneWayPlatform : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnTriggerEnter(Collider other) {

		Debug.Log ("CollisionEnter");
		Physics.IgnoreCollision (other, gameObject.GetComponent<Collider> ());

	}

	public void OnTriggerExit(Collider other) {

		Debug.Log ("CollisionExit");
		Physics.IgnoreCollision (other, gameObject.GetComponent<Collider> ());

	}
}
