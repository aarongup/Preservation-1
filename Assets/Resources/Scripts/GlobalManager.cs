using UnityEngine;
using System.Collections;

public class GlobalManager : MonoBehaviour {

	private string curLevel = "MenuLevel";

	// Use this for initialization
	void Start () {
	
		DontDestroyOnLoad (this);
	}
	
	// Update is called once per frame
	public void SetCurrentLevel(string level) {
		curLevel = level;
	}
}
