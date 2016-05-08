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
			BoxCollider2D myColliderTop = gameObject.AddComponent<BoxCollider2D> ();
			myColliderTop.offset = new Vector2 (-0.1697314f, 1.260993f);
			myColliderTop.size = new Vector2 (3.828859f, 0.5147915f);
			myColliderTop.sharedMaterial = Resources.Load ("Physics Materials/Ground") as PhysicsMaterial2D;
			BoxCollider2D myColliderBottom = gameObject.AddComponent<BoxCollider2D> ();
			myColliderBottom.offset = new Vector2 (-0.1819516f, .9855719f);
			myColliderBottom.size = new Vector2 (2.455615f, .7270756f);
			myColliderBottom.sharedMaterial = Resources.Load ("Physics Materials/Ground") as PhysicsMaterial2D;
		} else if (isActive == false) {
			foreach (SpriteRenderer curChild in gameObject.GetComponentsInChildren<SpriteRenderer> ()) {
				curChild.color = new Color (1f, 1f, 1f, .35f);
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
			BoxCollider2D[] boxArray = gameObject.GetComponents<BoxCollider2D> ();
			foreach (BoxCollider2D curCollider in boxArray)
				Destroy (curCollider);
			foreach (SpriteRenderer curChild in gameObject.GetComponentsInChildren<SpriteRenderer> ()) {
				curChild.color = new Color (1f, 1f, 1f, .35f);
			}
		} else if (isActive == false) {
			isActive = true;
			BoxCollider2D myColliderTop = gameObject.AddComponent<BoxCollider2D> ();
			myColliderTop.offset = new Vector2 (-0.1697314f, 1.260993f);
			myColliderTop.size = new Vector2 (3.828859f, 0.5147915f);
			myColliderTop.sharedMaterial = Resources.Load ("Physics Materials/Ground") as PhysicsMaterial2D;
			BoxCollider2D myColliderBottom = gameObject.AddComponent<BoxCollider2D> ();
			myColliderBottom.offset = new Vector2 (-0.1819516f, .9855719f);
			myColliderBottom.size = new Vector2 (2.455615f, .7270756f);
			myColliderBottom.sharedMaterial = Resources.Load ("Physics Materials/Ground") as PhysicsMaterial2D;
			foreach (SpriteRenderer curChild in gameObject.GetComponentsInChildren<SpriteRenderer> ()) {
				curChild.color = new Color (1f, 1f, 1f, 1f);
			}
		}
	}
}
