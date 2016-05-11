using UnityEngine;
using System.Collections;

public class SightScript : MonoBehaviour {

	public GameObject player;
	public float angleOfView = 90f;
	public bool playerSpotted = false;
	public bool facingRight = true;
	public bool targetBlocked;
	public bool targetVisable;
	// Use this for initialization
	void Start () {

		player = GameObject.FindWithTag ("Player");
	}

	// Update is called once per frame
	void Update () {

		Vector3 targetDir = player.transform.position - transform.position;
		Vector3 right = transform.right;
		Vector3 left = right * -1;
		float angleRight = Vector3.Angle (targetDir, right);
		float angleLeft = Vector3.Angle (targetDir, left);

		targetBlocked = Physics2D.Linecast (transform.position, player.transform.position, 1 << LayerMask.NameToLayer ("Ground"));
		Debug.DrawLine (transform.position, player.transform.position);
		targetVisable = Physics2D.Linecast (transform.position, player.transform.position, 1 << LayerMask.NameToLayer ("Player"));
		if (angleLeft < angleOfView && Vector3.Distance (player.transform.position, transform.position) < 10 && targetVisable && !targetBlocked) {

			playerSpotted = true;
		} else {
			playerSpotted = false;
		}
	}
}
