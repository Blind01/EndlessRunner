using UnityEngine;
using System.Collections;

public class KillPlayerScript : MonoBehaviour {
	private RoomGenerator roomGenerator;
	private GameObject initialObject;

	void Start(){
		initialObject = GameObject.FindGameObjectWithTag ("InitialObject");
		roomGenerator = initialObject.GetComponent<RoomGenerator> ();
	}

	void Update(){

	}

	void OnTriggerEnter (Collider col){
		
		if (col.gameObject.tag == "Deadly") { 
			roomGenerator.endGame();
			//StartCoroutine(WaitForAnimation());

		}
	}

	//for animated death, which doesn't currently work
	/*
	IEnumerator WaitForAnimation()
	{
		animation.Play("PlayerDeath");
		yield return new WaitForSeconds(2f); // wait for two seconds.
		roomGenerator.endGame();
	}
	*/
	
}
