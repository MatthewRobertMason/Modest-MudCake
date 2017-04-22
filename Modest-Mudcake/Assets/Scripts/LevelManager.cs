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
    private List<TileType> testTiles = new List<TileType> { TileType.Mountain, TileType.Mountain, TileType.Grassland, TileType.Grassland, 
                                                            TileType.Desert, TileType.Desert, TileType.River, TileType.River, 
                                                            TileType.Hills, TileType.Hills, TileType.Town, TileType.Town, 
                                                            TileType.Water, TileType.Water, TileType.Swamp, TileType.Swamp, 
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
    /*
    public void LevelManagerFactory(TileType[,] level, List<TileType> availableTiles) 
    {
        _level = testLevel;
        if (level != null)
            _level = level;

        _availableTiles = testTiles;
        if (availableTiles != null)
            _availableTiles = availableTiles;
    }
    */
    void Awake()
    {
        
    }

	void Start () 
    {
		/*
         * Build the board
         * Create the tiles
         * Assign the board to the tiles
         * Add tiles to hand
         */
        gameBoardWidth = _level.GetLength(1);
        gameBoardHeight = _level.Length;

        officialBoard = new GameObject[gameBoardHeight, gameBoardWidth];

        GameObject currentSocket = null;
        GameObject currentTile = null;

        for (int y = 0; y < gameBoardHeight; y++)
        {
            for (int x = 0; x < gameBoardWidth; x++)
            {
                if (_level[y,x] != TileType.Null)
                {
                    currentSocket = Instantiate(socketObject);
                    //socketObjects[y, x] = currentSocket;

                    currentSocket.transform.position = gameBoardObject.transform.position + 
                        new Vector3(x - ((float)gameBoardWidth / 2.0f), y - ((float)gameBoardHeight / 2.0f), 0);

                    switch(_level[y,x])
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
                    {
                        currentTile.transform.position = currentSocket.transform.position;
                        currentSocket.GetComponent<socketContext>().currentTile = currentTile;

                        currentTile.GetComponent<dragableTile>().board = gameBoardObject;
                    }
                }

                officialBoard[y, x] = currentSocket;
            }
        }
	}

    private TileType getTileType(int x, int y)
    {
        GameObject tile = officialBoard[y, x].GetComponent<socketContext>().currentTile;
        if (tile != null)
            return tile.GetComponent<dragableTile>().tileType;
        return TileType.Empty;
    }

    void Update() 
    {
		
	}

	TileType transition(TileType current, TileType catalyst){
		switch (current) {
		case TileType::Mountian:
			if (catalyst == Water)
				return Hills;
			break;

		case TileType::Grassland:
			switch (catalyst) {
			case Mountain:
				return River;
			case Desert:
				return Desert;
			case River:
				return Town;
			case Hills:
				return River;
			case Town:
				return Town;
			case Swamp: return Swamp;
			}
			break;

		case TileType::Desert:
			if (catalyst == River)
				return Grassland;
			if (catalyst == Swamp)
				return Grassland;
			if (catalyst == Mountain)
				return Hills;
			break;

		case TileType::River:
			if (catalyst == River)
				return Swamp;
			break;

		case Hills:
			switch (catalyst) {
			case Mountain:
				return Mountain;
			case River:
				return Grassland;
			case Water:
				return River;
			}
			break;

		case Town:
			switch (catalyst) {
			case Desert:
				return Desert;
			case Swamp:
				return Swamp;
			}
			break;

		case Water:
			switch (catalyst) {
			case Desert:
				return Swamp;
			}
			break;

		case Swamp:
			switch (catalyst) {
			case Desert:
				return Grassland;
			case River:
				return River;
			case Town:
				return Grassland;
			case Water:
				return Water;
			}
			break;
		}

		return current;
	}

	TileType chooseChange

	void UpdateBoard(Object changed, int newPosition){
		//
		int oldPosition = positionOf(changed);

		//
		if(oldPosition == newPosition) return;

		HashSet<Object> updated, finished;
		updated.Add (changed);

		fixedObjects.copyTo (finished);
		finished.Add (changed);

		Queue<Object> front;
		front.Enqueue(changed);

		//
		while(front.Count){
			// Get the next tile to process
			Object current = front.Dequeue();
			currentType = tileType (current);

			// Check if any of the changed tiles are adjacent to this one have changed
			HashSet<Object> couldChange;
			foreach (Object neighbour in getNeigbours(current)) {
				if(updated.Contains(neigbour)){
					newType = transition (tileType (current), tileType(neighbour));
					if (newType != currentType) {
						couldChange.Add (newType);
					}					
				}
			}
				
			// There is at least one thing to change into
			if (couldChange.Count) {
				// The current tile is changing, make sure neigbours that are not already
				// changed or forbidden to change are updated
				updated.Add (current);
				finished.Add (current);
				foreach (Object neighbour in getNeigbours(current)) {
					if (!finished.Contains (neighbour)) {
						front.Enqueue (neighbour);
					}
				}

				// 
				chooseChange(currentType, couldChange);
			}
		}
	}
}


