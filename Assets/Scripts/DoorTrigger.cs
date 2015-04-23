using UnityEngine;
using System.Collections;

public class DoorTrigger : MonoBehaviour {
	
	private GameObject initialObject;
	
	private RoomGenerator roomGenerator;
	bool fired;
	
	void Start(){
		initialObject = GameObject.FindGameObjectWithTag("InitialObject");
		roomGenerator = initialObject.GetComponent<RoomGenerator> ();
		fired = false;
	}

	void Update(){

	}
	
	void OnTriggerEnter(Collider hit){
		if (!fired) {
			fired = true;
			roomGenerator.newRoom ();
			Destroy (gameObject);
		}
	}
	
}
