using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEditor;

using TileType = LevelManager.TileType;

public class LevelFactory : MonoBehaviour {
    public GameSession gameSession = null;

	public static string NEXT_NAME = "GLORIOUS DICTATOR";

	public GameObject blankLevel = null;

	// Use this for initialization
	void Start () {
        gameSession = GameObject.Find("GameSession").GetComponent<GameSession>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void startLevel(GameObject level){
		Debug.Log ("START LEVEL");

		// Set level somewhere
		level.name = NEXT_NAME;
		DontDestroyOnLoad(level);
		Debug.Log ("Loading game scene with level " + level.name);
        
        //SceneManager.LoadScene("Modest-Mudcake"); 
        gameSession.ChangeLevel("Modest-Mudcake");
	}

	public void BuildLevelOne(){
		// Build Level
		GameObject levelObject = Instantiate(blankLevel);

		var level = levelObject.GetComponent<LevelManager>();
		level.finishIfNPlaced = 2;
		level.gameBoardHeight = 1;
		level.gameBoardWidth = 2;
		level.levelNumber = 1;

        level.startMessage = "For this simple world, just drag any tiles from the bottom into the two spaces.";
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

//	public void BuildLevel1_5(){
//		// Build Level
//		GameObject levelObject = Instantiate(blankLevel);
//
//		LevelManager level = levelObject.GetComponent<LevelManager>();
//		level.finishIfTypesPlaced = new Dictionary<TileType, int>{ 
//			{TileType.Water, 3}
//		};
//		level.gameBoardHeight = 3;
//		level.gameBoardWidth = 3;
//
//		level.startMessage = "For this simple world we want all water";
//		level.victoryMessage = "Good job!";
//
//		TileType[,] testLevel = {
//			{TileType.Empty,TileType.Empty},
//			{TileType.Empty,TileType.Empty},
//		};
//		List<TileType> testTiles = new List<TileType> { 
//			TileType.Mountain, TileType.Grassland, TileType.Water,
//		};
//		level.LevelManagerInit(testLevel, testTiles);
//
//		// Start the level created
//		startLevel(levelObject);
//	}

	public void BuildLevelTwo(){
		// Build Level
		GameObject levelObject = Instantiate(blankLevel);

		LevelManager level = levelObject.GetComponent<LevelManager>();
		level.finishIfTypesPlaced = new Dictionary<TileType, int>{ 
			{TileType.Water, 3}
		};
		level.gameBoardHeight = 2;
		level.gameBoardWidth = 2;
		level.levelNumber = 2;

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

		LevelManager level = levelObject.GetComponent<LevelManager>();
		level.finishIfTypesPlaced = new Dictionary<TileType, int>{ 
			{TileType.Swamp, 3}
		};
		level.gameBoardHeight = 2;
		level.gameBoardWidth = 3;
		level.levelNumber = 3;

		level.startMessage = "Make a big swamp.";
		level.victoryMessage = "Good job!";

		TileType[,] testLevel = {
			{TileType.Empty,TileType.Empty, TileType.Empty},
			{TileType.Empty,TileType.Empty, TileType.Empty},
		};
		List<TileType> testTiles = new List<TileType> { 
			TileType.River,TileType.River, TileType.Grassland, TileType.Swamp,
		};
		level.LevelManagerInit(testLevel, testTiles);

		// Start the level created
		startLevel(levelObject);
	}

	public void BuildLevelFour(){
		// Build Level
		GameObject levelObject = Instantiate(blankLevel);

		LevelManager level = levelObject.GetComponent<LevelManager>();
		level.finishIfTypesPlaced = new Dictionary<TileType, int>{ 
			{TileType.Mountain, 6}
		};
		level.gameBoardHeight = 3;
		level.gameBoardWidth = 3;
		level.levelNumber = 4;

		level.startMessage = "Mountains for miles.";
		level.victoryMessage = "Good job!";

		TileType[,] testLevel = {
			{TileType.Empty,TileType.Empty, TileType.Empty},
			{TileType.Empty,TileType.Empty, TileType.Empty},
			{TileType.Empty,TileType.Empty, TileType.Empty},
		};
		List<TileType> testTiles = new List<TileType> { 
			TileType.Mountain,TileType.Desert, TileType.Water, TileType.Water, TileType.Water, TileType.Water,
		};
		level.LevelManagerInit(testLevel, testTiles);

		// Start the level created
		startLevel(levelObject);
	}

	public void BuildLevelFive(){
		// Build Level
		GameObject levelObject = Instantiate(blankLevel);

		LevelManager level = levelObject.GetComponent<LevelManager>();
		level.finishIfTypesPlaced = new Dictionary<TileType, int>{ 
			{TileType.Town, 2}
		};
		level.gameBoardHeight = 4;
		level.gameBoardWidth = 3;
		level.levelNumber = 5;

		level.startMessage = "Two towns should be enough.";
		level.victoryMessage = "Good job!";

		TileType[,] testLevel = {
			{TileType.Null,TileType.Null, TileType.Empty},
			{TileType.Empty,TileType.Desert, TileType.Desert},
			{TileType.Null,TileType.Null, TileType.Empty},
		};
		List<TileType> testTiles = new List<TileType> { 
			TileType.Mountain, TileType.Water, TileType.Town,
		};
		level.LevelManagerInit(testLevel, testTiles);

		// Start the level created
		startLevel(levelObject);
	}

	public void BuildLevelSix(){
		// Build Level
		GameObject levelObject = Instantiate(blankLevel);

		LevelManager level = levelObject.GetComponent<LevelManager>();
		//		level.finishIfTypesPlaced = new Dictionary<TileType, int>{ 
		//			{TileType.Mountain, 6}
		//		};
		//		level.gameBoardHeight = 3;
		//		level.gameBoardWidth = 3;
		level.levelNumber = 6;
		//
		//		level.startMessage = "Mountains for miles.";
		//		level.victoryMessage = "Good job!";
		//
		//		TileType[,] testLevel = {
		//			{TileType.Empty,TileType.Empty, TileType.Empty},
		//			{TileType.Empty,TileType.Empty, TileType.Empty},
		//			{TileType.Empty,TileType.Empty, TileType.Empty},
		//		};
		//		List<TileType> testTiles = new List<TileType> { 
		//			TileType.Mountain,TileType.Desert, TileType.Water, TileType.Water, TileType.Water, TileType.Water,
		//		};
		//		level.LevelManagerInit(testLevel, testTiles);
		//
		// Start the level created
		startLevel(levelObject);
	}

	public void BuildLevelSeven(){
		// Build Level
		GameObject levelObject = Instantiate(blankLevel);

		LevelManager level = levelObject.GetComponent<LevelManager>();
		//		level.finishIfTypesPlaced = new Dictionary<TileType, int>{ 
		//			{TileType.Mountain, 6}
		//		};
		//		level.gameBoardHeight = 3;
		//		level.gameBoardWidth = 3;
		level.levelNumber = 7;
		//
		//		level.startMessage = "Mountains for miles.";
		//		level.victoryMessage = "Good job!";
		//
		//		TileType[,] testLevel = {
		//			{TileType.Empty,TileType.Empty, TileType.Empty},
		//			{TileType.Empty,TileType.Empty, TileType.Empty},
		//			{TileType.Empty,TileType.Empty, TileType.Empty},
		//		};
		//		List<TileType> testTiles = new List<TileType> { 
		//			TileType.Mountain,TileType.Desert, TileType.Water, TileType.Water, TileType.Water, TileType.Water,
		//		};
		//		level.LevelManagerInit(testLevel, testTiles);
		//
		// Start the level created
		startLevel(levelObject);
	}

	public void BuildLevelEight(){
		// Build Level
		GameObject levelObject = Instantiate(blankLevel);

		LevelManager level = levelObject.GetComponent<LevelManager>();
		//		level.finishIfTypesPlaced = new Dictionary<TileType, int>{ 
		//			{TileType.Mountain, 6}
		//		};
		//		level.gameBoardHeight = 3;
		//		level.gameBoardWidth = 3;
		level.levelNumber = 8;
		//
		//		level.startMessage = "Mountains for miles.";
		//		level.victoryMessage = "Good job!";
		//
		//		TileType[,] testLevel = {
		//			{TileType.Empty,TileType.Empty, TileType.Empty},
		//			{TileType.Empty,TileType.Empty, TileType.Empty},
		//			{TileType.Empty,TileType.Empty, TileType.Empty},
		//		};
		//		List<TileType> testTiles = new List<TileType> { 
		//			TileType.Mountain,TileType.Desert, TileType.Water, TileType.Water, TileType.Water, TileType.Water,
		//		};
		//		level.LevelManagerInit(testLevel, testTiles);
		//
		// Start the level created
		startLevel(levelObject);
	}

	public void BuildLevelNine(){
		// Build Level
		GameObject levelObject = Instantiate(blankLevel);

		LevelManager level = levelObject.GetComponent<LevelManager>();
		//		level.finishIfTypesPlaced = new Dictionary<TileType, int>{ 
		//			{TileType.Mountain, 6}
		//		};
		//		level.gameBoardHeight = 3;
		//		level.gameBoardWidth = 3;
		level.levelNumber = 9;
		//
		//		level.startMessage = "Mountains for miles.";
		//		level.victoryMessage = "Good job!";
		//
		//		TileType[,] testLevel = {
		//			{TileType.Empty,TileType.Empty, TileType.Empty},
		//			{TileType.Empty,TileType.Empty, TileType.Empty},
		//			{TileType.Empty,TileType.Empty, TileType.Empty},
		//		};
		//		List<TileType> testTiles = new List<TileType> { 
		//			TileType.Mountain,TileType.Desert, TileType.Water, TileType.Water, TileType.Water, TileType.Water,
		//		};
		//		level.LevelManagerInit(testLevel, testTiles);
		//
		// Start the level created
		startLevel(levelObject);
	}

	public void BuildLevelTen(){
		// Build Level
		GameObject levelObject = Instantiate(blankLevel);

		LevelManager level = levelObject.GetComponent<LevelManager>();
		//		level.finishIfTypesPlaced = new Dictionary<TileType, int>{ 
		//			{TileType.Mountain, 6}
		//		};
		//		level.gameBoardHeight = 3;
		//		level.gameBoardWidth = 3;
		level.levelNumber = 10;
		//
		//		level.startMessage = "Mountains for miles.";
		//		level.victoryMessage = "Good job!";
		//
		//		TileType[,] testLevel = {
		//			{TileType.Empty,TileType.Empty, TileType.Empty},
		//			{TileType.Empty,TileType.Empty, TileType.Empty},
		//			{TileType.Empty,TileType.Empty, TileType.Empty},
		//		};
		//		List<TileType> testTiles = new List<TileType> { 
		//			TileType.Mountain,TileType.Desert, TileType.Water, TileType.Water, TileType.Water, TileType.Water,
		//		};
		//		level.LevelManagerInit(testLevel, testTiles);
		//
		// Start the level created
		startLevel(levelObject);
	}

	public void BuildLevelTest(){
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
