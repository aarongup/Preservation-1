using UnityEngine;
using System.Collections;

public class Walrus : MonoBehaviour {
	public GameObject player;

	public Hole leftHole;
	public Hole rightHole;
	public Hole middleHole;

	private Vector3 leftPos;
	private Vector3 middlePos;
	private Vector3 rightPos;

	private int currentPosition;
	private float speed = 2.0f;
	private float timeLimit = 1.0f;

	private float lowerY = -50.7f;	//position where snowman is no longer seen
	private float upperY = -51.82f;
	private float deltaY = .02f;

	private float threeSeconds = 86f;


	private bool down;
	private float spawnTime;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player");
		GameObject l = GameObject.Find ("lefthole");
		GameObject m = GameObject.Find ("middlehole");
		GameObject r = GameObject.Find ("righthole");

		leftHole = l.GetComponent<Hole> ();
		middleHole = m.GetComponent<Hole> ();
		rightHole = r.GetComponent<Hole> ();

		leftPos = l.transform.position;
		middlePos = m.transform.position;
		rightPos = r.transform.position;

		currentPosition = 0;
		down = false;
		spawnTime = Time.realtimeSinceStartup;


	}

	void stuff() {
		float leftDist = (player.transform.position - leftPos).magnitude;
		float middleDist = (player.transform.position - middlePos).magnitude;
		float rightDist = (player.transform.position - rightPos).magnitude;

		if (!leftHole.blocked && !middleHole.blocked && !rightHole.blocked) {
			getNextHole (leftDist, middleDist, rightDist);
			changePosition ();
		} else if (leftHole.blocked) {
			getNextHole (-999f, middleDist, rightDist);
			changePosition ();
		} else if (middleHole.blocked) {
			getNextHole (leftDist, -999f, rightDist);
			changePosition ();
		} else if (rightHole.blocked) {
			getNextHole (leftDist, middleDist, -999f);
			changePosition ();
		}

	}

	void getNextHole(float left, float middle, float right) {
		switch (currentPosition) {
		case 0:
			if (left > middle && left > right) {
				break;
			}
			if (middle > right) {
				currentPosition = 1;
			} else  if(right > middle) {
				currentPosition = 2;
			}
			break;
		case 1:
			if (middle > left && middle > right) {
				break;
			}
			if (left > right) {
				currentPosition = 0;
			} else if(right > left) {
				currentPosition = 2;
			}
			break;
		case 2:
			if (right > left && right > middle) {
				break;
			}
			if (left > middle) {
				currentPosition = 0;
			} else if(middle > left) {
				currentPosition = 1;
			}
			break;
		}
	}

	// Update is called once per frame
	void Update () {
		/*float currentY = transform.position.y;
		if (currentY >= upperY) {
			down = true;
		} 
		else if (currentY <= lowerY) {
			down = false;
		}
		if (!down) {
			transform.position = new Vector2 (transform.position.x, currentY + deltaY);
		} else {
			transform.position = new Vector2 (transform.position.x, currentY - deltaY);
		}*/

		float currentTime = Time.realtimeSinceStartup;
		//print (currentTime - spawnTime);
		if (currentTime - spawnTime >= timeLimit) {
			stuff ();
			spawnTime = currentTime;
		}
	}

	void changePosition() {
		switch (currentPosition) {
		case 0:
			transform.position = leftPos;
			break;
		case 1:
			transform.position = middlePos;
			break;
		case 2:
			transform.position = rightPos;
			break;
		}
	}

	void goDown() {
		print ("inside down");
		print (down);
		float currentY = transform.position.y;
		while (currentY > upperY) {
			transform.position = new Vector2 (transform.position.x, currentY - deltaY);
			currentY = transform.position.y;
			print ("current y is " + currentY);
			down = false;
		}

	}

	public void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.name == "Player") {
			player.SendMessage ("decreaseHealth", .15f);
		}
	}

}
