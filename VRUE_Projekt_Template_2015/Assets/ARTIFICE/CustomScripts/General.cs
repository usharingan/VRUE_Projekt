using UnityEngine;
using System.Collections;

public class General : MonoBehaviour {
	public GameObject spacemouse;
	//GameObject spacemouseChild;
	public GameObject kinect;
	//GameObject kinectChild;

	// Use this for initialization
	void Start () {

	}

	
	// Update is called once per frame
	void Update () {
		//spacemouse = GameObject.Find ("Spacemouse");
		//kinect = GameObject.Find ("TrackLeftHand");
	

		if (spacemouse != null && kinect != null) {
			if (Network.isServer) {

				spacemouse.SetActive (false);
				kinect.SetActive (true);
			
				//spacemouse off
				//child from spacemouse off
				
			
				//trackerlefthand on
				//trackerobject child (lefthand) on
			
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
