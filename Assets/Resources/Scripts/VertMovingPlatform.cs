using UnityEngine;
using System.Collections;

public class VertMovingPlatform : MonoBehaviour {

	public float maxDistance = 3;
	public Vector3 startPosition;
	public bool direction = true; //True = right, false = left

	// Use this for initialization
	void Start () {

		startPosition = transform.position;
	}

	// Update is called once per frame
	void Update () {

		if (direction) {
			gameObject.transform.position = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y + .04f, gameObject.transform.position.z);
		} else if (!direction) {
			gameObject.transform.position = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y - .04f, gameObject.transform.position.z);
		}
		if (transform.position.y >= startPosition.y + maxDistance) {
			direction = !direction;
		} else if (transform.position.y <= startPosition.y - maxDistance) {
			direction = !direction;
		}
	}
}

