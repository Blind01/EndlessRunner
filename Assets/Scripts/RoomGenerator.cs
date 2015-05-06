using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RoomGenerator : MonoBehaviour {

	public GameObject Player;
	public GameObject killBarrier;
	public GameObject[] rooms;

	public int barrierSpeedupDistance = 100;
	public float barrierSpeedupAmount = 1f;

	public Text distanceText;
	public Text barrierText;
	public Text roomText;
	public Canvas pauseMenu;

	private GameObject previousRoom;
	private GameObject currentRoom;
	private GameObject nextRoom;
	private GameObject player;
	private KillBarrierScript killBarrierScript;
    private PlayerStateManager playerStateManager;

	int pDistance =  0; //player distance
	int bDistance = 0; //barrier distance
	int lastPDistance =  0;
	float multiplier = 1.0f;
	int pickupCounter = 0;
	int roomCounter = 1;
	bool paused = false;
	

	// Use this for initialization
	void Awake () {

		rooms = Resources.LoadAll<GameObject>("Rooms");

		currentRoom = Instantiate (rooms [Random.Range (0, rooms.Length)]) as GameObject;


		GameObject door = GameObject.FindGameObjectWithTag("Door");
		door.animation.Play ();

		GameObject doorTrigger = GameObject.FindGameObjectWithTag("DoorTrigger");
		Destroy (doorTrigger);

		GameObject tempObj = GameObject.FindGameObjectWithTag("NextRoom");
		nextRoom = Instantiate (rooms [Random.Range (0, rooms.Length)], tempObj.transform.position, tempObj.transform.rotation) as GameObject;
		Destroy (tempObj);

		player = Instantiate (Player) as GameObject;
        player.name = "Player";
	}

	void Start(){

		killBarrierScript = killBarrier.gameObject.GetComponent<KillBarrierScript> ();
        playerStateManager = player.GetComponent<PlayerStateManager>();
		pauseMenu.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.Escape)) {
			if(paused) unpause();
			else pause ();
		}

		if (player != null && Time.timeScale != 0) {
			pDistance = (int)Vector3.Distance (transform.position, player.transform.position);
			bDistance = (int)Vector3.Distance (player.transform.position, killBarrier.transform.position);

			if(pDistance - lastPDistance >= barrierSpeedupDistance){
				lastPDistance = pDistance;
				killBarrierScript.BarrierSpeedup(barrierSpeedupAmount);
                playerStateManager.PlayerSpeedUp(barrierSpeedupAmount);
			}

			distanceText.text = "Distance: " + pDistance + "m";
			barrierText.text = "Barrier: " + bDistance + "m";
		}

		roomText.text = "Room: " + roomCounter;
	}

	public void newRoom(){
        Destroy(previousRoom);
		previousRoom = currentRoom;
		currentRoom = nextRoom;

		roomCounter++;

		GameObject[] door = GameObject.FindGameObjectsWithTag("Door");

		foreach (GameObject d in door) {
			d.animation.Play ();
		}

		GameObject tempObj = GameObject.FindGameObjectWithTag("NextRoom");
		nextRoom = Instantiate (rooms [Random.Range (0, rooms.Length)], tempObj.transform.position, tempObj.transform.rotation) as GameObject;
		Destroy (tempObj);
	}

	public void endGame(){
		PlayerPrefs.SetInt("distance", pDistance);
		PlayerPrefs.SetFloat ("multiplier", multiplier);
		PlayerPrefs.SetInt ("pickups", pickupCounter);
		PlayerPrefs.SetInt ("rooms", roomCounter);
		Application.LoadLevel ("SummaryScreen");
	
	}

	 void pause(){
		paused = true;
		Time.timeScale = 0;
		pauseMenu.enabled = true;

	}

	public void unpause(){
		paused = false;
		Time.timeScale = 1;
		pauseMenu.enabled = false;
	}

	public void restartGame(){
		unpause ();
		Application.LoadLevel("mainScene");
	}

	public void quitGame(){
		unpause ();
		Application.LoadLevel("TitleScreen");
	}

}
