using UnityEngine;
using System.Collections;

public class CircleNearSensor : MonoBehaviour {

	Circle circle;
	// Use this for initialization
	void Start () {
		circle = GetComponentInParent<Circle> ();
	}

	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter2D(Collider2D other)
	{
        switch(other.tag)
        {
            case "triangle":
            case "square":
                circle.AIalert = AI_Alert.evade;
                break;
            case "cross": //prosoxi min einai infinity loop allazei se idle alla ksanampenei edw
                circle.AIalert = AI_Alert.contact;
                circle.objectInContact = other.gameObject;
                circle.pathF.enabled = false;
                circle.AIcontact = AI_Contact.getHeal;
                break;
            case "circle":
                circle.AIalert = AI_Alert.contact;
                circle.objectInContact = other.gameObject;
                circle.pathF.enabled = false;
                circle.AIcontact = AI_Contact.recreate;
                break;
            case "circleHome": //be accident to home what happends ???
                circle.AInoncontact = AI_NonContact.idle;
                circle.pathF.enabled = false;
                //Debug.Log("Home...");
                circle.vitality = circle.lowHealth + 15;
                break;
            case "Food":
                circle.stamina += 20;
                circle.pathF.enabled = false;
                circle.AIalert = AI_Alert.evade;
                FoodSpawner.foodCounter--;
                Destroy(other.gameObject);
                //
                break;
        }
    }

	void OnTriggerExit2D(Collider2D other)
	{//mporei na min xreiazontai
        if (other.tag == "triangle" || other.tag == "square" || other.tag == "cross" || other.tag == "circle")
        {
            circle.AInoncontact = AI_NonContact.alert;
            circle.AIalert = AI_Alert.evade;
            circle.AIcontact = AI_Contact.recreate;
        }
    }
}