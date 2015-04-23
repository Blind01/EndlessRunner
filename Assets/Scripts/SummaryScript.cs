using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SummaryScript : MonoBehaviour {

	public Text distanceText;
	public Text multiplierText;
	public Text pickupText;
	public Text roomText;
	public Text scoreText;
	public Text highScoreText;

	int distance =  0; 
	float multiplier = 1.0f;
	int pickupCounter = 0;
	int roomCounter = 1;
	float score = 0f;

	// Use this for initialization
	void Start () {
		if(PlayerPrefs.HasKey("distance")) distance = PlayerPrefs.GetInt("distance");
		if(PlayerPrefs.HasKey("multiplier")) multiplier = PlayerPrefs.GetFloat ("multiplier");
		if(PlayerPrefs.HasKey("pikcups")) pickupCounter = PlayerPrefs.GetInt ("pickups");
		if(PlayerPrefs.HasKey("rooms")) roomCounter = PlayerPrefs.GetInt ("rooms");

		score = (distance + pickupCounter) * multiplier;

		distanceText.text = "Distance: " + distance;
		roomText.text = "Rooms: " + roomCounter;
		pickupText.text = "Pickups: " + pickupCounter;
		multiplierText.text = "Multiplier: " + multiplier;
		scoreText.text = "Score: " + score;

		bool isHighScore = highScore (0);

		if(isHighScore) highScoreText.text = "new high score";
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	bool highScore(int x){
		float tempScore;

		for (int i = x; i <10; i++) {
			if(PlayerPrefs.HasKey("score" + i)){
				if(score > PlayerPrefs.GetFloat("score" + i)){
					tempScore = score;
					score = PlayerPrefs.GetFloat("score" + i);
					PlayerPrefs.SetFloat("score" + i, tempScore);
					bool tempBool = highScore (i+1);
					return true;
				}
			}
			else {
				PlayerPrefs.SetFloat("score" + i, score);
				return true;
			}
		}
		return false;
	}

	public void restartGame(){
		Application.LoadLevel ("mainScene");
	}

	public void quitGame(){
		Application.LoadLevel ("TitleScreen");
	}
}
