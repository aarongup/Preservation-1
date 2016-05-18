using UnityEngine;
using System.Collections;

public class IceBlock : MonoBehaviour {
	private bool once;
	public Rigidbody2D rb2d;

	void Start () {
		once = false;
		rb2d = GetComponent<Rigidbody2D> ();
	}

	void OnCollisionStay2D(Collision2D other) {
		if (other.gameObject.tag == "Player") {
			rb2d.isKinematic = false;
		}
		else if (other.gameObject.tag == "SlidePlatform") {
			if (!once) {
				rb2d.isKinematic = true;
				transform.parent = other.transform;
				once = true;
			}
		}
	}
		
	void OnCollisionExit2D(Collision2D other) {
		if (other.gameObject.tag == "SlidePlatform") {
			once = false;
			rb2d.isKinematic = false;
		}	

		if (other.gameObject.tag == "Player") {
			rb2d.isKinematic = false;
		}
	}
}
