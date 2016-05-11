using UnityEngine;
using System.Collections;

public class TogglePlatform : MonoBehaviour {

	private bool isActive;
	private float timer;
	// Use this for initialization
	void Start () {

		if (gameObject.tag == "Set1") {
			isActive = true;
		} else if (gameObject.tag == "Set2") {
			isActive = false;
		}

		if (isActive == true) {
			foreach (BoxCollider2D curCollider in gameObject.GetComponents<BoxCollider2D> ()) {
				curCollider.enabled = true;
			}
		} else if (isActive == false) {
			foreach (SpriteRenderer curChild in gameObject.GetComponentsInChildren<SpriteRenderer> ()) {
				curChild.color = new Color (1f, 1f, 1f, .35f);
			}
			foreach (BoxCollider2D curCollider in gameObject.GetComponents<BoxCollider2D> ()) {
				curCollider.enabled = false;
			}
		}
	}

	// Update is called once per frame
	void Update () {

		timer -= Time.deltaTime;
		if (Input.GetKeyDown(KeyCode.LeftShift) && timer <= 0) {
			swapState ();
		}

	}

	private void swapState() {

		timer = 3f;

		if (isActive == true) {
			isActive = false;
			foreach (BoxCollider2D curCollider in gameObject.GetComponents<BoxCollider2D> ()) {
				curCollider.enabled = false;
			}
			foreach (SpriteRenderer curChild in gameObject.GetComponentsInChildren<SpriteRenderer> ()) {
				curChild.color = new Color (1f, 1f, 1f, .35f);
			}
		} else if (isActive == false) {
			isActive = true;
			foreach (BoxCollider2D curCollider in gameObject.GetComponents<BoxCollider2D> ()) {
				curCollider.enabled = true;
			}
			foreach (SpriteRenderer curChild in gameObject.GetComponentsInChildren<SpriteRenderer> ()) {
				curChild.color = new Color (1f, 1f, 1f, 1f);
			}
		}
	}
}