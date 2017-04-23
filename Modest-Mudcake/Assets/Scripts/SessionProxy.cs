using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SessionProxy : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SwitchToLevels(){
		SceneManager.LoadScene ("level-menu");
	}

	public void SetSound(bool value){
		if (value)
			StartSound();
		else
			StopSound();
	}

	public void StartSound(){
		GameSession.getInstance().soundsdMuted = false;
	}

	public void StopSound(){
		GameSession.getInstance().soundsdMuted = true;
	}

	public void SetMusic(bool value){
		if (value)
			StartMusic();
		else
			StopMusic();
	}

	public void StartMusic(){
		GameSession.getInstance().MusicStart();
	}

	public void StopMusic(){
		GameSession.getInstance().MusicStop();
	}

	public void ExitGame(){
		Application.Quit();
	}
}
