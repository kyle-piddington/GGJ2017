using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public PlayerPulseScript pulseCharge;
    Slider chargeBar;
	// Use this for initialization
	void Start () {
        chargeBar = GetComponentInChildren<Slider>();
        chargeBar.maxValue = pulseCharge.maxCharge;
	}
	
	// Update is called once per frame
	void Update () {
        chargeBar.value = pulseCharge.currentCharge;
	}
}
