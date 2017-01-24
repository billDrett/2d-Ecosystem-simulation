using UnityEngine;
using System.Collections;

public class TriangleFarSensor : MonoBehaviour
{

    Triangle triangle;
    bool newDecision;
    // Use this for initialization
    void Start()
    {
        triangle = GetComponentInParent<Triangle>();
        newDecision = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (triangle.AInoncontact == AI_NonContact.alert) //if it is in another state but detectes hostile objects evade
        {
            if((other.tag == "square" || other.tag == "circle") && newDecision == true)
            {
                newDecision = false;
                triangle.objectDetected = other.gameObject;
                if (triangle.aggression > Random.Range(1, 10))
                {
                    triangle.AIalert = AI_Alert.chase;
                }
                else
                {
                    triangle.AIalert = AI_Alert.evade;
                }                
            }
        }
        else
        {
            if (other.tag == "square" || other.tag == "circle")
            {
                newDecision = false;
                triangle.AInoncontact = AI_NonContact.alert;
                triangle.objectDetected = other.gameObject;

                if (triangle.aggression > Random.Range(1, 10))
                {
                    triangle.AIalert = AI_Alert.chase;
                }
                else
                {
                    triangle.AIalert = AI_Alert.evade;
                }
            }
            else if ((other.tag == "cross" && triangle.vitality < Circle.maxVitality-20) || (other.tag == "triangle" && triangle.recreateKid)|| (other.tag == "Food"))
            {
                triangle.AInoncontact = AI_NonContact.alert;
                triangle.objectDetected = other.gameObject;
                triangle.AIalert = AI_Alert.chase;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {//mporei na min xreiazetai
        if ((triangle.objectDetected) && other.tag == triangle.objectDetected.tag)
        {
            triangle.AInoncontact = AI_NonContact.idle;
            triangle.AIalert = AI_Alert.evade;
            triangle.objectDetected = null;
            triangle.pathF.enabled = false;
            newDecision = true;
        }
    }
}

