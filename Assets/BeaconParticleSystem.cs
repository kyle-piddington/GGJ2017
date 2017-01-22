using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeaconParticleSystem : MonoBehaviour {
	public ParticleSystem pSystem;
	// Use this for initialization
	void Start () {
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void burstParticleSystem()
	{
		if (!pSystem.isPlaying) {
			
			pSystem.Clear ();
			pSystem.Play ();
		}
	}
}
