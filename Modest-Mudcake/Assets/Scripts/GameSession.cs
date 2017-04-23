using System.Collections;
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
    private bool musicMuted = false;
    public bool soundsdMuted = false;

    public GameObject Background = null;

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

    public void ChangeLevel(string level)
    {
        SceneManager.LoadScene(level); 
    }
	
	void Update () 
    {
		
	}
}
