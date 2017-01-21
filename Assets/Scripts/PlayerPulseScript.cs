using UnityEngine;
using System.Collections;

public class PlayerPulseScript : MonoBehaviour {

	public float _pulseDist;
	public float MaxCharge;
	public float minSonar = 20;
	public float chargeSpeed;
	private float _sonarCharge;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Space) && _sonarCharge < MaxCharge) {
			_sonarCharge += Time.deltaTime * chargeSpeed;
		
		}
		else if(Input.GetKeyUp(KeyCode.Space))
		{
			Debug.Log(_sonarCharge);
			Collider[] walls = Physics.OverlapSphere (transform.position, _sonarCharge + 5.0f);
			foreach (Collider c in walls) 
			{
				Debug.Log ("Hit");
				WallMaterialScript scr = c.GetComponent<WallMaterialScript> ();
				if (scr != null) {
					scr.SetSonar (transform.position, _sonarCharge);
				}
			}
			_sonarCharge = minSonar;
		}

	}

	public float ChargeAmount()
	{
		return _pulseDist;
	}
}
