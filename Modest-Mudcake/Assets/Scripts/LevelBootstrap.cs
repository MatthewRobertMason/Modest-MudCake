using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBootstrap : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log("Starting Scene... Try to start level: " + LevelFactory.NEXT_NAME);
		GameObject level = GameObject.Find(LevelFactory.NEXT_NAME);
		var script = level.GetComponent<LevelManager> ();
		script.enabled = true;
		level.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
