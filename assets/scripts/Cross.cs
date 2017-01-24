using UnityEngine;
using System.Collections;

public class Cross : Base_Shape{

    public float speed;
    public float maxSpeed;
    public Vector2 direction;
    public Rigidbody2D rgbdy2D;
	
    // Use this for initialization
	void Start () {
        base.start();
        rgbdy2D = GetComponent<Rigidbody2D>();
        rgbdy2D.gravityScale = 0;
        ID = GetInstanceID();
	}
	
	// Update is called once per frame
	void Update () {
        base.update();
        stamina = 100;
	}
    public override void idle()
    {
        base.idle();
        //print("I am Cross with ID = " + ID.ToString());
    }
}
