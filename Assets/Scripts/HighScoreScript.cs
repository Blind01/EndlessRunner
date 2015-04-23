using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HighScoreScript : MonoBehaviour {

	public Text[] scores;


	// Use this for initialization
	void Start () {

		for (int i = 0; i < scores.Length; i++) {
			if (PlayerPrefs.HasKey ("score"+i))scores[i].text = (i+1) + ": " + PlayerPrefs.GetFloat ("score"+i);
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void clearScores(){
		for (int i = 0; i < scores.Length; i++) {
			if (PlayerPrefs.HasKey ("score" + i)) PlayerPrefs.DeleteKey("score" + i);
			scores[i].text = (i+1) + ": 000";
		}
	}

	public void back(){
		Application.LoadLevel ("TitleScreen");
	}
}
