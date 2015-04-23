using UnityEngine;
using System.Collections;

public class TitleScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	

	public void startGame(){
		Application.LoadLevel ("mainScene");
	}

	public void highScores(){
		Application.LoadLevel ("HighScores");
	}

	public void quitGame(){
		Application.Quit();
	}
}
