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

    // Holds the actual socket objects
    GameObject[,] officialBoard;

    private TileType[,] _level;
    private List<TileType> _availableTiles;

    private TileType[,] testLevel = {
                                        {TileType.Null,TileType.Empty,TileType.Empty,TileType.Null},
                                        {TileType.Empty,TileType.Empty,TileType.Empty,TileType.Empty},
                                        {TileType.Empty,TileType.Empty,TileType.Empty,TileType.Empty},
                                        {TileType.Null,TileType.Empty,TileType.Empty,TileType.Null}
                                    };
    private List<TileType> testTiles = new List<TileType> { TileType.Mountain, TileType.Grassland, TileType.Desert, TileType.River, 
                                                            TileType.Hills, TileType.Town, TileType.Water, TileType.Swamp, 
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

    public enum TileType
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

    public bool initializedLevel = false;

    public void LevelManagerFactory(TileType[,] level, List<TileType> availableTiles) 
    {
        if (level != null)
            _level = level;

        if (availableTiles != null)
            _availableTiles = availableTiles;

        initializedLevel = true;
    }

	void Start () 
    {
        if (!initializedLevel)
            LevelManagerFactory(testLevel, testTiles);

		/*
         * Build the board
         * Create the tiles
         * Assign the board to the tiles
         * Add tiles to hand
         */
        gameBoardWidth = _level.GetLength(1);
        gameBoardHeight = _level.GetLength(0);

        officialBoard = new GameObject[gameBoardHeight, gameBoardWidth];

        GameObject currentSocket = null;
        socketContext sc = null;
        GameObject currentTile = null;

        for (int y = 0; y < gameBoardHeight; y++)
        {
            for (int x = 0; x < gameBoardWidth; x++)
            {
                if (_level[y,x] != TileType.Null)
                {
                    currentSocket = Instantiate(socketObject, gameBoardObject.transform);

                    currentSocket.transform.position = gameBoardObject.transform.position + 
                        new Vector3(x - ((float)gameBoardWidth / 2.0f), y - ((float)gameBoardHeight / 2.0f), 0);

                    sc = currentSocket.GetComponent<socketContext>();
                    sc.x = x;
                    sc.y = y;

                    if ((_level[y, x] != TileType.Empty) && (_level[y, x] != TileType.Null))
                        currentTile = createTile(_level[y, x], gameBoardObject);
                    
                    if (currentTile != null)
                    {
                        currentTile.transform.position = currentSocket.transform.position;
                        currentSocket.GetComponent<socketContext>().currentTile = currentTile;

                        currentTile.GetComponent<dragableTile>().board = gameBoardObject;
                    }
                }

                officialBoard[y, x] = currentSocket;
            }
        }
        
        // Add tiles to the bench
        float benchStart = -5.0f;
        float spacing = 1.0f;

        if (_availableTiles.Count < tileBenchLength)
        {
            benchStart = ((float)_availableTiles.Count / 2.0f);
            spacing = 1.0f;
        }   
        else if (_availableTiles.Count > 0)
        {
            benchStart = ((float)tileBenchLength/2.0f);
            spacing = (float)tileBenchLength / (float)_availableTiles.Count;   
        }

        int i = 0;
        foreach (TileType t in _availableTiles)
        {
            GameObject temp = createTile(t, gameBoardObject);
            temp.transform.position = tileBench.transform.position + new Vector3(-benchStart + (i*spacing), 0, 0);
            i ++;
        }
	}

    private TileType getTileType(int x, int y)
    {
        GameObject tile = officialBoard[y, x].GetComponent<socketContext>().currentTile;
        if (tile != null)
            return tile.GetComponent<dragableTile>().tileType;
        return TileType.Empty;
    }

    private GameObject createTile(TileType t, GameObject board)
    {
        GameObject currentTile = null;

        switch (t)
        {
            case TileType.Mountain:
                currentTile = Instantiate(mountain);
                break;
            case TileType.Grassland:
                currentTile = Instantiate(grassland);
                break;
            case TileType.Desert:
                currentTile = Instantiate(desert);
                break;
            case TileType.River:
                currentTile = Instantiate(river);
                break;
            case TileType.Hills:
                currentTile = Instantiate(hills);
                break;
            case TileType.Town:
                currentTile = Instantiate(town);
                break;
            case TileType.Water:
                currentTile = Instantiate(water);
                break;
            case TileType.Swamp:
                currentTile = Instantiate(swamp);
                break;
        }

        if (currentTile != null)
            currentTile.GetComponent<dragableTile>().board = board;

        return currentTile;
    }

    void Update() 
    {
		
	}
}


