﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

using TileType = LevelManager.TileType;

public class LevelFactory : MonoBehaviour {
	public static string NEXT_NAME = "GLORIOUS DICTATOR";

	public GameObject blankLevel = null;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	static void startLevel(GameObject level){
		Debug.Log ("START LEVEL");

		// Set level somewhere
		level.name = NEXT_NAME;
		DontDestroyOnLoad(level);
		Debug.Log ("Loading game scene with level " + level.name);
		SceneManager.LoadScene("Modest-Mudcake");
	}

	public void BuildLevelOne(){
		// Build Level
		GameObject levelObject = Instantiate(blankLevel);

		var level = levelObject.GetComponent<LevelManager>();
		level.finishIfNPlaced = 2;
		level.gameBoardHeight = 1;
		level.gameBoardWidth = 2;

        level.startMessage = "For this simple world, just place all of your tiles";
        level.victoryMessage = "Good job!";

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

	public void BuildLevelTwo(){
		// Build Level
		GameObject levelObject = Instantiate(blankLevel);

		LevelManager level = levelObject.GetComponent<LevelManager>();
		level.finishIfTypesPlaced = new Dictionary<TileType, int>{ 
			{TileType.Water, 3}
		};
		level.gameBoardHeight = 2;
		level.gameBoardWidth = 2;

        level.startMessage = "For this simple world we want all water";
        level.victoryMessage = "Good job!";

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

	public void BuildLevelThree(){
		// Build Level
		GameObject levelObject = Instantiate(blankLevel);

//		LevelManager level = levelObject.GetComponent<LevelManager>();
//		level.finishIfTypesPlaced = new Dictionary<TileType, int>{ 
//			{TileType.Water, 3}
//		};
//		level.gameBoardHeight = 2;
//		level.gameBoardWidth = 2;
//
//		TileType[,] testLevel = {
//			{TileType.Empty,TileType.Empty},
//			{TileType.Empty,TileType.Empty},
//		};
//		List<TileType> testTiles = new List<TileType> { 
//			TileType.Mountain, TileType.Grassland, TileType.Water,
//		};
//		level.LevelManagerInit(testLevel, testTiles);

		// Start the level created
		startLevel(levelObject);
	}

}
