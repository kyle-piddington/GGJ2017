using UnityEngine;
using System.Collections;

public class PlayerPulseScript : MonoBehaviour {
    public float camShakeDampFactor = .25f;
    public float coolDownTime;
	public float _pulseDist;
	public float MaxCharge;

    public float chargeRate = 10.0f;
    public float currentCharge = 0.0f;
    public float drainRate = 10.0f;
	public float minSonar = 2;
	public float chargeSpeed;
	private float _sonarCharge;
    private bool canCharge = true;
    private bool discharge = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Space) && canCharge)
        {
            _sonarCharge = minSonar;
        }
        if (Input.GetKey(KeyCode.Space) && _sonarCharge < MaxCharge && canCharge)
        {
            _sonarCharge += Time.deltaTime * chargeRate;
            currentCharge = Mathf.Clamp(currentCharge + chargeRate * Time.deltaTime, 0, MaxCharge);
            if (currentCharge == MaxCharge)
            {
                canCharge = false;
            }
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            releaseCharge();
            canCharge = false;
        }
      
        if (discharge)
        {
            currentCharge = Mathf.Clamp(currentCharge - Time.deltaTime * drainRate, 0, MaxCharge);
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
        StartCoroutine(Camera.main.GetComponent<CameraManager>().Shake((currentCharge / MaxCharge) * camShakeDampFactor));
        Debug.Log(_sonarCharge);
        Collider[] walls = Physics.OverlapSphere(transform.position, _sonarCharge + 0.5f);
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
        }

        _sonarCharge = 0;
        discharge = true;
    }
		
}
