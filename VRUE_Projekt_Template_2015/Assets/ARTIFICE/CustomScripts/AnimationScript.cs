using UnityEngine;
using System.Collections;

public class AnimationScript : MonoBehaviour {

	public bool dance_bool;
	bool bewegungNachLinks, bewegungNachRechts;
	Quaternion from, to;
	public float speed;


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
