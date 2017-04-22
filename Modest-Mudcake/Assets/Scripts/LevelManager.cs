using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
