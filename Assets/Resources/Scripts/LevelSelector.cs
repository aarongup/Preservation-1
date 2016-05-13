using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelSelector : MonoBehaviour {

	public string levelName = "Exit";
	public GameObject player;
	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {

		if (string.Compare(levelName, "Exit") == 0 && Vector3.Distance (player.transform.position, gameObject.transform.position) < 2 && Input.GetKeyDown (KeyCode.W)) {
			Application.Quit ();
		}

		if (string.Compare(levelName, "Exit") != 0  && Vector3.Distance (player.transform.position, gameObject.transform.position) < 2 && Input.GetKeyDown (KeyCode.W)) {
			SceneManager.LoadScene(levelName);
		}
	}
}
