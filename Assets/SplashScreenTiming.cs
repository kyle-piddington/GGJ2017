using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplashScreenTiming : MonoBehaviour {
	public float fadeTime = 1;

	IEnumerator runSplashScreen()
	{
		yield return new WaitForSeconds (fadeTime);
		gameObject.GetComponent<Image>().CrossFadeAlpha(0, fadeTime, false);
		yield return new WaitForSeconds (fadeTime);
		SceneManager.LoadScene ("MainMenu");
		yield return null;
	}
	// Use this for initialization
	void Start () {
		gameObject.GetComponent<CanvasRenderer>().SetAlpha( 0.0f );
		gameObject.GetComponent<Image>().CrossFadeAlpha(1, fadeTime, false);
		StartCoroutine (runSplashScreen());
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}
