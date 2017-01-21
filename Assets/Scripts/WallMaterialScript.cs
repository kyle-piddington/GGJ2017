using UnityEngine;
using System.Collections;

public class WallMaterialScript : MonoBehaviour {

	public const float SonarSpeed = 5;
	private GameObject _player;
	private PlayerPulseScript _playerPulseScript;
	private Renderer _renderer;
	private float _highlightTime;
	public float HighlightTime = 2.5f;
	private Vector3 _sonarPosition;
	private float _maxDist = 0;
	private float sonarTime = 0;
	// Use this for initialization
	void Start () {
		_renderer = GetComponent<Renderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		sonarTime += Time.deltaTime * SonarSpeed;

		_renderer.material.SetVector("_PlayerPosition", _sonarPosition);
		_renderer.material.SetFloat ("_VisibleDistance", sonarTime);
		_renderer.material.SetFloat ("_MaxDistance", _maxDist);


	}

	public void SetSonar(Vector3 location, float maxDist){
		_sonarPosition = location;
		_maxDist = maxDist;
		sonarTime = 0;
	}


		
}
