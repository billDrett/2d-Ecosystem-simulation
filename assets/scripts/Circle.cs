using UnityEngine;
using System.Collections;

public class Circle : Base_Shape{

    public float speed;
    public float maxSpeed;
    public Vector2 direction;
    public Rigidbody2D rgbdy2D;
	[Range(1,20)] public int restValue;
    

    // Use this for initialization
    void Start () {
        base.start();
        rgbdy2D = GetComponent<Rigidbody2D>();
        rgbdy2D.gravityScale = 0;
        ID = GetInstanceID();
        lowHealth = 10;
    }
	
	// Update is called once per frame
    void Update()
    {
        base.update();
		if ( (vitality < restValue) && (AInoncontact != AI_NonContact.alert) ) {	// if not in alert state - if health too low try to rest and heal
			AInoncontact = AI_NonContact.idle;	// set to idle so health can build up again - but also sub-states need to be reset
			AIalert = AI_Alert.evade;			// default to run away for circles
			AIcontact = AI_Contact.recreate;	// default for circles who are lovers more than haters
		}
	}

	public override void idle()
	{
		base.idle();
		if (vitality < maxVitality) vitality += healthDecayRate;
	}

	public override void recreate()
    {
		base.recreate();
		if (objectInContact != null) print ("I love " + objectInContact.ToString());
    }

    public override void explore()
    {
        base.explore();
       // Debug.Log("explore");
    }

}
