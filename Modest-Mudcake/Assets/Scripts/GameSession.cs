using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour 
{
    private static GameSession instance;

    public AudioClip[] music;
    private int musicIndex;
    private bool musicMuted = false;

	void Start () 
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this);
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        if (music.Length > 0)
        {
            musicIndex = 0;
            this.GetComponent<AudioSource>().clip = music[musicIndex];
            this.GetComponent<AudioSource>().Play();
        }
	}

    public void MusicChange()
    {
        if (!musicMuted)
        {
            musicIndex++;

            if (musicIndex < music.Length)
            {
                this.GetComponent<AudioSource>().clip = music[musicIndex];
                this.GetComponent<AudioSource>().Play();
            }
            else
            {
                musicMuted = true;
                musicIndex = -1;
                this.GetComponent<AudioSource>().Stop();
            }
        }
        else
            musicMuted = false;
    }
	
	void Update () 
    {
		
	}
}
