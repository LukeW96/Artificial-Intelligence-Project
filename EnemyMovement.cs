using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
	void OnCollisionEnter(Collision collision)
	{
		//Debug.Log("COLLISION DETECTED.");
	}
	
	public Transform alliedPosition;
	public float speed = 50.0F;
	string state;
	
	public Transform target;
	public float rayDistance;
	int turnCounter = 0;
	

	void Start () 
	{
		//this section basically finds the other enemy object.
		string otherName;
		GameObject newTarget = GameObject.FindGameObjectWithTag("Player");
		target = newTarget.transform; 
		
		if (gameObject.name == ("Cylinder")) {
			
			otherName = "Enemy";
			
		} else 
		{
			otherName = "Cylinder";
		}
		
		//finds the position of the other enemy object.
		GameObject ally = GameObject.Find(otherName);
		alliedPosition = ally.transform;

	}


	bool canSeePlayer = false;
	void Update () 
	{
		//sets the state to nothing each frame, this is so it can't get stuck on fleeing/attracting methods.
		state = "";
		checkRaycast ();

		//this commented out section is another way to interpret the finite state machine
		/**if (checkFlare () == true) 
		{
			flee ();
		}
		else if (canSeePlayer == true)
		{
			flock ();
			attract ();
		}
		else
			wander ();
*/
		switch (state) 
		{
			case "canSeeFlare":
				flee ();
				break;
			case "canSeePlayer":
				flock();
				attract();
				break;

			case "": 
				wander ();
				break;
		}
	}

	//checks infront of the object to see if it hits the wall or player object.
	private void checkRaycast()
	{
		GameObject newTarget = GameObject.FindGameObjectWithTag("Player");
		RaycastHit hit;

		Vector3 forward = transform.TransformDirection (Vector3.forward * 10);
		if (Physics.Raycast (transform.position, forward, out hit,  20.0f))
		{
			if(hit.collider.gameObject.tag == "wall")
			{
				if(turnCounter < 3)
				{
					transform.Rotate (0,-90,0);
					turnCounter++;
				}
				
				else if(turnCounter >= 3)
				{
					transform.Rotate (0,90,0);
					turnCounter++;
					
					if(turnCounter == 5)
					{
						turnCounter = 0;
					}
				}
			}

			if (hit.collider.gameObject.tag == "Player")
			{
				state = "canSeePlayer";
			}
		}
	}

	//checks if the flare is within a certain radius of the enemy, so it can flee if it's in a certain area.
	private void checkFlare()
	{
		
		//storing the enemy's position in a variable
		Vector3 objectsPosition = new Vector3 (transform.position.x, transform.position.y, transform.position.z);

		//checking if the object Flare(Clone) exists, so exception errors don't occur if they haven't used a flare
			if (GameObject.Find ("flare(Clone)") != null) 
			{
			
				GameObject flareObject = GameObject.Find ("flare(Clone)");
				Transform flarePosition = flareObject.transform;
				Debug.Log (flarePosition);
				if (flarePosition.position.x > (objectsPosition.x - 20) && flarePosition.position.x < (objectsPosition.x + 20))
				{
					state = "canSeeFlare";
				}


				if (flarePosition.position.z > (objectsPosition.z - 20) && flarePosition.position.z < (objectsPosition.z + 20))
				{
					state = "canSeeFlare";
				}
		}

	}


	//moves the enemy objects closer together.
	private void flock()
	{
		Vector3 objectPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		
		//The outer if is checking if the other enemy AI is within a certain radius on the x axis
		if (alliedPosition.position.x > (objectPosition.x - 50) && alliedPosition.position.x < (objectPosition.x + 50))
		{
			//if it is within a certain radius, and it is higher on the x axis, it will move to the right
			if(alliedPosition.position.x > (objectPosition.x))
			{
				transform.Translate(Vector3.left * speed *Time.deltaTime);
			}
			
			//else if it's below the other object on the x axis, it will move to the left
			if(alliedPosition.position.x < (objectPosition.x))
			{
				transform.Translate(Vector3.right * speed *Time.deltaTime);
			}
			
		}
	
		//does similarly to the if statements above, but along the z axis instead.
		if (alliedPosition.position.z > (objectPosition.z - 10) && alliedPosition.position.z < (objectPosition.z + 10))
		{
			if(alliedPosition.position.z > (objectPosition.z))
			{
				transform.Translate(Vector3.back * speed *Time.deltaTime);
			}
			
			if(alliedPosition.position.z < (objectPosition.z))
			{
				transform.Translate(Vector3.forward* speed *Time.deltaTime);
			}
		}
	}

	//enemy objects split up, so they can search for the player in a larger area.
	private void avoid()
	{
		Vector3 objectsPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		
		//The outer if is checking if the other enemy AI is within a certain radius on the x axis
		if (alliedPosition.position.x > (objectsPosition.x - 50) && alliedPosition.position.x < (objectsPosition.x + 50))
		{
			//if it is within a certain radius, and it is higher on the x axis, it will move to the right
			if(alliedPosition.position.x > (objectsPosition.x))
			{
				transform.Translate(Vector3.right * speed *Time.deltaTime);
			}
			
			//else if it's below the other object on the x axis, it will move to the left
			if(alliedPosition.position.x < (objectsPosition.x))
			{
				transform.Translate(Vector3.left * speed *Time.deltaTime);
			}
			
		}
		//similar to above but on the z axis.
		if (alliedPosition.position.z > (objectsPosition.z - 10) && alliedPosition.position.z < (objectsPosition.z + 10))
		{
			if(alliedPosition.position.z > (objectsPosition.z))
			{
				transform.Translate(Vector3.forward * speed *Time.deltaTime);
			}
			
			if(alliedPosition.position.z < (objectsPosition.z))
			{
				transform.Translate(Vector3.back* speed *Time.deltaTime);
			}
		}
	}

	//simply walks forward, it only needs to turn when the raycast hits a wall, in which the turning is done in the raycast method.
	private void wander()
	{
		int angle = Random.Range (0,180);
		float speed = 50F;
		transform.Translate(Vector3.forward * speed *Time.deltaTime);	
	}

	//looks at the player and moves towards.
	public void attract()
	{
		Debug.Log ("Attract");
		float speed = 50F;
		speed *= Time.deltaTime;
		transform.LookAt(target);
		transform.position = Vector3.MoveTowards(transform.position,target.position, speed);
	}

	//looks at the flare object, turns away from it and moves forward. 
	private void flee()
	{
		GameObject flareObject = GameObject.Find ("flare(Clone)");
		Transform flarePosition = flareObject.transform;
		speed *= Time.deltaTime;
		transform.LookAt (flarePosition);
		transform.Rotate(0,180,0);
		transform.Translate (Vector3.forward * speed * Time.deltaTime);      
	}
}