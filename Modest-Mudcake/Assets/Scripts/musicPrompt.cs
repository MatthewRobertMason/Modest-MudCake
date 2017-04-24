using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicPrompt : MonoBehaviour {
    private GameSession gameSession = null;
    public int musicTrack = 0;

	// Use this for initialization
	void Start () {
        gameSession = GameObject.Find("GameSession").GetComponent<GameSession>();

        if (!gameSession.musicMuted)
        {
            gameSession.MusicChange(musicTrack);
        }
        Destroy(this.gameObject);
	}
}
