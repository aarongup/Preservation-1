using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransitioner : MonoBehaviour {

	public GameObject beginObj;
	public GameObject controlObj;
	public GameObject replayObj;
	public GameObject menuObj;
	public GameObject hubObj;
	public GameObject controlTextObj;
	public GameObject continueObj;


	public Button beginButton;
	public Button controlButton;
	public Button replayButton;
	public Button menuButton;
	public Button hubButton;
	public Button continueButton;
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
		replayObj = GameObject.Find("ReplayButton1");
		if (replayObj == null) {
			replayObj = GameObject.Find("ReplayButton2");
		}
		if (replayObj != null) {
			replayButton = replayObj.GetComponent<Button> ();
			replayButton.onClick.AddListener (ReplayService);
		}
		hubObj = GameObject.Find ("HubButton");
		if (hubObj != null) {
			hubButton = hubObj.GetComponent<Button> ();
			hubButton.onClick.AddListener (HubService);
		}
		continueObj = GameObject.Find("ContinueButton");
		if (continueObj != null) {
			continueButton = continueObj.GetComponent<Button> ();
			continueButton.onClick.AddListener (ContinueService);
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
		if (replayObj.gameObject.name == "ReplayButton1") {
			LoadScene ("Level1");
		} else if (replayObj.gameObject.name == "ReplayButton2") {
			LoadScene ("Level2");
		}
	}

	private void MenuService() {
		LoadScene ("Menu");
	}

	private void HubService() {
		LoadScene ("Hub");
	}

	private void ContinueService() {
		LoadScene ("Level2");
	}

	void LoadScene(string level)
	{
		SceneManager.LoadScene(level);
	}
	// Update is called once per frame
	void Update () {

	}
}