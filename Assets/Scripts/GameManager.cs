using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour {
    public SFXManager sfx;
    public GameObject gameOverPanel;
    public AudioSource victoryJingle;
    public AudioSource defeatJungle;
    public static float minBeaconDistance = 20f;
	public static int NUM_BEACONS = 4;
	private static int MIN_BEACON_DISTANCE = 6;

	public static int numCollectedBeacons = 0;
    public static bool playerDead;
    public static bool victoryAchieved;

    private static bool audioBegin;

	public static void incrementCollectedBeacons() {

        // TODO: trigger sound effects, for example
        minBeaconDistance = 20f;
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
        if (numCollectedBeacons == NUM_BEACONS)
            victoryAchieved = true;

        if (playerDead && Input.GetKeyDown(KeyCode.Space))
            RestartGame();
        else if (victoryAchieved && Input.GetKeyDown(KeyCode.Space))
            RestartGame();

        if (playerDead && !audioBegin)
        {
            defeatJungle.Play();
            audioBegin = true;
        }
        else if(victoryAchieved && !audioBegin)
        {
            victoryJingle.Play();
            audioBegin = true;
        }

        if (minBeaconDistance < 2f)
            sfx.setBackGroundVol(.05f);
        else
            sfx.setBackGroundVol(1f);

        Debug.Log("Minbeacondistance: " + minBeaconDistance); 	     
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
        audioBegin = false;
        victoryAchieved = false;     
        playerDead = false;
        numCollectedBeacons = 0;
        gameOverPanel.SetActive(false);
		StopAllCoroutines();
		Destroy (mazeInstance.gameObject);
        SceneManager.LoadScene(1);
    }

	public GameObject bear;
	public GameObject lion;
	public GameObject horse;
	public GameObject dragon;

	public GameObject[] beaconModels;

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
			
			//beacon.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);

			beacons [i] = beacon;
			print (beacon);
			print (beacon.transform.parent);
		}

	}
}
