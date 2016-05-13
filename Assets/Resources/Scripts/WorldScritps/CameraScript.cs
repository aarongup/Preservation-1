using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public int animalCount;

	private float smoothTime;
	private GameObject player;
	private Vector2 velocity;

	public Vector3 minCameraPos = new Vector3 (6f, 4.5f, -10f);
	public Vector3 maxCameraPos = new Vector3 (60f, 9.5f, -10f);

	// Use this for initialization
	void Start () {
		animalCount = 0;
		smoothTime = .05f;
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	// Update is called once per frame
	void FixedUpdate () {
      
		GameObject echoText = GameObject.Find ("numberScanned");
		UnityEngine.UI.Text gui = echoText.GetComponent<UnityEngine.UI.Text> ();
      
		if (animalCount >= 5) {
			SceneManager.LoadScene("WinScreen");
		} else {
			gui.text = "Animals Scanned " + animalCount;
		}
      
		float posX = Mathf.SmoothDamp (transform.position.x, player.transform.position.x, ref velocity.x, smoothTime);
		float posY = Mathf.SmoothDamp (transform.position.y, player.transform.position.y, ref velocity.y, smoothTime);
      
		transform.position = new Vector3 (posX, posY, transform.position.z);

		transform.position = new Vector3 (Mathf.Clamp (transform.position.x, minCameraPos.x, maxCameraPos.x),
			Mathf.Clamp (transform.position.y, minCameraPos.y, maxCameraPos.y), transform.position.z);
	}
}
