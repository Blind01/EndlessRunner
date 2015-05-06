using UnityEngine;
using System.Collections;

public class SpeedBoost : MonoBehaviour {

    float boostTime = 5.0f;

    private MeshRenderer mesh;
    private PlayerStateManager playerStateManager;

	// Use this for initialization
	void Start () {
        mesh = GetComponent<MeshRenderer>();
        playerStateManager = GameObject.Find("Player").GetComponent<PlayerStateManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            mesh.enabled = false;
            playerStateManager.SpeedBoost(boostTime);
        }
    }
}
