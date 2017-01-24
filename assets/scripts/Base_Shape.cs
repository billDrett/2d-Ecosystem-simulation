using UnityEngine;
using System.Collections;

public enum Tribe { Square, Triangle, Circle, Cross }
public enum AI_NonContact { idle, explore, goHome, alert }
public enum AI_Alert { evade, chase, contact }
public enum AI_Contact { fight, recruit, recreate, healOther, getHeal }

public class Base_Shape : MonoBehaviour {

    public AI_NonContact AInoncontact;
    public AI_Alert AIalert;
    public AI_Contact AIcontact;

	// Base attributes of characters
    public int ID;
    public Tribe tribe; 
    public Tribe shapeType;
    public Vector2 size = Vector2.one;
	public float age;
	public const float maxVitality = 100;
	[Range(1,maxVitality)] public float vitality;
    [Range(1, 100)] public float stamina;
    [Range(1,10)] public float agility;
	[Range(1,10)] public float strength;
	[Range(1,10)] public float intelligence;
	[Range(1,10)] public float personality;
	[Range(1,10)] public float luck;
	[Range(1,10)] public float aggression;
	[Range(1,10)] public float attractiveness;

	// Parameters for changing attribute values
	public float healthDecayRate;
	public int agingRate;
	public int lifeSpan;
	public Vector2 growthRate;
    public int lowHealth;

    public int closeSensor;
    public int farSensor;

   // public MapGenerator mpGenerator;
    GameManager gameManager;

    public Base_Shape recreateObject;
    public Transform home;
	float gameStartTime, lastTime;
    public bool recreateKid;

    public Pathfinding pathF;
    public GameObject objectDetected;
    public GameObject objectInContact;

    bool newDecision;
    GameObject waypoint;

    int stayStillCounter;
    Vector3 previousposition;

    // Use this for initialization
    public void start () {
        
		gameStartTime = Time.time;
		lastTime = gameStartTime;

		gameManager = GameObject.FindWithTag("gameManager").GetComponent<GameManager>();
        // AI defaults
        AInoncontact = AI_NonContact.idle;
        AIalert = AI_Alert.evade;
        AIcontact = AI_Contact.recruit;

        pathF = GetComponent<Pathfinding>();
        objectDetected = null;
        objectInContact = null;
        newDecision = true;
        recreateKid = false;

        Coord randomPoint = gameManager.mpGenerator.RandomSpotForSpawn();
        waypoint = new GameObject("target0");
        waypoint.transform.position = gameManager.mpGenerator.CoordToWorldPoint(randomPoint);
    }
	
	// Update is called once per frame
	public void update ()
    {
	   
		vitality -= healthDecayRate;
		if (Time.time - lastTime > agingRate)
        {
			// aging
			age++;
            stamina -= 2;
            if(stamina <= 0)
            {
                Destroy(gameObject);
            }
            if (age != 0 && age % 10 == 0)
            {
                recreateKid = true;
            }
                //losing health
            vitality -= healthDecayRate * ((12 - luck) / 10);
			if (age > lifeSpan) if (Random.Range(0,12) < luck) Destroy(gameObject);
			if (vitality < 0) Destroy(gameObject);
			// growing
			size += growthRate;
			if (transform.localScale.x < 2)
				//transform.localScale += new Vector3 (size.x, size.y, 0);

			lastTime = Time.time;
		}

        if(this.transform.position == previousposition)
        {
            stayStillCounter++;
        }
        else
        {
            stayStillCounter = 0;
        }
        previousposition = this.transform.position;
        if (stayStillCounter > 60)//1 second still
        {
            stayStillCounter = 0;
            AInoncontact = AI_NonContact.explore;
            Coord randomPoint = gameManager.mpGenerator.RandomSpotForSpawn();
            waypoint.transform.position = gameManager.mpGenerator.CoordToWorldPoint(randomPoint);
            pathF.enabled = true;
            Debug.Log("tes");
        }

        
        //MAKE DECISION 1
        if(vitality < lowHealth)
        {//mporei allo state
            AInoncontact = AI_NonContact.goHome;
        }
        if(AInoncontact != AI_NonContact.alert) newDecision = true;

        /*if(AInoncontact == AI_NonContact.alert)
        {//prosoxi ama einai idio eidos
            //if(gameObject.tag == objectDetected.tag) exit;
            if(UnityEngine.Random.Range(1, 11) < aggression + Mathf.Round((float)vitality/(float)strength))
            {
                AIalert = AI_Alert.chase;
            }
            else
            {
                AIalert = AI_Alert.evade;
            }
        }*/

        switch (AInoncontact)
        {
            case AI_NonContact.idle: idle(); break;
            case AI_NonContact.explore: explore(); break;
            case AI_NonContact.goHome: goHome(); break;
            case AI_NonContact.alert:
            {
                    //MAKE DECISION 2
                   // Debug.Log("t");
                    //objectDetected.g;
                    // int diffAggresion = Mathf.Round((float)vitality / (float)strength) - Mathf.Round((float)objectDetected.vitality / (float)objectDetected.strength)
                    /*if(AIalert != AI_Alert.contact && newDecision == true)
                    {

                        newDecision = false;
                        if (UnityEngine.Random.Range(1, 11) < aggression)
                        {
                            AIalert = AI_Alert.chase;
                            
                        }
                        else
                        {
                            AIalert = AI_Alert.evade;
                        }
                    }*/
                    switch (AIalert)
                    {
                        case AI_Alert.chase: chase(); break;
                        case AI_Alert.evade: evade(); break;
                        case AI_Alert.contact:
                        {
                                //MAKE DECISION 3
                                switch (AIcontact)
                                {
                                    case AI_Contact.recruit: recruit(); break;
                                    case AI_Contact.recreate: recreate(); break;
                                    case AI_Contact.healOther: heal(); break;
                                    case AI_Contact.fight: fight(); break;
                                    case AI_Contact.getHeal: getHeal(); break;
                                    default: break;
                                }
                                break;
                        }
                        default: break;
                    }
                    break;
            }
            default : break; 
        }
	}

