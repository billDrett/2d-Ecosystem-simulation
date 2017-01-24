using UnityEngine;
using System.Collections;

public class SquareNearSensor : MonoBehaviour
{

    // Use this for initialization
    Square square;
    // Use this for initialization
    void Start()
    {
        square = GetComponentInParent<Square>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "circle":
            case "triangle":
                square.objectInContact = other.gameObject;
                square.AInoncontact = AI_NonContact.alert;
                square.AIalert = AI_Alert.contact;
                square.AIcontact = AI_Contact.fight;
                break;
            case "cross":
                square.AIalert = AI_Alert.contact;
                square.AIcontact = AI_Contact.getHeal;
                square.objectInContact = other.gameObject;
                square.pathF.enabled = false;
                break;
            case "square":
                square.AIalert = AI_Alert.contact;
                square.AIcontact = AI_Contact.recreate;
                square.objectInContact = other.gameObject;
                square.pathF.enabled = false;
                break;
            case "squareHome": //be accident to home what happends ???
                square.AInoncontact = AI_NonContact.idle;
                square.pathF.enabled = false;
                //Debug.Log("Home...");
                square.vitality = square.lowHealth + 15;
                break;
            case "Food":
                square.stamina += 20;
                square.pathF.enabled = false;
                square.AIalert = AI_Alert.evade;
                FoodSpawner.foodCounter--;
                Destroy(other.gameObject);
                break;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {//mporei na min xreiazetai
        if (other.tag == "triangle" || other.tag == "circle" || other.tag == "square" || other.tag == "cross")
        {
            square.AIalert = AI_Alert.evade;
            square.AIcontact = AI_Contact.fight;
        }
    }
}
