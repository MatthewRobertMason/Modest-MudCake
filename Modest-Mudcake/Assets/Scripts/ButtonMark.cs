using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMark : MonoBehaviour {

	public int levelNumber = -1;
	public GameObject doneMark = null;
	protected const string keyFormat = "level-finished-{0}";

	// Use this for initialization
	void Start () {
		if (levelNumber < 0)
			Debug.LogError ("A Level number hasn't been set for one of the button mark scripts");

		if (PlayerPrefs.GetInt (string.Format (keyFormat, levelNumber)) > 0) {
			Instantiate (doneMark, gameObject.transform);
		} 
	}

	public static void FinishedLevel(int number){
		if (number >= 0) {
			PlayerPrefs.SetInt (string.Format (keyFormat, number), 1);
			PlayerPrefs.Save ();
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
