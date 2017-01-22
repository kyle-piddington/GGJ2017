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
		print (beacon);
		player = GameObject.FindWithTag("Player");

	}
	
	// Update is called once per frame
	void Update () {
		Vector2 playerFlatVector = new Vector2 (player.transform.position.x, player.transform.position.z);
		Vector2 beaconFlatVector = new Vector2 (beacon.transform.position.x, beacon.transform.position.z);
		double beaconDistance = Vector2.Distance (playerFlatVector, beaconFlatVector);
		if (beaconDistance < BEACON_COLLECTION_DISTANCE) {
			collectBeacon();
		}
	}

	void collectBeacon() {
		GameManager.incrementCollectedBeacons();

		// Remove the beacon from the scene
		GameObject.Destroy(beacon);

		print ("Beacon collected.");

		// TODO: visual effects?

		// TODO: on-screen message?

		// TODO: sound effects?
	}
}
