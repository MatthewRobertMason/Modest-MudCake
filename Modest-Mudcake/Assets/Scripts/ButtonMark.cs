using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMark : MonoBehaviour {

	public int levelNumber = -1;
//	public int unlockLevel = 0;
	public static int totalLevels = 10;

	public GameObject doneMark = null;
	protected const string keyFormat = "level-finished-{0}";

	// Use this for initialization
	void Start () {
//		PlayerPrefs.DeleteAll ();
		int unlockLevel = Mathf.Max(0, levelNumber - 3);

		if (levelNumber < 0)
			Debug.LogError ("A Level number hasn't been set for one of the button mark scripts");

		if (IsFinished(levelNumber)) {
			Instantiate (doneMark, gameObject.transform);
		} 

		if (unlockLevel > CountFinished ()) {
			gameObject.GetComponent<UnityEngine.UI.Button>().interactable = false;
		}
	}

	public static int CountFinished(){
		int count = 0;
		for (int ii = 1; ii <= totalLevels; ii++) {
			count += IsFinished(ii) ? 1 : 0;
		}
		return count;
	}

	public static bool IsFinished(int number){
		return PlayerPrefs.GetInt (string.Format (keyFormat, number)) > 0;
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
