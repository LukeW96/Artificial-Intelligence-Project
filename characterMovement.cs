using UnityEngine;
using System.Collections;

public class characterMovement : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
	
	}

	void OnCollisionEnter(Collision col)
	{
		Debug.Log("COLLISION DETECTED.");

		if (col.collider.name == "relic") 
		{
			Debug.Log("Hit the endgame Object!");
		}

		if (col.collider.name == "Cylinder" || col.collider.name == "Enemy")
		{
			Debug.Log ("You were caught");
		}
	}
	

	// Update is called once per frame
	void Update () 
	{
		Transform currentPosition;
		float speed = 15.0F;
		float sprintSpeed = 20.0F;
		float rotationSpeed = 100.0F;

		if (Input.GetKey ("escape"))
		{
			Application.Quit();
		}
		//create a flare infront of the player if player left clicks. 
		if(Input.GetKeyDown(KeyCode.Mouse0))
		{
			Instantiate (GameObject.Find("flare"), transform.position+(transform.forward*2), transform.rotation);	
		}

		//the following GetKey commands are checking for input, if W or S are inputted, then the character moves forward/backwards.
		//If a is the input, it will rotate negatively on the y scale. d rotating positively on the y scale.
		if(Input.GetKey(KeyCode.W))
		{
			transform.Translate(Vector3.forward * speed * Time.deltaTime);
		}
		
		if(Input.GetKey(KeyCode.A))
		{
			transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
		}
		
		if(Input.GetKey(KeyCode.S))
		{
			transform.Translate(-Vector3.forward * speed * Time.deltaTime);
		}
		
		if(Input.GetKey(KeyCode.D))
		{
			transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
		}
	}
}