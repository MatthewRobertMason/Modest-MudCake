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

        level.startMessage = "For this simple world, just drag any tiles from the " +
        	"bottom into the two spaces.";
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

	public void BuildLevelTwoA(){
		// Build Level
		GameObject levelObject = Instantiate(blankLevel);

		LevelManager level = levelObject.GetComponent<LevelManager>();
		level.finishIfTypesPlaced = new Dictionary<TileType, int>{ 
			{TileType.River, 1}
		};
		level.gameBoardHeight = 1;
		level.gameBoardWidth = 3;
		level.levelNumber = 2;

		level.startMessage = "If you place a mountain beside grassland a river will run down from it.\n\n" +
			"Effects only occur when a tile is placed; and only on other tiles.";
		level.victoryMessage = "Good job!";

		TileType[,] testLevel = {
			{TileType.Empty, TileType.Empty, TileType.Empty},
		};
		List<TileType> testTiles = new List<TileType> { 
			TileType.Mountain, TileType.Grassland,
		};
		level.LevelManagerInit(testLevel, testTiles);

		// Start the level created
		startLevel(levelObject);
	}

	public void BuildLevelTwoB(){
		// Build Level
		GameObject levelObject = Instantiate(blankLevel);

		LevelManager level = levelObject.GetComponent<LevelManager>();
		level.finishIfTypesPlaced = new Dictionary<TileType, int>{ 
			{TileType.Water, 2}
		};
		level.gameBoardHeight = 2;
		level.gameBoardWidth = 2;
		level.levelNumber = 3;

		level.startMessage = "For this simple world we want all water.\n\n" +
			"Placing water next to many things will bring them closer to water.";
		level.victoryMessage = "Good job!";

		TileType[,] testLevel = {
			{TileType.Empty, TileType.Empty},
			{TileType.Empty, TileType.Empty},
		};
		List<TileType> testTiles = new List<TileType> { 
			TileType.Mountain, TileType.Water,
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
		level.levelNumber = 4;

		level.startMessage = "For this simple world we want all water.\n\n" +
			"Each tile reacts differently to others; if at all.\n\n" +
			"Remember there is a reset button if you get stuck.";
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
		level.levelNumber = 5;

		level.startMessage = "Make a big swamp.\n\n" +
			"You can do it!";
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

	public void BuildLevelFourA(){
		// Build Level
		GameObject levelObject = Instantiate(blankLevel);

		LevelManager level = levelObject.GetComponent<LevelManager>();
		level.finishIfTypesPlaced = new Dictionary<TileType, int>{ 
			{TileType.Mountain, 3}
		};
		level.gameBoardHeight = 2;
		level.gameBoardWidth = 2;
		level.levelNumber = 6;

		level.startMessage = "You need three mountains on this world.\n\n" +
			"Both hills and deserts can be raised into mountains if you already have one.";
		level.victoryMessage = "Good job!";

		TileType[,] testLevel = {
			{TileType.Empty,TileType.Desert},
			{TileType.Hills,TileType.Empty},
		};
		List<TileType> testTiles = new List<TileType> { 
			TileType.Mountain,
		};
		level.LevelManagerInit(testLevel, testTiles);

		// Start the level created
		startLevel(levelObject);
	}

	public void BuildLevelFourB(){
		// Build Level
		GameObject levelObject = Instantiate(blankLevel);

		LevelManager level = levelObject.GetComponent<LevelManager>();
		level.finishIfTypesPlaced = new Dictionary<TileType, int>{ 
			{TileType.Desert, 4}
		};
		level.gameBoardHeight = 3;
		level.gameBoardWidth = 2;
		level.levelNumber = 7;

		level.startMessage = "Your goal all deserts.\n\n" +
			"Swamps and seas can be dried up into deserts eventually.";
		level.victoryMessage = "Good job!";

		TileType[,] testLevel = {
			{TileType.Empty, TileType.Empty, TileType.Empty},
			{TileType.Empty, TileType.Empty, TileType.Empty},
		};
		List<TileType> testTiles = new List<TileType> { 
			TileType.Swamp, TileType.Desert, TileType.Water,
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
		level.levelNumber = 8;

		level.startMessage = "Fill this world with mountains.";
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
		level.levelNumber = 9;

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

	public void BuildLevelSeven(){
		// Build Level
		GameObject levelObject = Instantiate(blankLevel);

		LevelManager level = levelObject.GetComponent<LevelManager>();
		level.finishIfTypesPlaced = new Dictionary<TileType, int>{ 
			{TileType.Mountain, 6}
		};
		level.gameBoardHeight = 3;
		level.gameBoardWidth = 3;
		level.levelNumber = 11;
		
		level.startMessage = "";
		level.victoryMessage = "";

		TileType[,] testLevel = {
			{TileType.Hills, TileType.Empty, TileType.Swamp},
			{TileType.Swamp, TileType.Null, TileType.Empty},
			{TileType.Water, TileType.Empty, TileType.Swamp},
		};
		List<TileType> testTiles = new List<TileType> { 
			TileType.Mountain,TileType.Desert, TileType.Water, TileType.Swamp, TileType.River, TileType.Water,
		};
		level.LevelManagerInit(testLevel, testTiles);

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
		level.levelNumber = 12;
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
		level.levelNumber = 13;
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
		level.levelNumber = 14;
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
