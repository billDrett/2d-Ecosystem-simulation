using UnityEngine;
using System.Collections;

public class SquareFarSensor : MonoBehaviour {
    Square square;
    bool newDecision;
    // Use this for initialization
    void Start()
    {
        square = GetComponentInParent<Square>();
        newDecision = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (square.AInoncontact == AI_NonContact.alert) //if it is in another state but detectes hostile objects evade
        {
            if ((other.tag == "triangle" || other.tag == "circle") && newDecision == true)
            {
                newDecision = false;
                square.objectDetected = other.gameObject;
                if (square.aggression > Random.Range(1, 10))
                {
                    square.AIalert = AI_Alert.chase;
                }
                else
                {
                    square.AIalert = AI_Alert.evade;
                }
            }
        }
        else
        {
            if (other.tag == "triangle" || other.tag == "circle")
            {
                newDecision = false;
                square.AInoncontact = AI_NonContact.alert;
                square.objectDetected = other.gameObject;

                if (square.aggression > Random.Range(1, 10))
                {
                    square.AIalert = AI_Alert.chase;
                }
                else
                {
                    square.AIalert = AI_Alert.evade;
                }
            }
            else if ((other.tag == "cross" && square.vitality < Circle.maxVitality) || (other.tag == "square" && square.recreateKid) || (other.tag == "Food"))
            {
                square.AInoncontact = AI_NonContact.alert;
                square.objectDetected = other.gameObject;
                square.AIalert = AI_Alert.chase;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {//mporei na min xreiazetai
        if ((square.objectDetected) && other.tag == square.objectDetected.tag)
        {
            square.AInoncontact = AI_NonContact.idle;
            square.AIalert = AI_Alert.evade;
            square.objectDetected = null;
            square.pathF.enabled = false;
            newDecision = true;
        }
    }

}
