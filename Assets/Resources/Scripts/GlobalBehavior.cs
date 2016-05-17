/*Aaron Gupta
 * MP3
 * 
 * This is the Global behavior. 
 * Controls various aspects of the world
 * 
 * */

using UnityEngine;
using System.Collections;

public class GlobalBehavior : MonoBehaviour {


	private Bounds mWorldBound;  // this is the world bound
	private Vector2 mWorldMin;	// Better support 2D interactions
	private Vector2 mWorldMax;
	private Vector2 mWorldCenter;
	private Camera mMainCamera;
	public int enemyCount;
	public int eggCount;

	private const int totalEnemies = 50;
	private const float spawnTime = 3.0f;

	public bool enemyMotion = false; 

	// to support time ...
	private float mPreEnemySpawnTime = -1f; // 
	private const float kEnemySpawnInterval = 1.0f; // in seconds


	public bool moveEnemy = false;
	public GameObject enemyToSpawn = null;

	// Use this for initialization
	void Start () {
		eggCount = 0;
		mMainCamera = Camera.main;
		mWorldBound = new Bounds(Vector3.zero, Vector3.one);
		UpdateWorldWindowBound();

		for (int i = 0; i < totalEnemies; i++) {
			enemyToSpawn = Resources.Load ("Prefabs/Enemy") as GameObject;
		}

		enemyCount = 50;

		if (null == enemyToSpawn) {
			enemyToSpawn = Resources.Load ("Prefabs/Enemy") as GameObject;
		}
	}

	//Update is called once per frame
	void Update () {

		// To echo text to a defined GUIText
		GameObject echoText = GameObject.Find("EchoText");
		UnityEngine.UI.Text gui = echoText.GetComponent<UnityEngine.UI.Text> ();
		gui.text = "Current egg count " + eggCount ;

		GameObject enemyText = GameObject.Find("EnemyText");
		UnityEngine.UI.Text gui2 = enemyText.GetComponent<UnityEngine.UI.Text> ();
		gui2.text = "Current enemy count " + enemyCount ;

		// mMainCamera.transform.position += 0.1f * Vector3.one;
		// mMainCamera.orthographicSize += 1.0f;
		spawnAnEnemy();
	} 

	#region Game Window World size bound support
	public enum WorldBoundStatus {
		CollideTop,
		CollideLeft,
		CollideRight,
		CollideBottom,
		Outside,
		Inside
	};

	/// <summary>
	/// This function must be called anytime the MainCamera is moved, or changed in size
	/// </summary>
	public void UpdateWorldWindowBound()
	{
		// get the main 
		if (null != mMainCamera) {
			//orthographicSzie is the height 


			float maxY = mMainCamera.orthographicSize;
			float maxX = mMainCamera.orthographicSize * mMainCamera.aspect;
			float sizeX = 2 * maxX;
			float sizeY = 2 * maxY;
			float sizeZ = Mathf.Abs(mMainCamera.farClipPlane - mMainCamera.nearClipPlane);

			// Make sure z-component is always zero
			Vector3 c = mMainCamera.transform.position;
			c.z = 0.0f;
			mWorldBound.center = c;
			mWorldBound.size = new Vector3(sizeX, sizeY, sizeZ);

			mWorldCenter = new Vector2(c.x, c.y);
			mWorldMin = new Vector2(mWorldBound.min.x, mWorldBound.min.y);
			mWorldMax = new Vector2(mWorldBound.max.x, mWorldBound.max.y);
		}
	}

	public Vector2 WorldCenter { get { return mWorldCenter; } }
	public Vector2 WorldMin { get { return mWorldMin; }} 
	public Vector2 WorldMax { get { return mWorldMax; }}

	public Bounds getWorldBound() {
		return mWorldBound;
	}

	public WorldBoundStatus ObjectCollideWorldBound(Bounds objBound)
	{
		WorldBoundStatus status = WorldBoundStatus.Inside;

		if (mWorldBound.Intersects (objBound)) {
			if (objBound.max.x > mWorldBound.max.x)
				status = WorldBoundStatus.CollideRight;
			else if (objBound.min.x < mWorldBound.min.x)
				status = WorldBoundStatus.CollideLeft;
			else if (objBound.max.y > mWorldBound.max.y)
				status = WorldBoundStatus.CollideTop;
			else if (objBound.min.y < mWorldBound.min.y)
				status = WorldBoundStatus.CollideBottom;
			else if ((objBound.min.z < mWorldBound.min.z) || (objBound.max.z > mWorldBound.max.z))
				status = WorldBoundStatus.Outside;
		} else 
			status = WorldBoundStatus.Outside;

		return status;
	}
	#endregion 

	private void spawnAnEnemy() {
		if ((Time.realtimeSinceStartup - mPreEnemySpawnTime) > kEnemySpawnInterval) {
			increaseEnemyCount ();
			mPreEnemySpawnTime = Time.realtimeSinceStartup;
		}
	}

	public WorldBoundStatus ObjectTransformCollideWorldBound(Transform t)
	{
		WorldBoundStatus status = WorldBoundStatus.Inside;
		Vector3 transform = t.position;

		if (transform.x > mWorldMax.x) {
			status = WorldBoundStatus.CollideRight;
			transform.x = mWorldMax.x;
		} else if (transform.x < mWorldMin.x) {
			status = WorldBoundStatus.CollideLeft;
			transform.x = mWorldMin.x;
		}

		if ((transform.y < mWorldMin.y)) {
			transform.y = mWorldMin.y;
			status = WorldBoundStatus.Outside;
		}
	
		if(transform.y > mWorldMax.y) {
			transform.y = mWorldMax.y;
			status = WorldBoundStatus.Outside;
		}

		t.position = transform;
		return status;

	}

	public void toggleMovement(bool b) {
		enemyMotion = b;
	}

	public void increaseEggCount() {
		eggCount++;
	}

	public void decreaseEggCount() {
		eggCount--;
	}
		
	public void increaseEnemyCount() {
		enemyCount++;
	}

	public void decreaseEnemyCount() {
		enemyCount--;
	}
}