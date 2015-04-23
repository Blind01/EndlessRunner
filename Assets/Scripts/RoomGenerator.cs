using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RoomGenerator : MonoBehaviour {

	public GameObject Player;
	public GameObject killBarrier;
	public GameObject[] rooms;

<<<<<<< HEAD
	public Text distanceText;
	public Text barrierText;
	public Button restartButton;
	public Canvas buttonCanvas;
	public Image  summaryBackground;
=======
	public int barrierSpeedupDistance = 100;
	public float barrierSpeedupAmount = 1f;

	public Text distanceText;
	public Text barrierText;
	public Text roomText;
>>>>>>> origin/master

	private GameObject previousRoom;
	private GameObject currentRoom;
	private GameObject nextRoom;
	private GameObject player;
<<<<<<< HEAD


=======
	private KillBarrierScript killBarrierScript;

	int pDistance =  0; //player distance
	int bDistance = 0; //barrier distance
	int lastPDistance =  0;
	float multiplier = 1.0f;
	int pickupCounter = 0;
	int roomCounter = 1;
>>>>>>> origin/master
	

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

	}

	void Start(){
<<<<<<< HEAD
		summaryBackground.enabled = false;
		buttonCanvas.enabled = false;
		restartButton.enabled = false;


=======

		killBarrierScript = killBarrier.gameObject.GetComponent<KillBarrierScript> ();
>>>>>>> origin/master

	}
	
	// Update is called once per frame
	void Update () {
		if (player != null) {
<<<<<<< HEAD
			int pDistance = (int)Vector3.Distance (transform.position, player.transform.position);
			int bDistance = (int)Vector3.Distance (player.transform.position, killBarrier.transform.position);
=======
			pDistance = (int)Vector3.Distance (transform.position, player.transform.position);
			bDistance = (int)Vector3.Distance (player.transform.position, killBarrier.transform.position);

			if(pDistance - lastPDistance >= barrierSpeedupDistance){
				lastPDistance = pDistance;
				killBarrierScript.BarrierSpeedup(barrierSpeedupAmount);
			}
>>>>>>> origin/master

			distanceText.text = "Distance: " + pDistance + "m";
			barrierText.text = "Barrier: " + bDistance + "m";
		}
<<<<<<< HEAD
=======

		roomText.text = "Room: " + roomCounter;
>>>>>>> origin/master
	}

	public void newRoom(){
		Destroy (previousRoom);
		previousRoom = currentRoom;
		currentRoom = nextRoom;

<<<<<<< HEAD
=======
		roomCounter++;

>>>>>>> origin/master
		GameObject[] door = GameObject.FindGameObjectsWithTag("Door");

		foreach (GameObject d in door) {
			d.animation.Play ();
		}

		GameObject tempObj = GameObject.FindGameObjectWithTag("NextRoom");
		nextRoom = Instantiate (rooms [Random.Range (0, rooms.Length)], tempObj.transform.position, tempObj.transform.rotation) as GameObject;
		Destroy (tempObj);
	}

	public void endGame(){
<<<<<<< HEAD
		Destroy (player);
		Destroy (killBarrier);
		summaryBackground.enabled = true;
		buttonCanvas.enabled = true;
		restartButton.enabled = true;
	}

	public void restartGame(){
		Application.LoadLevel (Application.loadedLevel);
=======
		PlayerPrefs.SetInt("distance", pDistance);
		PlayerPrefs.SetFloat ("multiplier", multiplier);
		PlayerPrefs.SetInt ("pickups", pickupCounter);
		PlayerPrefs.SetInt ("rooms", roomCounter);
		Application.LoadLevel ("SummaryScreen");
	
>>>>>>> origin/master
	}
}
