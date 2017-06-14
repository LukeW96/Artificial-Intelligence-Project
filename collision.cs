using UnityEngine;
using System.Collections;

public class collision : MonoBehaviour {

	void onTriggerEnter (Collider other)
	{
		Debug.Log ("collision yo");
	}
}
