using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

    public Transform followObject;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 position = transform.position;
        position.x = followObject.position.x;
        transform.position = position;
	}
}
