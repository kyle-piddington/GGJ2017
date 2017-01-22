using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAudio : MonoBehaviour {
    public Transform player;
    AudioSource audioTest;

    public float playerDist;

	// Use this for initialization
	void Start () {
        audioTest = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        playerDist = Vector3.Distance(player.position, gameObject.transform.position);
        playerDist = Mathf.Clamp(playerDist, .5f, 20);

        audioTest.volume = .5f / playerDist;
	}
}
