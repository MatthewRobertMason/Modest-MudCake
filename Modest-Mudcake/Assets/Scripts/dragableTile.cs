﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragableTile : MonoBehaviour 
{
    public bool dragable = true;
    public float snapDistance = 1.0f;

    public GameObject board = null;
	public LevelManager level = null;

    private AudioSource audioSource = null;
    public AudioClip placeSound = null;
    public AudioClip badPlaceSound = null;

    private Vector3 originalPosition;
    public bool held = false;

    public LevelManager.TileType tileType;

	void Start () 
    {
        audioSource = GetComponent<AudioSource>();
	}
	
	void Update () 
    {
		
	}

    void OnMouseDown()
    {
        if (dragable)
        {
            held = true;
            originalPosition = this.transform.position;
        }
    }

    void OnMouseUp()
    {
        held = false;
        
        if (dragable)
        {
            GameObject nearest = nearestSocket();

            if (nearest != null)
            {
                if (Vector3.Distance(this.transform.position, nearest.transform.position) < snapDistance)
                {
                    this.transform.position = nearest.transform.position;
                    if (placeSound != null)
                        audioSource.PlayOneShot(placeSound);

					socketContext sc = nearest.GetComponent<socketContext> ();
					int x = sc.x;
					int y = sc.y;
					sc.currentTile = this.transform.gameObject;
					level.MoveTile(x, y);
                }
                else
                {
                    this.transform.position = originalPosition;
                    if (badPlaceSound != null)
                        audioSource.PlayOneShot(badPlaceSound);
                }
            }
        }
    }

    void OnMouseDrag()
    {
        if (dragable)
        {
            float x = Input.mousePosition.x;
            float y = Input.mousePosition.y;

            this.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(x, y, Camera.main.transform.position.z * -1));
        }
    }

    private GameObject nearestSocket()
    {
        Transform nearest = null;

        foreach (Transform t in board.GetComponentInChildren<Transform>())
        {
            if (t == board.transform)
                continue;

            if (nearest == null)
            {
                nearest = t;
            }

            if (Vector3.Distance(this.transform.position, t.position) 
                < Vector3.Distance(this.transform.position, nearest.position))
            {
                nearest = t;
            }
        }

        return nearest.gameObject;
    }
}
