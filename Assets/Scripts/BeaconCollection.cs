using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeaconCollection : MonoBehaviour {

	private const double BEACON_COLLECTION_DISTANCE = 0.5;

	private GameObject beacon;
	private GameObject player;

	// Use this for initialization
	void Start () {

		beacon = gameObject;
		player = GameObject.FindWithTag("Player");

	}
	
	// Update is called once per frame
	void Update () {
		double beaconDistance = Vector3.Distance (player.transform.position, gameObject.transform.position);
		if (beaconDistance < BEACON_COLLECTION_DISTANCE) {
			collectBeacon();
		}
	}

	void collectBeacon() {
		GameManager.incrementCollectedBeacons();

		// Remove the beacon from the scene
		GameObject.Destroy(beacon);

		// TODO: visual effects?

		// TODO: on-screen message?

		// TODO: sound effects?
	}
}
