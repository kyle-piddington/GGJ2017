using UnityEngine;
using System.Collections;

public class PlayerPulseScript : MonoBehaviour {
    public float camShakeDampFactor = .25f;
    public float coolDownTime;
	public float _pulseDist;
	public float MaxCharge;

    public float chargeRate = 10.0f;
    public float maxCharge = 100.0f;
    public float currentCharge = 0.0f;
    public float drainRate = 10.0f;
	private float _sonarCharge;
    private bool canCharge = true;
    private bool discharge = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Space) && _sonarCharge < MaxCharge && canCharge) {
			_sonarCharge += Time.deltaTime * chargeRate;
            currentCharge = Mathf.Clamp(currentCharge + chargeRate * Time.deltaTime, 0, maxCharge);
            if(currentCharge == maxCharge)
            {
                canCharge = false;
            }
		}
		else if(Input.GetKeyUp(KeyCode.Space))
		{
            releaseCharge();
		}

        if (discharge)
        {
            currentCharge = Mathf.Clamp(currentCharge - Time.deltaTime * drainRate, 0, maxCharge);
            if (currentCharge == 0)
            {
                canCharge = true;
                discharge = false;
            }
                
        }    
	}

	public float ChargeAmount()
	{
		return _pulseDist;
	}

    public void releaseCharge()
    {
        StartCoroutine(Camera.main.GetComponent<CameraManager>().Shake((currentCharge / maxCharge) * camShakeDampFactor));
        Debug.Log(_sonarCharge);
        Collider[] walls = Physics.OverlapSphere(transform.position, 30.5f);
        foreach (Collider c in walls)
        {
            Debug.Log("Hit");
            WallMaterialScript scr = c.GetComponent<WallMaterialScript>();
            if (scr != null)
            {
                scr.SetSonar(transform.position, 25);
            }
        }
        _sonarCharge = 0;
        discharge = true;
    }
}
