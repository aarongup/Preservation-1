using UnityEngine;
using System.Collections;

public class HorizMovingPlatform : MonoBehaviour {

	public float maxDistance = 3;
	public Vector3 startPosition;
	private bool direction; //True = right, false = left

	// Use this for initialization
	void Start () {
	
		startPosition = transform.position;
		direction = true;
	}
	
	// Update is called once per frame
	void Update () {

		if (direction) {
			gameObject.transform.position = new Vector3 (gameObject.transform.position.x + .04f, gameObject.transform.position.y, gameObject.transform.position.z);
		} else if (!direction) {
			gameObject.transform.position = new Vector3 (gameObject.transform.position.x - .04f, gameObject.transform.position.y, gameObject.transform.position.z);
		}
		if (transform.position.x >= startPosition.x + maxDistance) {
			direction = !direction;
		} else if (transform.position.x <= startPosition.x - maxDistance) {
			direction = !direction;
		}
	}
}
