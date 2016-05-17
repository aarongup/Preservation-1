using UnityEngine;
using System.Collections;

public class Hole : MonoBehaviour {
	public bool blocked;
	public Sprite block;

	// Use this for initialization
	void Start () {
		blocked = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.name == "IceBox") {
			blocked = true;
			Destroy (coll.gameObject);
			gameObject.GetComponent<SpriteRenderer> ().sprite = block;
		}
	}

	public bool getBlockedStatus() {
		return blocked;
	}
}
