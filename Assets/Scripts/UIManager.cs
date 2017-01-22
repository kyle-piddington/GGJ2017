using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public GameObject panel;
    public PlayerPulseScript pulseCharge;
    Text fragCount;
    Slider chargeBar;

	// Use this for initialization
	void Start () {
        fragCount = GetComponentInChildren<Text>();
        chargeBar = GetComponentInChildren<Slider>();
        chargeBar.maxValue = pulseCharge.MaxCharge;
	}
	
	// Update is called once per frame
	void Update () {
        chargeBar.value = pulseCharge.currentCharge;
        fragCount.text = GameManager.numCollectedBeacons + "/" + GameManager.NUM_BEACONS;

        if (GameManager.playerDead && !panel.activeSelf)
        {
            panel.SetActive(true);
        }
	}
}
