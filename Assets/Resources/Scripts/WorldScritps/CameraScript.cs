using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public string animal1;
	public string animal2;
	public string animal3;

	public int animalCount1;
	public int animalCount2;
	public int animalCount3;

	public int winConditionAnimal1;
	public int winConditionAnimal2;
	public int winConditionAnimal3;

	private float smoothTime;
	private GameObject player;
	private Vector2 velocity;

	public Vector3 minCameraPos = new Vector3 (6f, 4.5f, -10f);
	public Vector3 maxCameraPos = new Vector3 (60f, 9.5f, -10f);


	// Use this for initialization
	void Start () {
		animalCount1 = 0;
		animalCount2 = 0;
		animalCount3 = 0;

		winConditionAnimal1 = 3;
		winConditionAnimal2 = 3;
		winConditionAnimal3 = 3;

		smoothTime = .05f;
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	// Update is called once per frame
	void FixedUpdate () {
      
		GameObject echoText = GameObject.Find ("Animal1");
		UnityEngine.UI.Text gui = echoText.GetComponent<UnityEngine.UI.Text> ();

		GameObject echoText2 = GameObject.Find ("Animal2");
		UnityEngine.UI.Text gui2 = echoText2.GetComponent<UnityEngine.UI.Text> ();

		GameObject echoText3 = GameObject.Find ("Animal3");
		UnityEngine.UI.Text gui3 = echoText3.GetComponent<UnityEngine.UI.Text> ();

      
		if (animalCount1 >= winConditionAnimal1 && animalCount2 >= winConditionAnimal2 && animalCount3 >= winConditionAnimal3) {
			SceneManager.LoadScene("WinScreen");
		} else {
			gui.text = animal1 + " Scanned " + animalCount1 + " / " + winConditionAnimal1;
			gui2.text = animal2 + " Scanned " + animalCount2 + " / " + winConditionAnimal2;
			gui3.text = animal3 + " Scanned " + animalCount3 + " / " + winConditionAnimal3;
		}
      
		float posX = Mathf.SmoothDamp (transform.position.x, player.transform.position.x, ref velocity.x, smoothTime);
		float posY = Mathf.SmoothDamp (transform.position.y, player.transform.position.y, ref velocity.y, smoothTime);
      
		transform.position = new Vector3 (posX, posY, transform.position.z);

		transform.position = new Vector3 (Mathf.Clamp (transform.position.x, minCameraPos.x, maxCameraPos.x),
			Mathf.Clamp (transform.position.y, minCameraPos.y, maxCameraPos.y), transform.position.z);
	}

	void changeAnimals(string a, string b, string c) {
		animal1 = a;
		animal2 = b;
		animal3 = c;
	}

	void increaseScanCount(string type) {
		if (type.Equals (animal1)) {
			animalCount1++;
		}
		if (type.Equals (animal2)) {
			animalCount2++;
		}
		if (type.Equals (animal3)) {
			animalCount3++;
		}
	}

}
