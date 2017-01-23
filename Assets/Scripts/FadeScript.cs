using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeScript : MonoBehaviour
{
    Text instructions;
    public float fadeTime = 3;

    private bool flicker = false;

    IEnumerator fadeOut()
    {
        yield return new WaitForSeconds(fadeTime);
        for(int i = 0; i < 9; i++)
        {
            instructions.enabled = flicker;
            flicker = !flicker;
            yield return new WaitForSeconds(1 / Mathf.Pow(2*(i + 1), 2));
        }
        
        yield return null;
    }
    // Use this for initialization
    void Start()
    {
        instructions = GetComponentInChildren<Text>();
        StartCoroutine(fadeOut());
    }

    // Update is called once per frame
    void Update()
    {

    }


}