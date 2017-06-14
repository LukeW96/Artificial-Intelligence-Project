using UnityEngine;
using System.Collections;

public class preyScript : MonoBehaviour {

	// Use this for initialization
	float speed = 100.0f;
	void Start ()
	{
	}

	// Update is called once per frame
	void Update ()
	{
		GameObject parentObject = GameObject.Find ("preyLeader");
		Quaternion parentRotation = parentObject.transform.rotation;
		transform.localRotation = parentRotation;
	
		string objectName = "prey";
		string targetName = "prey";
		float furthestDistance = 0;
		string objectFurthestAway = "";
		int closestObject = 0;
		string moveTowardsObject = "";

		for (int i = 0; i<6; i++)
		{
			if(i >= 1)
			{
				objectName = objectName + " (" + i + ")";
			}

			GameObject target1Object = GameObject.Find (objectName);
			Transform target1Position = target1Object.transform;

			for(int j = 0; j<5; j++)
			{
				if(j >= 1)
				{		
					targetName = targetName + " (" + j + ")";
				}

				GameObject target2Object = GameObject.Find (targetName);
				Transform target2Position = target2Object.transform;
				float distanceBetween = Vector3.Distance(target1Position.position, target2Position.position);

				if(distanceBetween > furthestDistance)
				{
					furthestDistance = distanceBetween;
					objectFurthestAway = targetName;
				}
				targetName = "prey";
			}
			objectName = "prey";

		}

		if (objectFurthestAway == gameObject.name)
		{
			for(int x = 0; x < 6; x++)
			{
				targetName = "prey";
				if(x >= 1)
				{		
					targetName = targetName + " (" + x + ")";
				}
				
				GameObject target2Object = GameObject.Find (targetName);
				Transform target2Position = target2Object.transform;
				float distanceBetween = Vector3.Distance(transform.position, target2Position.position);

				if(distanceBetween < closestObject)
				{
					closestObject = x;
				}
			}

			if( closestObject >= 1)
			{
				moveTowardsObject = "prey" + " (" + closestObject + ")";
			}

			else 
			{
				moveTowardsObject = "prey";
			}

			GameObject objectMoveTo = GameObject.Find (moveTowardsObject);
			Transform objectMoveToPosition = objectMoveTo.transform;
			transform.position = Vector3.MoveTowards(transform.position, objectMoveToPosition.position, speed);
		}

	}
}