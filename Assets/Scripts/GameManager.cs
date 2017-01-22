using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static int NUM_BEACONS = 4;
	private static int MIN_BEACON_DISTANCE = 6;

	public static int numCollectedBeacons = 0;

	public static void incrementCollectedBeacons() {

		// TODO: trigger sound effects, for example

		++numCollectedBeacons;
	}

	public static int getNumCollectedBeacons() {
		return numCollectedBeacons;
	}

	// Use this for initialization
	private void Start () {
		BeginGame ();
	}
	
	// Update is called once per frame
	private void Update () {
//		if (Input.GetKeyDown (KeyCode.Space)) {
//			RestartGame ();
//		}
	}

	public Maze mazePrefab;
	
	public Maze mazeInstance;

	private void BeginGame() {
		mazeInstance = Instantiate (mazePrefab) as Maze;
		//StartCoroutine(mazeInstance.Generate());

		beaconModels = new GameObject[] {bear, lion, horse, dragon};
		placeBeacons (mazeInstance);
	}

	private void RestartGame() {
		StopAllCoroutines();
		Destroy (mazeInstance.gameObject);
		BeginGame ();
	}

	public GameObject bear;
	public GameObject lion;
	public GameObject horse;
	public GameObject dragon;

	public GameObject[] beaconModels;
	public GameObject particles;

	private void placeBeacons(Maze maze) {
		// Determine the beacon locations.
		IntVector2[] beaconLocations = new IntVector2[NUM_BEACONS];

		for (int i = 0; i < NUM_BEACONS; ++i) {
			bool successfulPlacement = false;

			// Try to place a new beacon
			while (!successfulPlacement) {
				int possibleX = UnityEngine.Random.Range (0, maze.size.x);
				int possibleZ = UnityEngine.Random.Range (0, maze.size.z);

				// Check if any existing beacon is closer than the minimum distance.
				bool otherBeaconTooClose = false;
				for (int j = 0; j < i; ++j) {
					IntVector2 existingBeaconLocation = beaconLocations [j];
					int manhattanDistanceToExistingBeacon =
						Mathf.Abs (possibleX - existingBeaconLocation.x) +
						Mathf.Abs (possibleZ - existingBeaconLocation.z);
					if (manhattanDistanceToExistingBeacon < MIN_BEACON_DISTANCE) {
						otherBeaconTooClose = true;
						break;
					}
				}

				if (!otherBeaconTooClose) {
					beaconLocations [i] = new IntVector2 ();
					beaconLocations [i].x = possibleX;
					beaconLocations [i].z = possibleZ;

					successfulPlacement = true;
				}
			}
		}


		// Create the beacon game objects.
		GameObject[] beacons = new GameObject[NUM_BEACONS];

		for (var i = 0; i < NUM_BEACONS; ++i) {
			IntVector2 beaconLocation = beaconLocations [i];
			//MazeCell beaconCell = maze.GetCell (beaconLocations [i]);
			GameObject beaconCell = GameObject.Find("Maze Cell " + beaconLocation.x + ", " + beaconLocation.z);

			Vector3 beaconPosition = new Vector3 (beaconCell.transform.position.x, -0.0f, beaconCell.transform.position.z);
			GameObject beacon = Instantiate (beaconModels[i % beaconModels.Length], beaconPosition, Quaternion.identity);
			GameObject beaconParticles = Instantiate (particles, beaconPosition, Quaternion.identity);
			//beacon.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);

			beacons [i] = beacon;
			print (beacon);
			print (beacon.transform.parent);
		}

	}
}
