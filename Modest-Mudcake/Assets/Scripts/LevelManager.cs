using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour 
{
    /*
     * Initial Level Conditions
     * Layout of the board
     * --Manage adjency of the board (stored in socketContext)
     * Types and positions of tiles
     * Respond when any tile position change
     * Test for victory
     * Quit to level select
    */

    // The object that will hold the board sockets
    public GameObject gameBoardObject = null;
    public GameObject tileBench = null;
    public float tileBenchLength = 8;

    [Range(1,5)]
    public int gameBoardWidth = 4;
    [Range(1, 5)]
    public int gameBoardHeight = 4;

    private tileType[,] _level;
    private List<tileType> _availableTiles;

    private tileType[,] testLevel = {
                                        {tileType.Empty,tileType.Empty,tileType.Empty,tileType.Empty},
                                        {tileType.Empty,tileType.Empty,tileType.Empty,tileType.Empty},
                                        {tileType.Empty,tileType.Empty,tileType.Empty,tileType.Empty},
                                        {tileType.Empty,tileType.Empty,tileType.Empty,tileType.Empty}
                                    };
    private List<tileType> testTiles = new List<tileType> { tileType.Mountain, tileType.Mountain, tileType.Grassland, tileType.Grassland, 
                                                            tileType.Desert, tileType.Desert, tileType.River, tileType.River, 
                                                            tileType.Hills, tileType.Hills, tileType.Town, tileType.Town, 
                                                            tileType.Water, tileType.Water, tileType.Swamp, tileType.Swamp, 
                                                          };

    public GameObject socketObject = null;

    public GameObject mountain = null;
    public GameObject grassland = null;
    public GameObject desert = null;
    public GameObject river = null;
    public GameObject hills = null;
    public GameObject town = null;
    public GameObject water = null;
    public GameObject swamp = null;

    public enum tileType
    {
        Null,
        Empty,
        Mountain,
        Grassland,
        Desert,
        River,
        Hills,
        Town,
        Water,
        Swamp
    }

    public LevelManager(tileType[,] level, List<tileType> availableTiles) 
    {
        if (level != null)
            _level = level;
        else
            _level = testLevel;

        if (availableTiles != null)
            _availableTiles = availableTiles;
        else
            _availableTiles = testTiles;
    }

	void Start () 
    {
		/*
         * Build the board
         * Create the tiles
         * Assign the board to the tiles
         */
        gameBoardWidth = _level.GetLength(1);
        gameBoardHeight = _level.Length;

        GameObject[,] socketObjects = new GameObject[gameBoardHeight, gameBoardWidth];

        GameObject currentSocket = null;
        GameObject currentTiles = null;

        for (int y = 0; y < gameBoardHeight; y++)
        {
            for (int x = 0; x < gameBoardWidth; x++)
            {
                if (_level[y,x] != tileType.Null)
                {
                    currentSocket = Instantiate(socketObject);
                    socketObjects[y, x] = currentSocket;

                    currentSocket.transform.position = gameBoardObject.transform.position + 
                        new Vector3(x - ((float)gameBoardWidth / 2.0f), y - ((float)gameBoardHeight / 2.0f), 0);

                    switch(_level[y,x])
                    {
                        case tileType.Empty:
                            currentTiles = Instantiate(socketObject);
                            break;
                        case tileType.Mountain:
                            currentTiles = Instantiate(mountain);
                            break;
                        case tileType.Grassland:
                            currentTiles = Instantiate(grassland);
                            break;
                        case tileType.Desert:
                            currentTiles = Instantiate(desert);
                            break;
                        case tileType.River:
                            currentTiles = Instantiate(river);
                            break;
                        case tileType.Hills:
                            currentTiles = Instantiate(hills);
                            break;
                        case tileType.Town:
                            currentTiles = Instantiate(town);
                            break;
                        case tileType.Water:
                            currentTiles = Instantiate(water);
                            break;
                        case tileType.Swamp:
                            currentTiles = Instantiate(swamp);
                            break;
                    }
                }
            }
        }

	}

    void Update() 
    {
		
	}
}

