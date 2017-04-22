using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragableTile : MonoBehaviour 
{
    public bool dragable = true;
    public float snapDistance = 1.0f;

    public GameObject board = null;

    private AudioSource audioSource = null;
    public AudioClip placeSound = null;
    public AudioClip badPlaceSound = null;

    private Vector3 originalPosition;
    public bool held = false;

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
            Transform nearest = nearestSocket();

            if (nearest != null)
            {
                if (Vector3.Distance(this.transform.position, nearest.position) < snapDistance)
                {
                    this.transform.position = nearest.position;
                    if (placeSound != null)
                        audioSource.PlayOneShot(placeSound);
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

    private Transform nearestSocket()
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

        return nearest;
    }
}
