using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RoomGenerator : MonoBehaviour {

	public GameObject Player;
	public GameObject killBarrier;
	public GameObject[] rooms;

	public Text distanceText;
	public Text barrierText;
	public Button restartButton;
	public Canvas buttonCanvas;
	public Image  summaryBackground;

	private GameObject previousRoom;
	private GameObject currentRoom;
	private GameObject nextRoom;
	private GameObject player;


	

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
		summaryBackground.enabled = false;
		buttonCanvas.enabled = false;
		restartButton.enabled = false;



	}
	
	// Update is called once per frame
	void Update () {
		if (player != null) {
			int pDistance = (int)Vector3.Distance (transform.position, player.transform.position);
			int bDistance = (int)Vector3.Distance (player.transform.position, killBarrier.transform.position);

			distanceText.text = "Distance: " + pDistance + "m";
			barrierText.text = "Barrier: " + bDistance + "m";
		}
	}

	public void newRoom(){
		Destroy (previousRoom);
		previousRoom = currentRoom;
		currentRoom = nextRoom;

		GameObject[] door = GameObject.FindGameObjectsWithTag("Door");

		foreach (GameObject d in door) {
			d.animation.Play ();
		}

		GameObject tempObj = GameObject.FindGameObjectWithTag("NextRoom");
		nextRoom = Instantiate (rooms [Random.Range (0, rooms.Length)], tempObj.transform.position, tempObj.transform.rotation) as GameObject;
		Destroy (tempObj);
	}

	public void endGame(){
		Destroy (player);
		Destroy (killBarrier);
		summaryBackground.enabled = true;
		buttonCanvas.enabled = true;
		restartButton.enabled = true;
	}

	public void restartGame(){
		Application.LoadLevel (Application.loadedLevel);
	}
}
