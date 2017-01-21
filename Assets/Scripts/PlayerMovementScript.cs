using UnityEngine;
using System.Collections;

public class PlayerMovementScript : MonoBehaviour {
	public float PlayerSpeed = 10;
	public float PlayerTurnSpeed = 1;
	
	private Vector3 _playerTargetPosition;
	private float _playerTargetRotation;
	private float _playerAccumRotation;
	private bool _playerIsColliding;

	private bool playerIsMoving;
	private bool playerIsTurning;
	// Use this for initialization
	void Start () {
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
				if (!willCollideWithWall (transform.forward * 2.5f)) {
					
					_playerTargetPosition = transform.position + transform.forward * 2; 
					playerIsMoving = true;
				} else {
					_playerIsColliding = true;
				}
			}
			else if(Input.GetKey(KeyCode.S))
			{
				if(!willCollideWithWall(-transform.forward * 2.5f)){
					_playerTargetPosition = transform.position - transform.forward * 2; 
					playerIsMoving = true;
				} else{
						_playerIsColliding = true;
				}
			}
			else if(Input.GetKey(KeyCode.A))
			{
				_playerTargetRotation -= 90;
				playerIsTurning = true;
			}
			else if(Input.GetKey(KeyCode.D))
			{
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
	}
}
