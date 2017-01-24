using UnityEngine;
using System.Collections;

public class TriangleNearSensor : MonoBehaviour {

	Triangle triangle;
	// Use this for initialization
	void Start () {
		triangle = GetComponentInParent<Triangle> ();
	}

	// Update is called once per frame
	void Update () {
		if (triangle.AIcontact == AI_Contact.fight) {

		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
        switch(other.tag)
        {
            case "circle":
            case "square":
                triangle.objectInContact = other.gameObject;
                triangle.AInoncontact = AI_NonContact.alert;
                triangle.AIalert = AI_Alert.contact;
                triangle.AIcontact = AI_Contact.fight;
                break;
            case "cross":
                triangle.AIalert = AI_Alert.contact;
                triangle.AIcontact = AI_Contact.getHeal;
                triangle.objectInContact = other.gameObject;
                triangle.pathF.enabled = false;                
                break;
            case "triangle":
                triangle.AIalert = AI_Alert.contact;
                triangle.AIcontact = AI_Contact.recreate;
                triangle.objectInContact = other.gameObject;
                triangle.pathF.enabled = false;                
                break;
            case "triangleHome": //be accident to home what happends ???
                triangle.AInoncontact = AI_NonContact.idle;
                triangle.pathF.enabled = false;
                //Debug.Log("Home...");
                triangle.vitality = triangle.lowHealth + 15;
                break;
            case "Food":
                triangle.stamina += 20;
                triangle.pathF.enabled = false;
                triangle.AIalert = AI_Alert.evade;
                FoodSpawner.foodCounter--;
                Destroy(other.gameObject);
                break;
        }
	}

	void OnTriggerExit2D(Collider2D other)
	{//mporei na min xreiazetai
        if (other.tag == "triangle" || other.tag == "circle" || other.tag == "square" || other.tag == "cross")
        {
            triangle.AIalert = AI_Alert.evade;
            triangle.AIcontact = AI_Contact.fight;
        }
    }
}