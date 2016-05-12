using UnityEngine;
using System.Collections;

public class AlternateBG : MonoBehaviour {

	public SpriteRenderer myRenderer;
	public float alphaValue;
	public bool alphaDirection; //True for increase, false for decrease
	// Use this for initialization
	void Start () {
	
		myRenderer = gameObject.GetComponent<SpriteRenderer> ();
		if (gameObject.tag == "BG1") {
			alphaValue = 1f;
			alphaDirection = false;
		} else if (gameObject.tag == "BG2") {
			alphaValue = 0f;
			alphaDirection = true;
		}
		myRenderer.color = new Color (1f, 1f, 1f, alphaValue);
	}
	
	// Update is called once per frame
	void Update () {
	
		if (!alphaDirection) {
			alphaValue -= .005f;
		} else if (alphaDirection) {
			alphaValue += .005f;
		}

		if (alphaValue >= 1) {
			alphaDirection = false;
		}

		if (alphaValue <= 0) {
			alphaDirection = true;
		}

		myRenderer.color = new Color (1f, 1f, 1f, alphaValue);
	}
}
