using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	
	public int strength = 10;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate() {
		rigidbody.AddForce(
			Input.GetAxis("Horizontal") * strength,
			Input.GetAxis("Vertical") * strength,
			0);
	}
}
