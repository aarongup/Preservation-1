﻿using UnityEngine;
using System.Collections;

public class ToggleMaze : MonoBehaviour {

	private bool isActive;
	private float timer;
	// Use this for initialization
	void Start () {

		if (gameObject.tag == "MazeSet1") {
			isActive = true;
		} else if (gameObject.tag == "MazeSet2") {
			isActive = false;
		}

		if (isActive == true) {
			foreach (BoxCollider2D curCollider in gameObject.GetComponents<BoxCollider2D> ()) {
				curCollider.enabled = true;
			}
		} else if (isActive == false) {
			SpriteRenderer myRenderer = gameObject.GetComponent<SpriteRenderer> ();
			myRenderer.color = new Color (1f, 1f, 1f, 0f);
			foreach (BoxCollider2D curCollider in gameObject.GetComponents<BoxCollider2D> ()) {
				curCollider.enabled = false;
			}
		}
	}

	// Update is called once per frame
	void Update () {

		timer -= Time.deltaTime;
		if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) {
			swapState ();
		}

	}

	private void swapState() {

		timer = .5f;

		if (isActive == true) {
			isActive = false;
			foreach (BoxCollider2D curCollider in gameObject.GetComponents<BoxCollider2D> ()) {
				curCollider.enabled = false;
			}
			SpriteRenderer myRenderer = gameObject.GetComponent<SpriteRenderer> ();
			myRenderer.color = new Color (1f, 1f, 1f, 0f);
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