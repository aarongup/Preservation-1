using UnityEngine;
using System.Collections;

public class PowerUpDJ : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.name == "Player") {
			Destroy(this.gameObject);
			GameObject p =  GameObject.Find ("Player");
			p.SendMessage ("activateDoubleJump");
		}
	}
}
