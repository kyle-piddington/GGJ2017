using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {

    public GameObject enemyPrefab;

    public void Spawn()
    {
        if (GameManager.numCollectedBeacons == 1)
        {
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
	
	// Update is called once per frame
	void Update () {
        Spawn();
	}
}
