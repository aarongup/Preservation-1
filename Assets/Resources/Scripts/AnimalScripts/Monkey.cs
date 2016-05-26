//http://ideas.wikia.com/wiki/File:Sweet_the_monkey.png

using UnityEngine;
using System.Collections;

public class Monkey : MonoBehaviour {

	public GameObject projectile = null;
	public GameObject player;
	private SightScript mySight;

	public const float targetRadius = 3f;
	public const float projectileInterval = .24f;
	private float prevProj;

	// Use this for initialization
	void Start () {
		prevProj = Time.realtimeSinceStartup;
		mySight = gameObject.GetComponent<SightScript> ();
		player = GameObject.FindWithTag("Player");
		if (null == projectile) {
			projectile = Resources.Load ("Prefabs/projectile") as GameObject;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (mySight.playerSpotted) {
			float currentTime = Time.realtimeSinceStartup;
			if (currentTime - prevProj > projectileInterval) {
				prevProj = currentTime;
				GameObject e = Instantiate (projectile) as GameObject;
				BananaBehavior egg = e.GetComponent<BananaBehavior> ();
				if (null != egg) {
					e.transform.position = transform.position;
					egg.SetForwardDirection (NewDirection());
				}
			}
		}
	}

	private Vector2 NewDirection() {
		float mTowardsCenter = 0.5f; 
		//transform.localScale = new Vector3 (5f, 5f);
		//GlobalBehavior globalBehavior = GameObject.Find ("GameManager").GetComponent<GlobalBehavior>();

		// we want to move towards the center of the world
		Vector2 v = new Vector2(player.transform.position.x, player.transform.position.y) - new Vector2(transform.position.x, transform.position.y);  
		// this is vector that will take us back to world center
		v.Normalize();
		Vector2 vn = new Vector2(v.y, -v.x); // this is a direciotn that is perpendicular to V

		float useV = 1.0f - Mathf.Clamp(mTowardsCenter, 0.01f, 1.0f);
		float tanSpread = Mathf.Tan( useV * Mathf.PI / 2.0f );

		float randomX = Random.Range(0f, 1f);
		float yRange = tanSpread * randomX;
		float randomY = Random.Range (-yRange, yRange);

		Vector2 newDir = randomX * v + randomY * vn;
		newDir.Normalize();
		return newDir;
	}

	public void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.name == "Player") {
			GameObject p =  GameObject.Find ("Player");
			p.SendMessage ("decreaseHealth", .05f);
		}
	}
}
