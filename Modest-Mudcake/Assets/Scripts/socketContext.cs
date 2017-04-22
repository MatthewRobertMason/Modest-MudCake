using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class socketContext : MonoBehaviour 
{
    [Header("Neighbours")]
    public socketContext north = null;
    public socketContext east = null;
    public socketContext south = null;
    public socketContext west = null;

    LevelManager.tileType currentType;

	void Start () 
    {
	}
	
	void Update () 
    {
		
	}
}
