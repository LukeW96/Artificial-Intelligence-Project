using UnityEngine;
using System.Collections;

public class cameraScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		float speed = 1.8f;

		//sets the camera to the players position
		GameObject player = GameObject.Find ("playerBall");
		Transform playerPosition = player.transform;
		transform.position = playerPosition.transform.position;
		transform.LookAt (playerPosition);

		//rotates the camera left/right depending on the key pressed.
		if(Input.GetKey (KeyCode.A))
		{
			transform.RotateAround(Vector3.up, -speed *Time.deltaTime);
		}

		if(Input.GetKey (KeyCode.D))
		{
			transform.RotateAround(Vector3.up, speed *Time.deltaTime);
		}
	}
}
