using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	private static int numCollectedBeacons = 0;

	public static void incrementCollectedBeacons() {

		// TODO: trigger sound effects, for example

		++numCollectedBeacons;
	}

	public static int getNumCollectedBeacons() {
		return numCollectedBeacons;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
