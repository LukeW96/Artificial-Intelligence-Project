using UnityEngine;
using System.Collections;

public class terrainGeneration : MonoBehaviour 
{
	int directionchosen;
	int[,] mapArray = new int[50,50];
	// Use this for initialization
	void Start () 
	{
		//initialises the 20x20 array 
		for (int i = 0; i < 20; i++)
		{
			for (int j = 0; j < 20; j++)
			{
				mapArray [i, j] = 1;
			}
		}

		//sets the initial position to the middle of the array
		int xCoord = 10;
		int yCoord = 10;

		//randomly removes potential wall placements.
		for (int a = 0; a < 400; a++)
		{
			directionchosen = Random.Range (0, 4);
			while (outofBounds (xCoord,yCoord,directionchosen))
			{
				directionchosen = Random.Range (0, 4);
			}
			switch (directionchosen)
			{
				case 0: 
					xCoord--;
					mapArray [xCoord, yCoord] = 0;
					xCoord--;
					mapArray [xCoord, yCoord] = 0;
					break;
				case 1: 
					xCoord++;
					mapArray [xCoord, yCoord] = 0;
					xCoord++;
					mapArray [xCoord, yCoord] = 0;
					break;
				case 2: 
					yCoord--;
					mapArray [xCoord, yCoord] = 0;
					yCoord--;
					mapArray [xCoord, yCoord] = 0;
					break;
				case 3: 
					yCoord++;
					mapArray [xCoord, yCoord] = 0;
					yCoord++;
					mapArray [xCoord, yCoord] = 0;
					break;
			}
		}

		//generates the procedurally generated maze
		Vector3 positionVector = new Vector3 (0, 0, 0);
		if (gameObject.name == "wall") 
		{
			for (int k = 0; k < 20; k++) {
				for (int l = 0; l < 20; l++) {
					if (mapArray [k, l] == 1) {

						Instantiate (gameObject);
						positionVector.x = k*10;
						positionVector.z = l*10;
						transform.position = positionVector;
						transform.localScale = new Vector3(10,10,10);
					}
				}
			}
		}
		//placeCharacter ();
	}

	// Update is called once per frame
	void Update () 
	{
	}

	//checks if it will go outside the array size, this is so they out of index exceptions don't occur.
	public bool outofBounds(int xCoord, int yCoord, int directionChosen)
	{
		if(xCoord == 2 && directionChosen == 0)
			return true;
		
		else if(xCoord == 48 && directionChosen == 1)
			return true;
		
		else if(yCoord == 2 && directionChosen == 2)
			return true;
		
		else if(yCoord == 48 && directionChosen == 3)
			return true;
		
		else
			return false;	
	}

	//loops through the maze array, finds the first space not occupied by a wall and positions the player there.
	void placeCharacter()
	{
		bool flag = false;
		Vector3 placeChar = new Vector3(0,0,0);

		int i = 0;
		int j = 0;
		while (i <20 && flag == false)
		{
			for(j = 0; j <20; j++)
			{
				if(mapArray[i,j] == 0)
				{
					placeChar.x = (i*10);
					placeChar.z = (j*10);
					GameObject newTarget = GameObject.FindGameObjectWithTag("Player");
					newTarget.transform.position = placeChar;
					flag = true;
					break;		
				}			
			}
			i++;
		}
	}
}