using UnityEngine;
using System.Collections;

public class Toggle : MonoBehaviour {

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
			gameObject.AddComponent<BoxCollider2D> ();
			BoxCollider2D myCollider = gameObject.GetComponent<BoxCollider2D> ();
			myCollider.sharedMaterial = Resources.Load ("GroundTiles") as PhysicsMaterial2D;
		} else if (isActive == false) {
			SpriteRenderer myRenderer = gameObject.GetComponent<SpriteRenderer> ();
			myRenderer.material.color = new Color (1f, 1f, 1f, .35f);
		}
    }
	
	// Update is called once per frame
	void Update () {

		timer -= Time.deltaTime;
		if (Input.GetKeyDown("space") && timer <= 0) {
				swapState ();
			}

	}

	private void swapState() {

		timer = 3f;

		if (isActive == true) {
			isActive = false;
			Destroy (gameObject.GetComponent<BoxCollider2D> ());
			SpriteRenderer myRenderer = gameObject.GetComponent<SpriteRenderer> ();
			myRenderer.material.color = new Color (1f, 1f, 1f, .35f);
		} else if (isActive == false) {
			isActive = true;
			gameObject.AddComponent<BoxCollider2D> ();
			BoxCollider2D myCollider = gameObject.GetComponent<BoxCollider2D> ();
			myCollider.sharedMaterial = Resources.Load ("GroundTiles") as PhysicsMaterial2D;
			SpriteRenderer myRenderer = gameObject.GetComponent<SpriteRenderer> ();
			myRenderer.material.color = new Color (1f, 1f, 1f, 1f);
		}
	}
}