    public virtual void fight()
    {
        //Debug.Log("fight");
        float hit;
        pathF.enabled = false;

        if (!objectInContact) //the game object is destroyed.
        {
            AIalert = AI_Alert.evade;

            return;
        }

        if (Random.Range(0, 10) == 0) //10% critical hit
        {
            hit = (float)strength / 3;
        }
        else
        {
            hit = (float)strength / 5;
        }

        objectInContact.SendMessage("hitMe", hit);
    }

    public virtual void heal()
    {
       // Debug.Log("Heal");
    }

    public virtual void recreate()
    {
        //Debug.Log("recreate");
        if (recreateKid == false) return;
        recreateKid = false;
        Instantiate(this, this.transform.localPosition, Quaternion.identity);
        AIalert = AI_Alert.evade;
        AInoncontact = AI_NonContact.idle;

    }

    public virtual void recruit()
    {
        //Debug.Log("recruit");
    }

    public virtual void evade()
    {
        Coord randomPoint;
        AInoncontact = AI_NonContact.explore;

        randomPoint = gameManager.mpGenerator.RandomSpotForSpawn();
        waypoint.transform.position = gameManager.mpGenerator.CoordToWorldPoint(randomPoint);
        // Debug.Log("evade");
        //AInoncontact = AI_NonContact.goHome;
    }

    public virtual void chase()
    {
        if (!objectDetected) //the game object is destroyed.
        {
            AIalert = AI_Alert.evade;
            return;
        }
        //Debug.Log("chase");
        pathF.target = objectDetected.transform;
        pathF.enabled = true;
    }

    public virtual void huntFood()
    {
        //Debug.Log("huntFood");
    }

    public virtual void explore()
    {
        //Debug.Log("explore");
        Coord randomPoint;

        pathF.target = waypoint.transform;
        pathF.enabled = true;
        if (Vector3.Distance(waypoint.transform.position, this.transform.position) < 5)
        {
            randomPoint = gameManager.mpGenerator.RandomSpotForSpawn();
            waypoint.transform.position = gameManager.mpGenerator.CoordToWorldPoint(randomPoint);
            //pathF.target = waypoint.transform;
        }
    }

    public virtual void goHome()
    {//pathfinding to home
        pathF.target = home;
        pathF.enabled = true;
    }

    public virtual void idle()
    {
        //Debug.Log("idle");
        int randomNum = Random.Range(0, 10);
        if(randomNum == 0)
        {
            return;
        }
        else
        {
            AInoncontact = AI_NonContact.explore;
        }
    }

    public virtual void getHeal()
    {
        vitality += 5;
        if (vitality > maxVitality)
        {
            vitality = maxVitality;
           AInoncontact = AI_NonContact.idle;
        }
    }

    public virtual void hitMe(int healthLost)
    {
        /*if (vitality < lowHealth)
        {
            AIalert = AI_Alert.evade;
        }*/
        vitality -= healthLost;
        //AInoncontact = AI_NonContact.alert; // May already be in this top state anyway
        //AIalert = AI_Alert.evade;			// Getting hurt so run away
    }
}
