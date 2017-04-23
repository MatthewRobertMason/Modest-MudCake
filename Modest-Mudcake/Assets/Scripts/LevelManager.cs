using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public GameSession gameSession = null;

    // The object that will hold the board sockets
	public GameObject gameBoardObjectPrefab = null;
	public GameObject tileBenchPrefab = null;
	public GameObject controlBarPrefab = null;

	protected GameObject gameBoardObject = null;
	protected GameObject tileBench = null;
	protected GameObject benchGraphic = null;

	public float tileBenchLength = 8;
	public int levelNumber = -1;

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
                                                            TileType.Mountain, TileType.Grassland, TileType.Desert, TileType.River,
                                                            TileType.Hills, TileType.Town, TileType.Water, TileType.Swamp
                                                          };

    public string startMessage;
    public string victoryMessage;

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


    // Message Box
    public GameObject messageBoxPrefab = null;
    private MessageBox messageBox = null;

	class TileChange {
		public TileChange(int _x, int _y, TileType _t){
			x = _x;
			y = _y;
			type = _t;
		}

		public int x;
		public int y;
		public TileType type;
	};

	// List of changes to the 
	Queue<TileChange> changeQueue = new Queue<TileChange>();
	protected const float tileChangeTime = 0.5f;
	TileChange currentChange = null;
	float currentChangeStart = 0;
	HashSet<GameObject> draggableTiles = new HashSet<GameObject>();

	// Assorted exit conditions
	public int finishIfNPlaced = -1;
	public Dictionary<TileType, int> finishIfTypesPlaced = null; 


    public bool initializedLevel = false;

    public void LevelManagerInit(TileType[,] level, List<TileType> availableTiles)
    {
        if (level != null)
            _level = level;

        if (availableTiles != null)
            _availableTiles = availableTiles;
        
        initializedLevel = true;
    }

	void Start ()
    {
        gameSession = GameObject.Find("GameSession").GetComponent<GameSession>();

		Debug.Log("LevelManager::Start");
        if (!initializedLevel)
            LevelManagerInit(testLevel, testTiles);

		gameBoardObject = Instantiate(gameBoardObjectPrefab);
		tileBench = Instantiate(tileBenchPrefab);
		foreach (Transform child in tileBench.transform)
			benchGraphic = child.gameObject;

		/*
         * Build the board
         * Create the tiles
         * Assign the board to the tiles
         * Add tiles to hand
         */
        gameBoardWidth = _level.GetLength(1);
        gameBoardHeight = _level.GetLength(0);

		reset();

        // Attempt to load the MessageBox
        GameObject mb = Instantiate(messageBoxPrefab);
        if (mb != null)
        {
            messageBox = mb.GetComponent<MessageBox>();
            messageBox.text = startMessage;
            messageBox.buttonText = "OK!";
            //messageBox.transform.gameObject.SetActive(true);
            messageBox.SetVisible(true);
        }
        else
            messageBox = null;
	}

	public void reset(){
		foreach (var tile in Object.FindObjectsOfType<dragableTile>()) {
			Destroy(tile.gameObject);
		}

		if (officialBoard != null) {
			foreach (var slot in officialBoard) {
				Destroy (slot);
			}
		}

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
						new Vector3(x - ((float)gameBoardWidth / 2.0f) + 0.5f, y - ((float)gameBoardHeight / 2.0f) + 0.5f, 0);

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

		benchGraphic.transform.localScale = new Vector3(tileBenchLength*2+2, 1, 1);

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
			temp.transform.position = tileBench.transform.position + new Vector3(-benchStart + (spacing/2) + (i*spacing), 0, 0);
			temp.GetComponent<SpriteRenderer>().sortingOrder = i+1;
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

	private GameObject setTileType(int x, int y, TileType type)
	{
        socketContext sc = officialBoard[y, x].GetComponent<socketContext>();
		bool inDraggable = draggableTiles.Contains (sc.currentTile);
		if (inDraggable)
			draggableTiles.Remove (sc.currentTile);
		sc.currentTile.GetComponent<dragableTile>().Destroy();

        GameObject newTile = createTile(type, gameBoardObject);
        newTile.transform.position = officialBoard[y, x].transform.position;
        sc.currentTile = newTile;
        sc.currentTile.GetComponent<dragableTile>().currentSocket = officialBoard[y, x];
		if (inDraggable)
			draggableTiles.Add (sc.currentTile);

        return newTile;
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

	bool hasCompletedLevel = false;

    void Update()
    {
		// Check if the map is finished, only enter this this once, when
		// we first discover it is done
		if (isFinished () && !hasCompletedLevel) {
			gameSession.FinishedLevel (levelNumber);
			hasCompletedLevel = true;
			messageBox.text = victoryMessage;
			messageBox.buttonText = "OK!";
			messageBox.SetVisible(true);
		}

		// The map is completed, keep checking if the player has closed the message box
		if (hasCompletedLevel && !messageBox.IsVisible()){
            // Return to menu
            Destroy(gameObject);
            //SceneManager.LoadScene("level-menu");
            gameSession.ChangeLevel("level-menu");
		}

		if (currentChange != null) {
			GameObject newTile = setTileType (currentChange.x, currentChange.y, currentChange.type);
            newTile.GetComponent<ParticleSystem>().Play();

			if (currentChangeStart + tileChangeTime < Time.time) {
				currentChange = null;
				enableDragging ();
			}
		} 
			
		if (changeQueue.Count > 0 && currentChange == null) {
			currentChange = changeQueue.Dequeue ();	
			currentChangeStart = Time.time;
			disableDragging();
		}
	}

	void disableDragging(){
		draggableTiles.Clear();
		foreach (var tile in Object.FindObjectsOfType<dragableTile>()) {
			var draggable = tile as dragableTile;
			if (draggable.dragable && draggable.gameObject != null) {
				draggableTiles.Add (draggable.gameObject);
				draggable.dragable = false;
			}
		}
	}

	void enableDragging(){
		if(draggableTiles != null){
			foreach (var go in draggableTiles) {
				if(go != null)
					go.GetComponent<dragableTile> ().dragable = true;
			}
			draggableTiles.Clear();
		}
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
			if (catalyst == TileType.Water)
				return TileType.Water;
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

		var updated = new Dictionary<Pair, TileType>();
		var finished = new HashSet<Pair>();
		updated[changed] = getTileType(x, y);

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
				if(updated.ContainsKey(neighbour)){
					TileType neighbourType = updated[neighbour];
					TileType newType = transition(currentType, neighbourType);

					if (newType != currentType) {
						couldChange.Add (newType);
					}
				}
			}

			// There is at least one thing to change into
			if (couldChange.Count > 0) {
				// The current tile is changing, make sure neigbours that are not already
				// changed or forbidden to change are updated
				finished.Add (current);
				foreach (Pair neighbour in getNeighbours(current)) {
					if (!finished.Contains (neighbour)) {
						front.Enqueue (neighbour);
					}
				}

				//
				TileType type = chooseChange(currentType, couldChange);
				updated [current] = type;

				changeQueue.Enqueue (new TileChange (current.Key, current.Value, type));
			}
		}
	}

	bool isFinished(){
		if (finishIfNPlaced > 0)
			if (countAllTiles () >= finishIfNPlaced)
				return true;

		if (finishIfTypesPlaced != null) {
			bool allMatched = true;
			foreach(TileType key in finishIfTypesPlaced.Keys){
				allMatched &= countTiles(key) >= finishIfTypesPlaced[key];
			}
			return allMatched;
		}

		return false;
	}

	int countAllTiles(){
		int count = 0;
		for (int x = 0; x < gameBoardWidth; x++) {
			for (int y = 0; y < gameBoardHeight; y++) {
				TileType type = getTileType (x, y);
				count += (type != TileType.Null && type != TileType.Empty) ? 1 : 0;
			}			
		}
		return count;
	}

	int countTiles(TileType target){
		int count = 0;
		for (int x = 0; x < gameBoardWidth; x++) {
			for (int y = 0; y < gameBoardHeight; y++) {
				TileType type = getTileType (x, y);
				count += (type == target ? 1 : 0);
			}			
		}
		return count;
	}

	public void quit(){
		Destroy (this.gameObject);
		//SceneManager.LoadScene ("level-menu");
        gameSession.ChangeLevel("level-menu");
	}
}
