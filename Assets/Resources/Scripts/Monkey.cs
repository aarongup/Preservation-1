//http://ideas.wikia.com/wiki/File:Sweet_the_monkey.png

using UnityEngine;
using System.Collections;

public class Monkey : MonoBehaviour {

	public GameObject projectile = null;
	private float prevProj;
	public Player player;

	// Use this for initialization
	void Start () {
		prevProj = Time.realtimeSinceStartup;
		if (null == projectile) {
			projectile = Resources.Load ("Prefabs/projectile") as GameObject;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if ((player.transform.position - transform.position).magnitude < 3) {
			float currentTime = Time.realtimeSinceStartup;
			if (currentTime - prevProj > .1f) {
				prevProj = currentTime;
				GameObject e = Instantiate (projectile) as GameObject;
				EggBehavior egg = e.GetComponent<EggBehavior> ();
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
}
