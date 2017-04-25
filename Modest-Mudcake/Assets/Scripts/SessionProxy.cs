using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SessionProxy : MonoBehaviour {
    private GameSession gameSession = null;

    //public GameObject musicSlider = null;
    //public GameObject soundSlider = null;

    public GameObject musicControl = null;
    public GameObject soundControl = null;

    public GameObject levelScroll = null;

	// Use this for initialization
	void Start () {
        gameSession = GameObject.Find("GameSession").GetComponent<GameSession>();

        if (musicControl != null)
        {
            musicControl.GetComponentInChildren<Toggle>().isOn = !gameSession.musicMuted;
            musicControl.GetComponentInChildren<Slider>().value = gameSession.musicVolume;
            
        }

        if (soundControl != null)
        {
            soundControl.GetComponentInChildren<Toggle>().isOn = !gameSession.soundsMuted;
            soundControl.GetComponentInChildren<Slider>().value = gameSession.soundVolume;
        }

        if (levelScroll != null)
        {
            levelScroll.GetComponent<Scrollbar>().value = gameSession.levelScrollPosition;
            // Reset the scroll
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SwitchToLevels(){
		//SceneManager.LoadScene ("level-menu");
        gameSession.ChangeLevel("level-menu");
	}

	public void SwitchToLanding(){
		//SceneManager.LoadScene ("main-menu");
        gameSession.ChangeLevel("main-menu");
	}

	public void SetSound(bool value){
		if (value)
			StartSound();
		else
			StopSound();
	}

	public void StartSound(){
        gameSession.soundsMuted = false;
	}

	public void StopSound(){
        gameSession.soundsMuted = true;
	}

	public void SetMusic(bool value){
		if (value)
			StartMusic();
		else
			StopMusic();
	}

	public void StartMusic(){
        gameSession.MusicStart();
	}

	public void StopMusic(){
        gameSession.MusicStop();
	}

    public void changeMusicVolume(float value)
    {
        gameSession.OnMusicVolumeChange(value);
    }

    public void changeSoundVolume(float value)
    {
        gameSession.OnSoundVolumeChange(value);
    }

    public void changeScrollPosition(float value)
    {
        gameSession.OnLevelScrollChanged(value);
    }

	public void ExitGame(){
		Application.Quit();
	}
}
