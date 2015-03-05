using UnityEngine;
using System.Collections;

public class RoomGenerator : MonoBehaviour {

	public GameObject Player;
	public GameObject[] rooms;

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


	}
	
	// Update is called once per frame
	void Update () {
	
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
}
