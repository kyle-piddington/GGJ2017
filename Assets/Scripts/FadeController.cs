using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour {
    public Image fader;

    private Color alpha;
	// Use this for initialization
	void Start () {
        alpha = fader.color;
    }
	
	// Update is called once per frame
	void Update () {
        if (alpha.a > 0f)
            alpha.a -= .005f;
        fader.color = alpha;
	}
}
