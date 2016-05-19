using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransitioner : MonoBehaviour {

	public GameObject beginObj;
	public GameObject controlObj;
	public GameObject replayObj;
	public GameObject menuObj;
	public GameObject controlTextObj;
	public Button beginButton;
	public Button controlButton;
	public Button replayButton;
	public Button menuButton;
	public Text controlText;

	// Use this for initialization
	void Start () {

		beginObj = GameObject.Find("BeginButton");
		if (beginObj != null) {
			beginButton = beginObj.GetComponent<Button> ();
			beginButton.onClick.AddListener (BeginService);
		}

		controlObj = GameObject.Find("Controls");
		if (controlObj != null) {
			controlButton = controlObj.GetComponent<Button> ();
			controlButton.onClick.AddListener (ControlService);
		}
		controlTextObj = GameObject.Find ("ControlText");
		if(controlTextObj != null) {
			controlText = controlTextObj.GetComponent<Text> ();
		}
		replayObj = GameObject.Find("ReplayButton");
		if (replayObj != null) {
			replayButton = replayObj.GetComponent<Button> ();
			replayButton.onClick.AddListener (ReplayService);
		}
		menuObj = GameObject.Find("MenuButton");
		if (menuObj != null) {
			menuButton = menuObj.GetComponent<Button> ();
			menuButton.onClick.AddListener (MenuService);
		}
	}

	private void BeginService()
	{
		LoadScene("Hub");
	}

	private void ControlService() {

		controlText.enabled = !controlText.enabled;
	}

	private void ReplayService() {
		LoadScene ("Level1");
	}

	private void MenuService() {
		LoadScene ("Menu");
	}

	void LoadScene(string level)
	{
		SceneManager.LoadScene(level);
	}
	// Update is called once per frame
	void Update () {

	}
}