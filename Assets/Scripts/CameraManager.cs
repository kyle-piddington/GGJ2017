using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    public float elapsed = 0.0f;
    public float duration = 3.0f;
    public float shakeAmount = 1.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public IEnumerator Shake(float shakeAmount)
    {
        Vector3 originalCamPos = Camera.main.transform.position;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float x = 1, y = 1;

            x *= Random.value * shakeAmount;
            y *= Random.value * shakeAmount;

            Camera.main.transform.position = new Vector3(x, y, gameObject.transform.parent.position.z);

            yield return null;
        }
        elapsed = 0.0f;
        Camera.main.transform.position = new Vector3(originalCamPos.x, originalCamPos.y, gameObject.transform.parent.position.z);
    }

}
