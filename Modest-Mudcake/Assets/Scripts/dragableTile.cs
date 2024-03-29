﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragableTile : MonoBehaviour 
{
    public GameSession gameSession = null;
    public bool dragable = true;
    public float snapDistance = 1.0f;

    public GameObject board = null;
	public LevelManager level = null;

    private AudioSource audioSource = null;
    public AudioClip placeSound = null;
    public AudioClip badPlaceSound = null;
    public AudioClip invalidSound = null;

    private Vector3 originalPosition;
    public bool held = false;

    public GameObject currentSocket = null;

    public GameObject lockedOverlay = null;

    public LevelManager.TileType tileType;

	void Start () 
    {
        audioSource = GetComponent<AudioSource>();
        gameSession = GameObject.Find("GameSession").GetComponent<GameSession>();

        if (gameSession == null)
        {
            Debug.Log("Error finding GameSession");
            Destroy(this.gameObject);
        }
	}
	
	void Update () 
    {
		
	}

    void OnMouseDown()
    {
        if (dragable && !held)
        {
            held = true;
			GetComponent<SpriteRenderer>().sortingOrder += 10000;
            originalPosition = this.transform.position;
        }
    }

    void OnMouseUp()
    {
        if (dragable && held)
        {
            held = false;
			GetComponent<SpriteRenderer>().sortingOrder -= 10000;

            GameObject nearest = nearestSocket();

            if (nearest != null)
            {
                if ((Vector3.Distance(this.transform.position, nearest.transform.position) < snapDistance) &&
                    nearest.GetComponent<socketContext>().currentTile == null)
                {
                    if (currentSocket != null)
                        currentSocket.GetComponent<socketContext>().currentTile = null;

                    this.SetPosition(nearest.transform.position);
                    
                    if (!gameSession.soundsMuted)
                    {
                        if (placeSound != null)
                        {
                            audioSource.volume = gameSession.soundVolume;
                            audioSource.PlayOneShot(placeSound);
                        }
                    }

					socketContext sc = nearest.GetComponent<socketContext> ();
					int x = sc.x;
					int y = sc.y;
					sc.currentTile = this.transform.gameObject;
					level.MoveTile(x, y);

                    currentSocket = nearest;
                }
                else
                {
                    this.transform.position = originalPosition;
                    if (!gameSession.soundsMuted)
                    {
                        if (badPlaceSound != null)
                        {
                            audioSource.volume = gameSession.soundVolume;
                            audioSource.PlayOneShot(badPlaceSound);
                        }
                    }
                }
            }
        }
        else
        {
            this.transform.position = originalPosition;
            if (!gameSession.soundsMuted)
            {
                if (invalidSound != null)
                {
                    audioSource.volume = gameSession.soundVolume;
                    audioSource.PlayOneShot(invalidSound);
                }
            }
        }
    }

    void OnMouseDrag()
    {
        if (dragable && held)
        {
            float x = Input.mousePosition.x;
            float y = Input.mousePosition.y;

            this.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(x, y, Camera.main.transform.position.z * -1));
        }
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
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

    public void setLockedOverlay(bool value)
    {
        if (lockedOverlay != null)
        {
            lockedOverlay.SetActive(value);
        }
    }

    public void SetPosition(Vector3 newPosition)
    {
        this.transform.position = newPosition;
        originalPosition = newPosition;
    }
}
