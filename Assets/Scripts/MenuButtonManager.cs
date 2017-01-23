using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuButtonManager : EventTrigger {
    public AudioSource buttonHover;
    public AudioSource buttonClick;
    public Text buttonText;
    Button mButton;
    //Renderer buttonColor;

    //private Color initColor;

	// Use this for initialization
	void Start () {
        mButton = GetComponent<Button>();
        mButton.onClick.AddListener(manageClick);
        //buttonColor = GetComponent<Renderer>();
        //initColor = buttonColor.material.color;
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    public override void OnPointerEnter(PointerEventData data)
    {
        buttonHover.Play();
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        buttonClick.Play();
        if (gameObject.tag == "Start")
        {
            buttonText.text = "Hell";
            SceneManager.LoadScene("MazeScene2");
        }
            
        else if (gameObject.tag == "Quit")
            Application.Quit();
    }

    void mouseHover()
    {
        buttonHover.Play();
        //buttonColor.material.color = Color.white;
    }

    void OnMouseExit()
    {
        //buttonColor.material.color = initColor;
    }

    void manageClick()
    {
        
    }
}
