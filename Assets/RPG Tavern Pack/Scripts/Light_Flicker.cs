using UnityEngine;
using System.Collections;

public class Light_Flicker : MonoBehaviour {

	public Light thisLight;

	public float maxIntensitity;
	public float minIntenstity;

	void Update ()
	{
		int roll = Random.Range(0,100);
		if (roll > 75)
		{
			thisLight.intensity = Random.Range(minIntenstity,maxIntensitity);
		}
	}

}
