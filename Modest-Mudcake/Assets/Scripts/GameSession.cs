﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour 
{
    private static GameSession instance;
    private static GameObject background = null;

	public static GameSession getInstance(){
		return instance;
	}

    public AudioClip[] music;
    private int musicIndex;
    public bool musicMuted = false;
    public bool soundsMuted = false;

    public GameObject Background = null;

    public float soundVolume;
    public float musicVolume;

    public int musicTrack = 0;

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
            return;
        }

        if (music.Length > 0)
        {
            musicIndex = 0;
            AudioSource audio = this.GetComponent<AudioSource>();
            audio.loop = true;
            audio.clip = music[musicIndex];
            audio.Play();
        }

        if (background == null)
        {
            background = Instantiate(Background);
            DontDestroyOnLoad(background);
        }
	}

	public void MusicStart(){
		MusicChange (musicTrack);
	}

	public void MusicStop(){
        MusicChange(-1);
	}

    public void MusicChange(int track)
    {
        if ((track == musicTrack) && (!musicMuted))
            return;

        AudioSource audio = this.GetComponent<AudioSource>();

        if ((track >= 0) && (track < music.Length))
        {
            musicMuted = false;
        }
        else
        {
            musicMuted = true;
        }

        if (!musicMuted)
        {
            if ((track >= 0) && (track < music.Length))
            {
                musicTrack = track;
                musicMuted = false;
                audio.clip = music[track];
                audio.Play();
            }
            else
            {
                musicMuted = true;
                audio.Stop();
            }
        }
        else
            audio.Stop();
    }

	public void FinishedLevel(int number){
		ButtonMark.FinishedLevel (number);
	}

    public void ChangeLevel(string level)
    {
        SceneManager.LoadScene(level); 
    }

    public void OnSoundVolumeChange(float value)
    {
        soundVolume = value;
    }

    public void OnMusicVolumeChange(float value)
    {
        AudioSource audio = this.GetComponent<AudioSource>();
        musicVolume = value;
        audio.volume = value;
    }

	void Update () 
    {
		
	}
}
