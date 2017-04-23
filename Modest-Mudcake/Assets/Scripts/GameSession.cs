using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour 
{
    private static GameSession instance;

	public static GameSession getInstance(){
		return instance;
	}

    public AudioClip[] music;
    private int musicIndex;
    private bool musicMuted = false;
    public bool soundsdMuted = false;

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
            AudioSource audio = this.GetComponent<AudioSource>();
            audio.loop = true;
            audio.clip = music[musicIndex];
            audio.Play();
        }
	}

	public void MusicStart(){
		while (musicMuted) {
			musicMuted = false;
			MusicChange ();
		}
	}

	public void MusicStop(){
		musicMuted = true;
		musicIndex = -1;
		this.GetComponent<AudioSource>().Stop();
	}

    public void MusicChange()
    {
        if (!musicMuted)
        {
            musicIndex++;

            if (musicIndex < music.Length)
            {
                AudioSource audio = this.GetComponent<AudioSource>();
                audio.clip = music[musicIndex];
                audio.Play();
            }
            else
            {
                AudioSource audio = this.GetComponent<AudioSource>();
                musicMuted = true;
                musicIndex = -1;
                audio.Stop();
            }
        }
        else
            musicMuted = false;
    }
	
	void Update () 
    {
		
	}
}
