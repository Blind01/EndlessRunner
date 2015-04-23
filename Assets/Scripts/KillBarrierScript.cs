using UnityEngine;
using System.Collections;

public class KillBarrierScript : MonoBehaviour {
	public Transform startPoint;
	public Transform endPoint;
	public float lineWidth = 0.2f;
	public float chaseSpeed = 1f;

	LineRenderer mistLine;


	// Use this for initialization
	void Start () {
		mistLine = GetComponent<LineRenderer> ();
		mistLine.SetWidth (lineWidth, lineWidth);


	}

	void Update(){
		mistLine.SetPosition (0, startPoint.position);
		mistLine.SetPosition (1, endPoint.position);

	
		transform.position += Vector3.right * chaseSpeed * Time.deltaTime;

	}

	public void BarrierSpeedup(float increment){
		chaseSpeed += increment;
	}
}
