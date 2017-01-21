using UnityEngine;
using System.Collections;

public class PlayerPulseScript : MonoBehaviour {

	public float _pulseDist;
	public float MaxCharge;
	public float chargeSpeed;
	private float _sonarCharge;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space) && _sonarCharge < MaxCharge) {
			_sonarCharge += Time.deltaTime * chargeSpeed;
		
		}
		else if(Input.GetKeyUp(KeyCode.Space))
		{
			Debug.Log(_sonarCharge);
			Collider[] walls = Physics.OverlapSphere (transform.position, 30.5f);
			foreach (Collider c in walls) 
			{
				Debug.Log ("Hit");
				WallMaterialScript scr = c.GetComponent<WallMaterialScript> ();
				if (scr != null) {
					scr.SetSonar (transform.position, 25);
				}
			}
			_sonarCharge = 0;
		}

	}

	public float ChargeAmount()
	{
		return _pulseDist;
	}
}
