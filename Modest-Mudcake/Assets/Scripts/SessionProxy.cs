using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SessionProxy : MonoBehaviour {
    public GameSession gameSession = null;

    public GameObject musicSlider = null;
    public GameObject soundSlider = null;

	// Use this for initialization
	void Start () {
        gameSession = GameObject.Find("GameSession").GetComponent<GameSession>();

        if (musicSlider != null)
        {
            musicSlider.GetComponent<Slider>().value = gameSession.musicVolume;
        }

        if (soundSlider != null)
        {
            soundSlider.GetComponent<Slider>().value = gameSession.soundVolume;
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
        gameSession.soundsdMuted = false;
	}

	public void StopSound(){
        gameSession.soundsdMuted = true;
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

	public void ExitGame(){
		Application.Quit();
	}
}
