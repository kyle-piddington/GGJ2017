﻿using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	// Use this for initialization
	private void Start () {
		BeginGame ();
	}
	
	// Update is called once per frame
	private void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			RestartGame ();
		}
	}

	public Maze mazePrefab;
	
	public Maze mazeInstance;

	private void BeginGame() {
		mazeInstance = Instantiate (mazePrefab) as Maze;
		StartCoroutine(mazeInstance.Generate());
	}

	private void RestartGame() {
		StopAllCoroutines();
		Destroy (mazeInstance.gameObject);
		BeginGame ();
	}
}
