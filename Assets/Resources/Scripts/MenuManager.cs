using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

	private static GlobalManager stateManager = null;

	private static void createGlobalManager()
	{
		GameObject gameState = new GameObject();
		gameState.name = "GlobalStateManager";
		gameState.AddComponent<GlobalManager>();
		stateManager = gameState.GetComponent<GlobalManager>();
	}

	public static GlobalManager returnManager
	{
		get
		{
			return stateManager;
		}
	}

	void Awake()
	{
		if(stateManager == null)
		{
			createGlobalManager();
		}
	}
	// Use this for initialization
	void Start () {

	}
}
