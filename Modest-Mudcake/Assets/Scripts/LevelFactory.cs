﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using TileType = LevelManager.TileType;

public class LevelFactory : MonoBehaviour {

	static GameObject blankLevel = null;

	// 
	void Awake() {
		if(blankLevel == null)
			blankLevel = Resources.Load ("Prefabs/LevelManager") as GameObject;
	}

	// Use this for initialization
	void Start () {
				
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	static void startLevel(GameObject level){
		Debug.Log ("START LEVEL");
		// Set level somewhere

		DontDestroyOnLoad(level);
		SceneManager.LoadScene("Modest-Mudcake");
	}

	public static void BuildLevelOne(){
		// Build Level
		GameObject levelObject = Instantiate(blankLevel);

		LevelManager level = levelObject.GetComponent<LevelManager>();
		level.finishIfNPlaced = 2;
		level.gameBoardHeight = 1;
		level.gameBoardWidth = 2;

		TileType[,] testLevel = {
			{TileType.Empty,TileType.Empty},
		};
		List<TileType> testTiles = new List<TileType> { 
			TileType.River, TileType.Hills, TileType.Swamp
		};
		level.LevelManagerInit(testLevel, testTiles);

		// Start the level created
		startLevel(levelObject);
	}

	public static void BuildLevelTwo(){
		// Build Level
		GameObject levelObject = Instantiate(blankLevel);

		LevelManager level = levelObject.GetComponent<LevelManager>();
		level.finishIfTypesPlaced = new Dictionary<TileType, int>{ 
			{TileType.Hills, 3}
		};
		level.gameBoardHeight = 2;
		level.gameBoardWidth = 2;

		TileType[,] testLevel = {
			{TileType.Empty,TileType.Empty},
			{TileType.Empty,TileType.Empty},
		};
		List<TileType> testTiles = new List<TileType> { 
			TileType.Mountain, TileType.Grassland, TileType.Water,
		};
		level.LevelManagerInit(testLevel, testTiles);

		// Start the level created
		startLevel(levelObject);
	}
}