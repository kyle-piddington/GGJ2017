using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour {

    public PlayerPulseScript playerPulse;
    public AudioSource drone;
    public AudioSource chargingAudio;
    public AudioSource fullyChargedAudio;
    public AudioSource unchargedPingAudio;
    public AudioSource chargedPingAudio;

    private bool charging;
    private bool fullyCharged;
    private bool unchargedPing;
    private bool chargedPing;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(playerPulse.charging && !charging)
        {
            chargingAudio.Play();
            charging = true;
        }
        else if(playerPulse.fullyCharged && !fullyCharged)
        {
            chargingAudio.Stop();
            fullyChargedAudio.Play();
            fullyCharged = true;
        }

        if(playerPulse.fullyCharged && playerPulse.discharging && !chargedPing)
        {
            fullyChargedAudio.Stop();
            chargedPingAudio.Play();
            chargedPing = true;
        }
        else if(playerPulse.discharging && !unchargedPing)
        {
            chargingAudio.Stop();
            unchargedPingAudio.Play();
            unchargedPing = true;
        }
	}

    public void resetAudioMarkers()
    {
        charging = false;
        fullyCharged = false;
        unchargedPing = false;
        chargedPing = false;
    }

    public void setBackGroundVol(float vol)
    {
        if(vol < drone.volume)
        {
            while(drone.volume > vol)
                drone.volume -= 1 * Time.deltaTime;
        }
        else
        {
            while (drone.volume < vol)
                drone.volume += 1 * Time.deltaTime;
        }
    }
}
