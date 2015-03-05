using UnityEngine;
using System.Collections;

public class CameraTracking : MonoBehaviour {

	private GameObject player;
	public float xOffset = 10.0f;
	public float yOffset = 7.0f;
	public float zOffset = -10.0f;
	
	void LateUpdate() {
		this.transform.position = new Vector3(player.transform.position.x + xOffset,
		                                      player.transform.position.y + yOffset,
		                                      player.transform.position.z + zOffset);
	}


	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
