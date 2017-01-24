using UnityEngine;
using System.Collections;

public class Square : Base_Shape{

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
	}

    public override void idle()
    {
        base.idle();
        //print("I am Square with ID = " + ID.ToString() + " and I am in Idle");
    }

    public override void evade()
    {
        base.evade();
        //print("I am Square with ID = " + ID.ToString() + " and I am in evade");
    }
}
