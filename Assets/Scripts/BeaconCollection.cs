using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeaconCollection : MonoBehaviour {
    public AudioSource beaconAhoy;
    public AudioSource beaconPickUp;
	private const float BEACON_COLLECTION_DISTANCE = 0.5f;

	private GameObject beacon;
	private GameObject player;
    public GameObject particlePrefab;
    private GameObject particles;
    private bool _collected;

	// Use this for initialization
	void Start () {
        _collected = false;
        beaconAhoy = GetComponent<AudioSource>();
		beacon = gameObject;
		print (beacon);
		player = GameObject.FindWithTag("Player");
        particles = Instantiate(particlePrefab, transform.position, Quaternion.identity);
    }
	
	// Update is called once per frame
	void Update () {
		Vector2 playerFlatVector = new Vector2 (player.transform.position.x, player.transform.position.z);
		Vector2 beaconFlatVector = new Vector2 (beacon.transform.position.x, beacon.transform.position.z);
		float beaconDistance = Vector2.Distance (playerFlatVector, beaconFlatVector);
		if (beaconDistance < BEACON_COLLECTION_DISTANCE) {
			collectBeacon();
		}
        beaconAhoy.volume = .5f / Mathf.Clamp(beaconDistance, .5f, 20f);

        if (beaconDistance < GameManager.minBeaconDistance && !_collected)
            GameManager.minBeaconDistance = beaconDistance;
	}

	void collectBeacon() {
		GameManager.incrementCollectedBeacons();
        beaconAhoy.Stop();
        beaconPickUp.Play();
        // Remove the beacon from the scene
        _collected = true;
        gameObject.transform.position = new Vector3(0f, 999f, 0f);
		Destroy(beacon, beaconPickUp.clip.length);
        Destroy(particles);
        Destroy(gameObject, beaconPickUp.clip.length);

		print ("Beacon collected.");

		// TODO: visual effects?

		// TODO: on-screen message?

		// TODO: sound effects?
	}
}
