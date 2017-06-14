using UnityEngine;
using System.Collections;

public class preyLeaderScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}


	float timer = 0;
	float speed;

	//randomly wanders the map, rotating every 5 seconds.
	void Update ()
	{
		speed = 10 * Time.deltaTime;
		timer = timer + Time.deltaTime;
		transform.Translate(Vector3.forward *speed);
		if (timer >= 5)
		{
			int direction = Random.Range (-90, 90);
			transform.Rotate (0, direction, 0);
			timer = 0;
		}
	}
}
