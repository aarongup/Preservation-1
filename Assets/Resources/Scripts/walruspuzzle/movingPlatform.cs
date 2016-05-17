using UnityEngine;
using System.Collections;

public class movingPlatform : MonoBehaviour {
	public GameObject player;
	public GameObject slidingPlatform;


	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player");
		slidingPlatform = GameObject.FindWithTag ("SlidePlatform");

	
	}
	
	// Update is called once per frame
	void Update () {
		//slidingPlatform.transform.position = new Vector3 (transform.parent.position.x + .001f, transform.parent.position.y,transform.parent.position.z);

	
	}
	/*
	public void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.name == "Player") {
			Vector3 direction = player.transform.forward;
			direction.Normalize ();
			slidingPlatform.transform.position = new Vector3 (transform.parent.position.x + .05f, transform.parent.position.y,transform.parent.position.z);
			//rb2d.AddForce (Vector3.right * 1.5f);

		}
	}
*/


}
