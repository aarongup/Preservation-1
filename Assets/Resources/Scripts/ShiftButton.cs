using UnityEngine;
using System.Collections;

public class ShiftButton : MonoBehaviour {
	public Sprite button;

	private SpriteRenderer rend;
	private IEnumerator coroutine;

	private bool playerCollide;
	private bool appear;
	// Use this for initialization
	void Start () {
		rend = GetComponent<SpriteRenderer> ();
		playerCollide = false;
		appear = false;
		coroutine = null;
	}
	
	// Update is called once per frame
	void Update () {
		if (playerCollide) {
			if (!appear) {
				rend.sprite = button;
				appear = true;
				coroutine = Blink (rend, 10, 1f, true);
				StartCoroutine (coroutine);
			}

		} else {
			rend.sprite = null;
			if (coroutine != null) {
				StopCoroutine (coroutine);
				coroutine = null;
			} 
			appear = false;
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			playerCollide = true;
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "Player") {
			playerCollide = false;
		}
	}

	IEnumerator Blink(SpriteRenderer sprite, int numTimes, float delay, bool disabled) {
		for (int i = 0; i < numTimes; i++) {
			if (disabled) {
				sprite.enabled = false;
			} else {
				sprite.color = new Color (sprite.color.r, sprite.color.g, sprite.color.b, .5f);
			}

			yield return new WaitForSeconds (delay);

			if (disabled) {
				sprite.enabled = true;
			} else {
				sprite.color = new Color (sprite.color.r, sprite.color.g, sprite.color.b, 1);
			}

			yield return new WaitForSeconds (delay);
		}
	
	}
}
