using UnityEngine;
using System.Collections;

public class PushBehavior : MonoBehaviour {

	public float pushPower = 2.0f;


	public void OnControllerColliderHit(ControllerColliderHit hit) {
		Rigidbody body = (Rigidbody) hit.collider.attachedRigidbody;

		if (body == null || body.isKinematic) {
			return;
		}

		if (hit.moveDirection.y < -.3f) {
			return;
		}

		Vector2 pusDirection = new Vector2 (hit.moveDirection.x, 0);
		body.velocity = pusDirection * pushPower;
	}
}
