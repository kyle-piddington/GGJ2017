﻿using UnityEngine;
using System.Collections;

public class PlayerMovementScript : MonoBehaviour {
    private PlayerPulseScript playerPulse;

    public float PlayerSpeed = 10;
	public float PlayerTurnSpeed = 1;
	
	private Vector3 _playerTargetPosition;
	private float _playerTargetRotation;
	private float _playerAccumRotation;
    private char prevDirection = 'W';
    private char currentDirection = 'A';
	private bool _playerIsColliding;

	private bool playerIsMoving;
	private bool playerIsTurning;
    private bool playerBump;
	// Use this for initialization
	void Start () {
        playerPulse = GetComponent<PlayerPulseScript>();

		_playerTargetPosition = transform.position;
		_playerAccumRotation = 0.0f;
		_playerTargetRotation = 0.0f;
	}

	bool willCollideWithWall(Vector3 direction)
	{
		RaycastHit rayOut;
		if(Physics.Linecast(transform.position, transform.position + direction, out rayOut))
		{
			if (rayOut.collider.gameObject.tag == "Wall") {
				return true;
			}
		}
		return false;
	}

	// Update is called once per frame
	void Update () {
		if(!playerIsMoving && !playerIsTurning)
		{
			if(Input.GetKey(KeyCode.W))
			{
                currentDirection = 'W';
				if (!willCollideWithWall (transform.forward * 0.75f)) {
					
					_playerTargetPosition = transform.position + transform.forward.normalized; 
					playerIsMoving = true;
				} else {
					_playerIsColliding = true;
				}
			}
			else if(Input.GetKey(KeyCode.S))
			{
                currentDirection = 'S';
				if(!willCollideWithWall(-transform.forward * 0.75f)){
					_playerTargetPosition = transform.position - transform.forward; 
					playerIsMoving = true;
				} else{
					_playerIsColliding = true;
				}
			}
			else if(Input.GetKey(KeyCode.A))
			{
                currentDirection = 'A';
				_playerTargetRotation -= 90;
				playerIsTurning = true;
			}
			else if(Input.GetKey(KeyCode.D))
			{
                currentDirection = 'D';
				_playerTargetRotation += 90;
				playerIsTurning = true;
			}
		}
		else
		{
			if(playerIsMoving)
			{
				if ((_playerTargetPosition - transform.position).magnitude < PlayerSpeed * Time.deltaTime) {
					transform.position = _playerTargetPosition;
					playerIsMoving = false;
				}
				else {						
					Vector3 direction = (_playerTargetPosition - transform.position).normalized;
					transform.Translate (direction * PlayerSpeed * Time.deltaTime, Space.World);
				}
			}
			if (playerIsTurning) {
				if (Mathf.Abs(_playerTargetRotation - _playerAccumRotation) < PlayerTurnSpeed * Time.deltaTime) {
					transform.eulerAngles = new Vector3 (0, _playerTargetRotation, 0);
					_playerAccumRotation = _playerTargetRotation;
					playerIsTurning = false;
				} else {
					float direction = Mathf.Sign (_playerTargetRotation - _playerAccumRotation);
					_playerAccumRotation += direction * PlayerTurnSpeed;
					transform.Rotate (new Vector3 (0, PlayerTurnSpeed * direction, 0));	
				}
			}
		}

        if (_playerIsColliding && !playerBump)
        {
            playerPulse.bumpPulse();
            _playerIsColliding = false;
            playerBump = true;
        }

        if (prevDirection != currentDirection)
            playerBump = false;

        prevDirection = currentDirection;
	}
}
