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
	private float timeLimit = 5.0f;

	private float lowerY = -6f;
	private float upperY = -4.4f;
	private float deltaY = .02f;

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
	
	// Update is called once per frame
	void Update () {
		float currentY = transform.position.y;
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
		}

		float currentTime = Time.realtimeSinceStartup;
		//print (currentTime - spawnTime);
		if (currentTime - spawnTime >= timeLimit) {
			currentPosition++;
			currentPosition = currentPosition % 3; 
			changePosition ();
			spawnTime = currentTime;
		}
	}

	void changePosition() {
		goDown ();
		print ("curr " + currentPosition);
		if (currentPosition == 0) {
			if (leftHole.blocked) {
				currentPosition++;
			}
		} else if (currentPosition == 1) {
			if (middleHole.blocked) {
				currentPosition++;
			} 
		} else {
			print (currentPosition);
			if (rightHole.blocked) {
				currentPosition = 0;
			} 
		}
		move ();
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

	void move() {
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

	public void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.name == "Player") {
			player.SendMessage ("decreaseHealth", .15f);
		}
	}




}
