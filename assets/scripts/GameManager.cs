using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
    public Base_Shape sqr;
    public Base_Shape circle;
    public Base_Shape cross;
    public Base_Shape tri;
    public MapGenerator mpGenerator;
    public List<Base_Shape> ShapeList;
    public int NumberOfShapes = 4;
	// Use this for initialization
	void Start () {
        
		/*
		ShapeList = new List<Base_Shape>();
        for (int i = 0; i < NumberOfShapes; i++)
        {
            Instantiate(sqr, new Vector3(10+i*5, 10, 0), Quaternion.identity);
            ShapeList.Add(sqr);
            Instantiate(circle, new Vector3(-10 - i * 5, 10, 0), Quaternion.identity);
            ShapeList.Add(circle);
            Instantiate(cross, new Vector3(10 + i * 5, -10, 0), Quaternion.identity);
            ShapeList.Add(cross);
            Instantiate(tri, new Vector3(-10 - i * 5, -10, 0), Quaternion.identity);
            ShapeList.Add(tri);
        }
        */
	}
	
	// Update is called once per frame
	void Update () {
        
	}
}
