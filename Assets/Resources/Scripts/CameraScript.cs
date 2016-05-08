using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	private float smoothTime;
	private GameObject player;
	private Vector2 velocity;

	private Vector3 minCameraPos;
	private Vector3 maxCameraPos;

	// Use this for initialization
	void Start () {
	
		smoothTime = .05f;
		player = GameObject.FindGameObjectWithTag ("Player");
		minCameraPos = new Vector3 (.5f, .16f, -10);
		maxCameraPos = new Vector3 (10.6f, 13f, -10);
	}

	// Update is called once per frame
	void FixedUpdate () {
	
		float posX = Mathf.SmoothDamp (transform.position.x, player.transform.position.x, ref velocity.x, smoothTime);
		float posY = Mathf.SmoothDamp (transform.position.y, player.transform.position.y, ref velocity.y, smoothTime);

		transform.position = new Vector3 (posX, posY, transform.position.z);

		transform.position = new Vector3 (Mathf.Clamp (transform.position.x, minCameraPos.x, maxCameraPos.x),
			Mathf.Clamp (transform.position.y, minCameraPos.y, maxCameraPos.y), transform.position.z);
	}
}
