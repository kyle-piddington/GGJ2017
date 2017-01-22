using UnityEngine;
using System.Collections;

public class PlayerPulseScript : MonoBehaviour {

    public float camShakeDampFactor = .25f;
	public float _pulseDist;
	public float MaxCharge;

    // Audio Cues
    public SFXManager sfx;
    public bool fullyCharged;
    public bool charging;
    public bool discharging;

    public float chargeRate = 10.0f;
    public float currentCharge = 0.0f;
    public float drainRate = 10.0f;
	public float minSonar = 2;
	public float chargeSpeed;
	private float _sonarCharge;
    private bool canCharge = true;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.Space) && canCharge)
        {
            charging = true;
            discharging = false;

            _sonarCharge += (Time.deltaTime * chargeRate) + minSonar;
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
        charging = false;
        discharging = true;
        StartCoroutine(Camera.main.GetComponent<CameraManager>().Shake((currentCharge / MaxCharge) * camShakeDampFactor));

        Debug.Log(_sonarCharge);
        Collider[] walls = Physics.OverlapSphere(transform.position, _sonarCharge);
        foreach (Collider c in walls)
        {
            WallMaterialScript scr = c.GetComponent<WallMaterialScript>();
            if (scr != null)
            { 
               scr.SetSonar(transform.position, _sonarCharge);
            }
        }

        _sonarCharge = 0;
        sfx.resetAudioMarkers();
    }
}
