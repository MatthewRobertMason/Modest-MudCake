using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMark : MonoBehaviour {

	protected int levelNumber = -1;
	public static int totalLevels = 0;

	public GameObject doneMark = null;
	protected const string keyFormat = "level-finished-{0}";

	void Awake(){
		levelNumber = transform.GetSiblingIndex() + 1;
		totalLevels = Mathf.Max(totalLevels, levelNumber + 1);
	}

	// Use this for initialization
	void Start () {
//		PlayerPrefs.DeleteAll ();
		int unlockLevel = Mathf.Max(0, levelNumber - 3);

		var button = gameObject.GetComponent<UnityEngine.UI.Button>();
		Debug.Log ("Level " + levelNumber + " " + button.onClick.GetPersistentMethodName (0) + (IsFinished(levelNumber) ? " Finished" : ""));

		if (IsFinished(levelNumber)) {
			Instantiate (doneMark, gameObject.transform);
		} 

		if (unlockLevel > CountFinished ()) {
			button.interactable = false;
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
			Debug.Log ("Finished Level " + number);
			PlayerPrefs.SetInt (string.Format (keyFormat, number), 1);
			PlayerPrefs.Save ();
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
