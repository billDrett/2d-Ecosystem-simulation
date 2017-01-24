using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FoodSpawner : MonoBehaviour {
	public int foodItems;
	List<Food> foodList;
	public int timer;
	public Food food;
	public MapGenerator mpGenerator;
    public static int foodCounter;

	
	// Use this for initialization
	void Start () {
		foodList = new List<Food>();
        foodCounter = 0;
    }
	
	// Update is called once per frame
	void Update () {
		if (Time.time > timer && foodCounter < 50)
		{
			Coord random_food = mpGenerator.RandomSpotForSpawn();

			//Vector3 _positionJ = new Vector3 ((float)(0.3f * entr.tileX), (float)(0.3f * entr.tileY), 0);


			Instantiate(food,mpGenerator.CoordToWorldPoint(random_food) , Quaternion.identity);
			timer++;
            foodCounter++;
		}
	}
}
