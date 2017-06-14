using UnityEngine;
using System.Collections;

public class collisionDetection : MonoBehaviour {

	void onCollisionEnter(Collision col)
	{
		Debug.Log("COLLISION DETECTED.");
	}
}
