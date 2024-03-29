﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBootstrap : MonoBehaviour {

    public GameSession gameSession = null;

	// Use this for initialization
	void Start () {
        gameSession = GameObject.Find("GameSession").GetComponent<GameSession>();
		Debug.Log("Starting Scene... Try to start level: " + LevelFactory.NEXT_NAME);
		GameObject level = GameObject.Find(LevelFactory.NEXT_NAME);
		if (level == null) {
			Debug.Log ("Couldn't load target level. Starting test level.");
			var factory = gameObject.GetComponent<LevelFactory>();
			factory.BuildLevelTest();
			return;
		}

		// Now that we have the level activate it
		var script = level.GetComponent<LevelManager> ();
		script.enabled = true;
		level.SetActive(true);

		// Bind the level to the buttons in the scene 
		var button = GameObject.Find("Reset Button").GetComponent<UnityEngine.UI.Button>();
		button.onClick.AddListener(script.reset);

		// Bind the level to the buttons in the scene 
		button = GameObject.Find("Abandon Button").GetComponent<UnityEngine.UI.Button>();
		button.onClick.AddListener(script.quit);

		// Bind the level to the buttons in the scene 
		button = GameObject.Find("Goal Button").GetComponent<UnityEngine.UI.Button>();
		button.onClick.AddListener(script.showObjective);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
