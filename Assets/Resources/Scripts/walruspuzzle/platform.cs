using UnityEngine;
using System.Collections;

public class platform : MonoBehaviour {
	private float initX;
	private float initY;
	// Use this for initialization
	void Start () {
		initX = transform.position.x;
		initY = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x != initY) {
			transform.position = new Vector2 (transform.position.x, initY);
		}
	}
}
