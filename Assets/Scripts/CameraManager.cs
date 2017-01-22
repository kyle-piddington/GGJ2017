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
        Vector3 originalCamPos = transform.localPosition;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float x = 1, y = 1;

            x *= Random.value * shakeAmount;
            y *= Random.value * shakeAmount;

            transform.localPosition = new Vector3(x, y, 0);

            yield return null;
        }
        elapsed = 0.0f;
        transform.localPosition = new Vector3(originalCamPos.x, originalCamPos.y, 0);
        yield return null;
    }

    public IEnumerator Oscillate(float shakeAmount)
    {
        Vector3 originalCamPos = transform.localPosition;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float z = 1;

            z *= Random.value * shakeAmount;

            transform.localPosition = new Vector3(originalCamPos.x, originalCamPos.y, z);

            yield return null;
        }
        elapsed = 0.0f;
        transform.localPosition = new Vector3(originalCamPos.x, originalCamPos.y, 0);
        yield return null;
    }

}
