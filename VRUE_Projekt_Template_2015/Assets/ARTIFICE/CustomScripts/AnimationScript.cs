using UnityEngine;
using System.Collections;

public class AnimationScript : MonoBehaviour {

	public bool dance_bool;
	bool bewegungNachLinks, bewegungNachRechts;
	Quaternion from, to;
	public float speed;

	GameObject cello; 
	GameObject sax;
	GameObject guitar;
	GameObject flute;
	GameObject drum;
	GameObject violin;


	//cheat - temporaer?
	public GameObject currentGameObj;

	// Use this for initialization
	void Start () {
		dance_bool = true;
		bewegungNachLinks = true;
	//	bewegungNachRechts = false;
		from = Quaternion.Euler(new Vector3(0, 0, -20));
		to = Quaternion.Euler(new Vector3(0, 0, 20));
		speed = 0.1F;

		cello = GameObject.Find("Cello"); 
		sax = GameObject.Find("Sax"); 
		guitar = GameObject.Find("Guitar"); 
		flute  = GameObject.Find("Flute"); 
		drum = GameObject.Find("Drum"); 
		violin = GameObject.Find("Violin"); 
	}
	
	// Update is called once per frame
	void Update () {
	
		// rotation Z zwischen [-20, 20]; start at 0

		/*
		 * public Transform from;
    public Transform to;
    public float speed = 0.1F;
    void Update() {
        transform.rotation = Quaternion.Slerp(from.rotation, to.rotation, Time.time * speed);
    }
		 */

	// TODO !
	// custom rotation for each individual instrument! :D
/*		if ((this == cello) || (this == sax) || (this == drum) || (this == guitar)) {
			transform.Rotate (0, 20 * Time.deltaTime, 0);
		} else if () {
		
		}*/
		if(gameObject.GetInstanceID() == violin.GetInstanceID()){
			transform.Rotate (0, 20 * Time.deltaTime, /*20 * Time.deltaTime*/0);
		}
		else if (gameObject.GetInstanceID() == flute.GetInstanceID()){
			transform.Rotate (0, 0, -10 * Time.deltaTime);
		}
		else{
			transform.Rotate (0, 20 * Time.deltaTime, 0);
		}
	/*	if(gameObject.tranform.rotation.z > -20){

//		currentGameObj.transform.rotation = Quaternion.Slerp (from, to, Time.time * speed);

		//gameObject
		gameObject.transform.rotation = Quaternion.Slerp (from, to, Time.time * speed);
		
		}
	*/
	}

	void setDanceBool(bool var){
		dance_bool = var;
	}
}
