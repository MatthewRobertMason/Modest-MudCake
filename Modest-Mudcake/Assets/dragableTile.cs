using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragableTile : MonoBehaviour 
{
    public bool dragable = true;
    public bool held = false;
	
	void Start () 
    {
		
	}
	
	void Update () 
    {
		
	}

    void OnMouseDown()
    {
        held = true;
        if (dragable)
        {
            
        }
    }

    void OnMouseUp()
    {
        held = false;
        
        if (dragable)
        {
            // attempt to socket to nearest game slot
        }
    }
}
