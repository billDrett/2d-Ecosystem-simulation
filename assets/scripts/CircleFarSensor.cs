using UnityEngine;
using System.Collections;

public class CircleFarSensor : MonoBehaviour {
    Circle circle;
    // Use this for initialization
    void Start ()
    {
        circle = GetComponentInParent<Circle>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other)
	{
        if (circle.AInoncontact == AI_NonContact.alert) //if it is in another state but detectes hostile objects evade
        {
            if ((other.tag == "triangle" || other.tag == "square") && (circle.objectDetected.tag == "cross" || circle.objectDetected.tag == "circle"))
            {
                Debug.Log("if " + other.tag);
                circle.AIalert = AI_Alert.evade;
                circle.objectDetected = other.gameObject;
            }
        }
        else
        {
            if (other.tag == "triangle" || other.tag == "square")
            {

                circle.AInoncontact = AI_NonContact.alert;
                circle.objectDetected = other.gameObject;
                circle.AIalert = AI_Alert.evade;
            }
            else if((other.tag == "cross" && circle.vitality < Circle.maxVitality) || (other.tag == "circle" && circle.recreateKid) || (other.tag == "Food")) //kai an xreiazete kati apo ta dyo 
            {
                circle.AInoncontact = AI_NonContact.alert;
                circle.objectDetected = other.gameObject;
                circle.AIalert = AI_Alert.chase;
            }
        }
    }

	void OnTriggerExit2D(Collider2D other)
	{
        if((circle.objectDetected) && other.tag == circle.objectDetected.tag)
        {
            circle.AInoncontact = AI_NonContact.idle;
            circle.AIalert = AI_Alert.evade;
            circle.objectDetected = null;
            circle.pathF.enabled = false;
        }
    }
	
}
