using UnityEngine;
using System.Collections;

public class SpawnInstrumentController : MonoBehaviour {

	// Use this for initialization
	public string PathInHierarchy="Orchestra";

	public AudioClip celloAudioClip = null;
	public AudioClip drumAudioClip = null;
	public AudioClip violinAudioClip = null;
	public AudioClip saxAudioClip = null;
	public AudioClip fluteAudioClip = null;
	public AudioClip guitarAudioClip = null;

	public Transform celloObj = null;
	public Transform drumObj = null;
	public Transform violinObj = null;
	public Transform saxObj = null;
	public Transform fluteObj = null;
	public Transform guitarObj = null;
	

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//if (Network.isServer) {
		if(Network.isClient){
			if (Input.GetButtonDown ("btChello")) {
				//SpawnNetworkObject (celloObj);
				this.networkView.RPC("SpawnNetworkInstrument",RPCMode.Server, "Cello");

			}
			if (Input.GetButtonDown ("btDrum")) {
				//SpawnNetworkObject (drumObj);
				this.networkView.RPC("SpawnNetworkInstrument",RPCMode.Server, "Drum");

			}
			if (Input.GetButtonDown ("btViolin")) {
				//SpawnNetworkObject (violinObj);
				this.networkView.RPC("SpawnNetworkInstrument",RPCMode.Server, "Violin");

			}
			if (Input.GetButtonDown ("btFlute")) {
				//SpawnNetworkObject (fluteObj);
				this.networkView.RPC("SpawnNetworkInstrument",RPCMode.Server, "Flute");

			}
			if (Input.GetButtonDown ("btGuitar")) {
				//SpawnNetworkObject (guitarObj);
				this.networkView.RPC("SpawnNetworkInstrument",RPCMode.Server, "Guitar");

			}
			if (Input.GetButtonDown ("btSaxophone")) {
				//SpawnNetworkObject (saxObj);
				this.networkView.RPC("SpawnNetworkInstrument",RPCMode.Server, "Sax");
			}
		}
	}

	void addAllComponents(GameObject obj, NetworkViewID viewID, Vector3 coliderSize, Vector3 coliderCenter, AudioClip clip){

		BoxCollider colider = obj.AddComponent<BoxCollider>();
		colider.size = coliderSize;
		colider.center = coliderCenter;
		
		AudioSource audio = obj.AddComponent<AudioSource>();
		audio.loop = true;
		
		audio.clip = clip;//clip;
		audio.enabled = false;


		UserManagementObjectController controller = obj.AddComponent<UserManagementObjectController> ();
		controller.accessGrantedName = "player2";
		//RPC Methode unten vielleicht
		//this.networkView.RPC("setAccessGrantedPlayer", RPCMode.Server, 0);
		controller.setAccessGrantedPlayer ();

		NetworkView net = obj.AddComponent<NetworkView> ();
		net.observed = controller;

		net.viewID = viewID;


		Rigidbody rigid = obj.AddComponent<Rigidbody> ();
		rigid.useGravity = false;
		rigid.isKinematic = true;

		AnimationScript anime = obj.AddComponent<AnimationScript> ();
		anime.currentGameObj = obj;
		anime.enabled = false;


		MeshRenderer ren = obj.AddComponent<MeshRenderer> ();
	}

	/*private void SpawnNetworkObject(Transform obj)
	{

		NetworkViewID viewID = Network.AllocateViewID();
		Network.Instantiate(obj, transform.position, transform.rotation, 0);
		this.networkView.RPC("relocateInstrumentRPC", RPCMode.AllBuffered, viewID, obj.name);	
	}*/
	[RPC]
	private void SpawnNetworkInstrument(string name)
	{
		NetworkViewID viewID = Network.AllocateViewID();
		Transform obj = null;

		if (name == "Cello" ) {
			obj = celloObj;
			
		}
		if (name == "Drum") {
			obj = drumObj;
			
		}
		if (name == "Violin") {
			obj = violinObj;
			
		}
		if (name == "Flute") {
			obj = fluteObj;
			
		}
		if (name == "Guitar") {
			obj = guitarObj;
			
		}
		if (name == "Sax") {
			obj = saxObj;
		}
		Network.Instantiate(obj, transform.position, transform.rotation, 0);
		this.networkView.RPC("relocateInstrumentRPC", RPCMode.AllBuffered, viewID, obj.name);	
	}

	private void relocateObject2(NetworkViewID viewID, string name)
	{

		Vector3 scale = new Vector3();
		Vector3 coliderSize = new Vector3();
		Vector3 coliderCenter = new Vector3();

		AudioClip clip = null;

		if (name == "Cello") {
			
			scale = new Vector3 (0.04f, 0.04f, 0.04f);
			coliderSize = new Vector3(70.0f,160.0f,30.0f);
			coliderCenter = new Vector3(0.0f,0.0f,0.0f);

			clip = celloAudioClip;
			
		}
		if (name == "Drum") {
			scale = new Vector3 (0.015f, 0.015f, 0.015f);
			coliderSize = new Vector3(70.0f,110.0f,60.0f);
			coliderCenter = new Vector3(0.0f,50.0f,0.0f);

			clip = drumAudioClip;
			
		}
		if (name == "Violin") {
			scale = new Vector3 (0.02f, 0.02f, 0.02f);
			coliderSize = new Vector3(40.0f,15.0f,110.0f);
			coliderCenter = new Vector3(0.0f,0.0f,0.0f);

			clip = violinAudioClip;
			
		}
		if (name == "Flute") {
			scale = new Vector3 (1.2f, 1.2f, 1.2f);
			coliderSize = new Vector3(-0.1f,1.3f,0.12f);
			coliderCenter = new Vector3(0.0f,-0.46f,0.0f);

			clip = fluteAudioClip;
			
		}
		if (name == "Guitar") {
			scale = new Vector3 (0.05f, 0.05f, 0.05f);
			coliderSize = new Vector3(19.0f,52.0f,4.0f);
			coliderCenter = new Vector3(0.0f,0.0f,0.0f);

			clip = guitarAudioClip;
			
		}
		if (name == "Sax") {
			scale = new Vector3 (0.04f, 0.04f, 0.04f);
			coliderSize = new Vector3(2.0f,9.0f,8.0f);
			coliderCenter = new Vector3(0.0f,0.0f,0.0f);

			clip = saxAudioClip;
			
		}


		string objName="/"+name+"(Clone)";
		Debug.Log(objName);
		GameObject newObj = GameObject.Find(objName);

		if((GameObject.Find(PathInHierarchy)!=null)&&(newObj!=null))
		{
			Debug.Log("attached to parent network");
			Vector3 locPos=newObj.transform.localPosition;
			Quaternion locRot = newObj.transform.localRotation;
			newObj.transform.parent=GameObject.Find(PathInHierarchy).transform;
			newObj.transform.localScale=scale;
			newObj.transform.localPosition=locPos;
			newObj.transform.localRotation = locRot;
			
			newObj.transform.tag = name;
			
			addAllComponents(newObj, viewID, coliderSize, coliderCenter, clip);
		}
	}

	
	[RPC]
	public virtual void relocateInstrumentRPC(NetworkViewID viewID, string name)
	{
		relocateObject2(viewID, name);
	}
}
