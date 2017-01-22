using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour {
    public Image fader;
    public bool permanent;
    private bool beginFadeIn;
    private bool beginFadeOut;

    private Color alpha;
	// Use this for initialization
	void Start () {
        alpha = fader.color;
    }
	
	// Update is called once per frame
	void Update () {
        if (alpha.a > 0f && beginFadeIn)
            alpha.a -= .005f;

        if (alpha.a <= 0f && !permanent)
            gameObject.SetActive(false);
        else if (alpha.a <= 0f && permanent && !beginFadeOut)
        {
            beginFadeOut = true;
            alpha = Color.black;
            alpha.a = 0f;
        }          

        if(beginFadeOut && alpha.a < 255f)
        {
            alpha.a += .05f;
        }

        fader.color = alpha;
    }

    void OnEnable()
    {
        beginFadeIn = true;
        alpha.a = 1f;
    }
}
