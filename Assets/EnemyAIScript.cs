using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAIScript : MonoBehaviour {
	NavMeshAgent agent;
	public float walkRadius;
	public static float EnemySFXDist = 2;
	private GameObject player;

	Vector3 target;

	bool chasingPlayer = false;
	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
		target = getRandomPosition ();
		agent.SetDestination (target);
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		updateShaders (chasingPlayer);
		if (atTarget ()) {
			if (chasingPlayer) {
				Vector3 newTarg;
				if (canFindPlayer (out newTarg)) {
					Debug.Log ("Found player");
					target = newTarg;
				} else {
					Debug.Log ("Lost player");
					target = getRandomPosition ();
					chasingPlayer = false;
	
				}
			} else {
				target = getRandomPosition ();
			}
			agent.SetDestination (target);
		}

		
	}
	bool atTarget(){
		Vector3 transformXZ = new Vector3 (transform.position.x, 0, transform.position.z);
		Vector3 targXZ = new Vector3 (target.x, 0, target.z);


		if (!agent.pathPending && !agent.hasPath){
			Debug.Log("At destination");
			return true;
		}
		return false;
	}

	Vector3 getRandomPosition()
	{
		Vector3 randomDirection = Random.insideUnitSphere * walkRadius;
		randomDirection += transform.position;
		NavMeshHit hit;
		NavMesh.SamplePosition(randomDirection, out hit, walkRadius, 1);
		Vector3 finalPosition = hit.position;
		return finalPosition;
	}

	bool canFindPlayer(out Vector3 playerPos)
	{
		RaycastHit hit;
		playerPos = Vector3.zero;

		if (Physics.Raycast (new Ray (transform.position, player.gameObject.transform.position - transform.position), out hit)) {
			if (hit.collider.gameObject.tag == "Player") {
				playerPos = player.gameObject.transform.position;
				return true;
			} else {
				return false;
			}
		}
		Debug.Log ("Could not find player");

		return false;

	}

	public void playerPinged(Vector3 playerPos)
	{
			
			target = playerPos;
			agent.SetDestination (playerPos);
			chasingPlayer = true;
		updateShaders (true);
		Debug.Log ("After the player!");
	}

	void updateShaders(bool chasingPlayer)
	{
		Collider[] walls = Physics.OverlapSphere(transform.position, EnemySFXDist + 5.0f);
		foreach (Collider c in walls) {
			WallMaterialScript scr = c.GetComponent<WallMaterialScript> ();
			if (scr != null) { 
				scr.setEnemy (transform.position, chasingPlayer);
			}
		}
	}


}
