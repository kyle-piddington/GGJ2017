using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeScript : MonoBehaviour
{
    AudioSource staticSFX;
    Text instructions;
    public float fadeTime = 3;

    private bool flicker = false;

    IEnumerator fadeOut()
    {
        yield return new WaitForSeconds(fadeTime);
        staticSFX.Play();
        for(int i = 0; i < 9; i++)
        {
            instructions.enabled = flicker;
            flicker = !flicker;
            yield return new WaitForSeconds(1 / Mathf.Pow(2*(i + 1), 2));
        }
        staticSFX.Stop();
        instructions.enabled = true;
        instructions.text = "You will burn here like the rest of us.";
        flicker = false;
        yield return new WaitForSeconds(fadeTime / 3f);
        staticSFX.Play();
        for (int i = 0; i < 19; i++)
        {
            instructions.enabled = flicker;
            flicker = !flicker;
            yield return new WaitForSeconds(1 / Mathf.Pow(2 * (i + 1), 2));
        }
        staticSFX.Stop();
        yield return null;
    }
    // Use this for initialization
    void Start()
    {
        staticSFX = GetComponent<AudioSource>();
        instructions = GetComponentInChildren<Text>();
        StartCoroutine(fadeOut());
    }

    // Update is called once per frame
    void Update()
    {

    }


}