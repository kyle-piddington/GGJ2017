using UnityEngine;
using System.Collections;

public class PlayerPulseScript : MonoBehaviour {

    public float camShakeDampFactor = .25f;
	public float _pulseDist;
	public float MaxCharge;
    public float rangeRatio = 1.0f;

    // Audio Cues
    public SFXManager sfx;
    public bool fullyCharged;
    public bool charging;
    public bool discharging;

    public float chargeRate = 10.0f;
    public float currentCharge = 0.0f;
    public float drainRate = 10.0f;
	public float minSonar = 2;
    public float maxSonar = 5;
	public float chargeSpeed;
    private bool canCharge = true;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (GameManager.playerDead)
            return;

        if (Input.GetKey(KeyCode.Space) && canCharge)
        {
            charging = true;
            discharging = false;
            currentCharge = Mathf.Clamp(currentCharge + chargeRate * Time.deltaTime, 0, MaxCharge);

            if (currentCharge == MaxCharge)
            {
                charging = false;
                fullyCharged = true;
                Debug.Log("Once");
                canCharge = false;
            }
        }
        else if (Input.GetKeyUp(KeyCode.Space) && !discharging)
        {
            releaseCharge();
            canCharge = false;
        }
      
        if (discharging)
        {
            currentCharge = Mathf.Clamp(currentCharge - Time.deltaTime * drainRate, 0, MaxCharge);
            if (currentCharge == 0)
            {
                canCharge = true;
                discharging = false;
                fullyCharged = false;
                sfx.resetAudioMarkers();
            }              
        }    
	}

	public float ChargeAmount()
	{
		return _pulseDist;
	}

    public void releaseCharge()
    {

        StartCoroutine(Camera.main.GetComponent<CameraManager>().Shake((currentCharge / MaxCharge) * camShakeDampFactor));
        float chargeRatio = currentCharge / MaxCharge;
        float _sonarCharge = chargeRatio * maxSonar + minSonar;

        charging = false;
        discharging = true;
        StartCoroutine(Camera.main.GetComponent<CameraManager>().Shake(chargeRatio * camShakeDampFactor));

        Collider[] walls = Physics.OverlapSphere(transform.position, (_sonarCharge));
        foreach (Collider c in walls)
        {
            WallMaterialScript scr = c.GetComponent<WallMaterialScript>();
            if (scr != null)
            { 
               scr.SetSonar(transform.position, _sonarCharge);
            }
			EnemyAIScript eAI = c.GetComponent<EnemyAIScript> ();
			if (eAI != null) {
				eAI.playerPinged (transform.position);
			}
			BeaconParticleSystem bSys = c.GetComponent<BeaconParticleSystem> ();
			if (bSys != null) {
				bSys.burstParticleSystem ();
			}
        }

        _sonarCharge = 0;
        sfx.resetAudioMarkers();
    }

    public void bumpPulse()
    {
        sfx.unchargedPingAudio.Play();
        StartCoroutine(Camera.main.GetComponent<CameraManager>().Shake(camShakeDampFactor));
        Collider[] walls = Physics.OverlapSphere(transform.position, minSonar);
        foreach (Collider c in walls)
        {
            WallMaterialScript scr = c.GetComponent<WallMaterialScript>();
            if (scr != null)
            {
                scr.SetSonar(transform.position, minSonar);
            }
        }
    }
}
