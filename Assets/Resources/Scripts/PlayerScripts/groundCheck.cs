using UnityEngine;
using System.Collections;

public class groundCheck : MonoBehaviour {

	private Player player;
	// Use this for initialization
	void Start () {
		player = gameObject.GetComponentInParent<Player> ();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay2D(Collider2D col) {
		player.grounded = true;
	}

	void OnTriggerEnter2D(Collider2D col) {
		player.grounded = true;
	}

	void OnTriggerExit2D(Collider2D col) {
		player.grounded = false;
	}
}
