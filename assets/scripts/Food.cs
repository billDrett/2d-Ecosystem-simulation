using UnityEngine;
using System.Collections;

public class Food : MonoBehaviour {
	public int value;
	public int maxValue;
	float timer = 0.5f;
	float lastTime;
	public float lifeTime;
	float startTime;
	// Use this for initialization
	void Start () {
		startTime = Time.time;
		lastTime = startTime;
        //gameObject.tag = "Food";
	}
	
	// Update is called once per frame
	void Update () {
        /*
		if (Time.time > lastTime + timer)
		{
			if (value != maxValue)
			{
				value++;
				this.transform.localScale = new Vector3(this.transform.localScale.x + 0.1f, this.transform.localScale.y + 0.1f, this.transform.localScale.z + 0.1f);
			}
			lastTime = Time.time;           
		}
		if (Time.time - startTime > lifeTime) Destroy(gameObject);*/
	}
}