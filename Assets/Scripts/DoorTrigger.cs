using UnityEngine;
using System.Collections;

public class DoorTrigger : MonoBehaviour {
	
	private GameObject initialObject;
	
	private RoomGenerator roomGenerator;
	
	void Start(){
		initialObject = GameObject.FindGameObjectWithTag("InitialObject");
		roomGenerator = initialObject.GetComponent<RoomGenerator> ();
	}

	void Update(){

	}
	
	void OnTriggerEnter(Collider hit){
		roomGenerator.newRoom ();
		Destroy (gameObject);
	}
	
}
