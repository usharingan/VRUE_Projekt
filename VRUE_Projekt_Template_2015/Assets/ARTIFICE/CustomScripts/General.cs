using UnityEngine;
using System.Collections;

public class General : MonoBehaviour {
	public GameObject spacemouse;
	//GameObject spacemouseChild;
	public GameObject kinect;
	//GameObject kinectChild;

	// TEST
	public GameObject virtHand;
	public GameObject arm_wrist_left;
	public GameObject trackerObj;


	// Use this for initialization
	void Start () {
		Physics.gravity = Vector3.zero;


	}

	
	// Update is called once per frame
	void Update () {
		//spacemouse = GameObject.Find ("Spacemouse");
		//kinect = GameObject.Find ("TrackLeftHand");
	

		if (spacemouse != null && kinect != null) {
			if (Network.isServer) {

				//spacemouse off
				//child from spacemouse off
				spacemouse.SetActive (false);

				//trackerlefthand on
				//trackerobject child (lefthand) on
				kinect.SetActive (true);
			
				// TEST
				// TEST
				virtHand = GameObject.Find ("VirtualHand(Clone)");
				arm_wrist_left = GameObject.Find ("arm_finger_3a_left");//"arm_wrist_left");
				trackerObj = GameObject.Find("TrackerObject");

				// put virtual Hand position on arm_wrist_left gameObj
	//			virtHand.transform.position = arm_wrist_left.transform.position;
	//			virtHand.transform.rotation = arm_wrist_left.transform.rotation;

				trackerObj.transform.position = arm_wrist_left.transform.position; //+ new Vector3(0.0f, 0.5f, 0.0f);
				trackerObj.transform.rotation = arm_wrist_left.transform.rotation;
			

			
			} else {
				if (Network.isClient) {
				
					spacemouse.SetActive (true);
					kinect.SetActive (false);
					//spacemouse on
					//child from spacemouse on
				
					//trackerlefthand off
					//trackerobject child (lefthand) off
					GameObject virtualhand = GameObject.Find ("VirtualHand(Clone)");
					if(virtualhand == null)
					{
						Debug.Log("keine hand da");
					}else{
							if(virtualhand.GetComponents<TrackSpaceMouse>().Length == 0)
						{
							/*TrackSpaceMouse tmp = virtualhand.AddComponent(typeof(TrackSpaceMouse)) as TrackSpaceMouse;
							tmp.deviceName = "SpaceMouse";
							tmp.scalePosition = 20;
							tmp.enabled = true;
							tmp.isTracked();*/

							virtualhand.transform.parent = spacemouse.transform;
						}else{
							virtualhand.GetComponent<TrackBase>().isTracked();
							virtualhand.GetComponent<TrackBase>().setVisability(spacemouse,true);
						}
					}
				}
			}
		} else {
			Debug.Log ("bin im else");

		}
	}
}
