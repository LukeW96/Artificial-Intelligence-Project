using UnityEngine;
using System.Collections;

public class flareScript : MonoBehaviour {

	
	float timer = 0;

	void Start ()
	{}

	//keeps the flare alive for 3s, then destroys the object
	void Update () 
	{
		timer = timer + Time.deltaTime;
		//checks that the object name is "flare(Clone), this ensures the original is not deleted and won't 
		//get nullreferenceexceptions before attempted duplicating a then non existent object.
		if(gameObject.name == ("flare(Clone)"))
		{
			if (timer >= 3) 
			{
				Destroy(this.gameObject);
				Debug.Log ("destroyed!");
			}
		}
	}
}
