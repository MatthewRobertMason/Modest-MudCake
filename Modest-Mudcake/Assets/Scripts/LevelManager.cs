using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pair = System.Collections.Generic.KeyValuePair<int, int>;

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

    [Range(1, 5)]
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

        for (int y = 0; y < gameBoardHeight; y++)
        {
            for (int x = 0; x < gameBoardWidth; x++)
            {
				GameObject currentSocket = null;

                if (_level[y,x] != TileType.Null)
                {
                    currentSocket = Instantiate(socketObject, gameBoardObject.transform);

                    currentSocket.transform.position = gameBoardObject.transform.position +
                        new Vector3(x - ((float)gameBoardWidth / 2.0f), y - ((float)gameBoardHeight / 2.0f), 0);

					GameObject currentTile = null;

					if ((_level[y, x] != TileType.Empty) && (_level[y, x] != TileType.Null))
						currentTile = createTile(_level[y, x], gameBoardObject);

                    socketContext sc = currentSocket.GetComponent<socketContext>();
                    sc.x = x;
                    sc.y = y;

                    if (currentTile != null)
                    {
                        currentTile.transform.position = currentSocket.transform.position;
                        currentSocket.GetComponent<socketContext>().currentTile = currentTile;
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
            temp.GetComponent<SpriteRenderer>().sortingOrder = i;
            i ++;
        }
	}

	private TileType getTileType(int x, int y)
	{
		if (officialBoard [y, x] == null)
			return TileType.Null;

		GameObject tile = officialBoard[y, x].GetComponent<socketContext>().currentTile;
		if (tile != null)
			return tile.GetComponent<dragableTile>().tileType;
		
		return TileType.Empty;
	}

	private void setTileType(int x, int y, TileType type)
	{
        socketContext sc = officialBoard[y, x].GetComponent<socketContext>();
        Destroy(sc.currentTile);
        GameObject newTile = createTile(type, gameBoardObject);
        newTile.transform.position = officialBoard[y, x].transform.position;
        sc.currentTile = newTile;
        

		Debug.Log("SET TILE TYPE");
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
        {
            currentTile.GetComponent<dragableTile>().board = board;
            currentTile.GetComponent<dragableTile>().level = this;
        }

        return currentTile;
    }

    void Update()
    {

	}

	TileType transition(TileType current, TileType catalyst){
		switch (current) {
		case TileType.Mountain:
			if (catalyst == TileType.Water)
				return TileType.Hills;
			break;

		case TileType.Grassland:
			switch (catalyst) {
			case TileType.Mountain:
				return TileType.River;
			case TileType.Desert:
				return TileType.Desert;
			case TileType.River:
				return TileType.Town;
			case TileType.Hills:
				return TileType.River;
			case TileType.Town:
				return TileType.Town;
			case TileType.Swamp:
				return TileType.Swamp;
			}
			break;

		case TileType.Desert:
			if (catalyst == TileType.River)
				return TileType.Grassland;
			if (catalyst == TileType.Swamp)
				return TileType.Grassland;
			if (catalyst == TileType.Mountain)
				return TileType.Hills;
			break;

		case TileType.River:
			if (catalyst == TileType.River)
				return TileType.Swamp;
			break;

		case TileType.Hills:
			switch (catalyst) {
			case TileType.Mountain:
				return TileType.Mountain;
			case TileType.River:
				return TileType.Grassland;
			case TileType.Water:
				return TileType.River;
			}
			break;

		case TileType.Town:
			switch (catalyst) {
			case TileType.Desert:
				return TileType.Desert;
			case TileType.Swamp:
				return TileType.Swamp;
			}
			break;

		case TileType.Water:
			switch (catalyst) {
			case TileType.Desert:
				return TileType.Swamp;
			}
			break;

		case TileType.Swamp:
			switch (catalyst) {
			case TileType.Desert:
				return TileType.Grassland;
			case TileType.River:
				return TileType.River;
			case TileType.Town:
				return TileType.Grassland;
			case TileType.Water:
				return TileType.Water;
			}
			break;
		}

		return current;
	}

	TileType chooseChange(TileType old, HashSet<TileType> newValues){
		// TODO for determinism each possible set of transitions for a tile should have a priority
		TileType[] data = new TileType[newValues.Count];
		newValues.CopyTo (data);
		return data [0];
	}

	List<Pair> getNeighbours(Pair center){
		Pair[] offsets = {
			new Pair(1, 0),
			new Pair(-1, 0),
			new Pair(0, 1),
			new Pair(0, -1),
		};

		List<Pair> output = new List<Pair>();

		foreach(Pair offset	in offsets){
			int x = center.Key + offset.Key;
			int y = center.Value + offset.Value;
			if (0 > x || x >= gameBoardWidth || 0 > y || y >= gameBoardHeight)
				continue;
			TileType type = getTileType(x, y);
			if (type != TileType.Null)
				output.Add (new Pair(x, y));
		}
		return output;
	}

	public void MoveTile(int x, int y){
		Pair changed = new Pair(x, y);

		//
		// Pair oldPosition = positionOf(tile);
		//
		//
		// if(oldPosition == newPosition) return;
		//

		HashSet<Pair> updated = new HashSet<Pair>();
		HashSet<Pair> finished = new HashSet<Pair>();
		updated.Add (changed);

		// fixedObjects.copyTo (finished);
		finished.Add (changed);

		Queue<Pair> front = new Queue<Pair>();
		foreach(Pair neighbour in getNeighbours(changed))
			front.Enqueue(neighbour);

		//
		while(front.Count > 0){
			// Get the next tile to process
			Pair current = front.Dequeue();
			TileType currentType = getTileType(current.Key, current.Value);
			if (currentType == TileType.Null)
				continue;

			// Check if any of the changed tiles are adjacent to this one have changed
			HashSet<TileType> couldChange = new HashSet<TileType>();
			foreach (Pair neighbour in getNeighbours(current)) {
				if(updated.Contains(neighbour)){
					TileType newType = transition(currentType, getTileType(neighbour.Key, neighbour.Value));

					if (newType != currentType) {
						couldChange.Add (newType);
					}
				}
			}

			// There is at least one thing to change into
			if (couldChange.Count > 0) {
				// The current tile is changing, make sure neigbours that are not already
				// changed or forbidden to change are updated
				updated.Add (current);
				finished.Add (current);
				foreach (Pair neighbour in getNeighbours(current)) {
					if (!finished.Contains (neighbour)) {
						front.Enqueue (neighbour);
					}
				}

				//
				TileType type = chooseChange(currentType, couldChange);
				setTileType(current.Key, current.Value, type);
			}
		}
	}
}
