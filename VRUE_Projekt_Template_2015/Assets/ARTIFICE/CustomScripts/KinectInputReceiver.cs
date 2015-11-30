using UnityEngine;
using System.Collections;

public class KinectInputReceiver : MonoBehaviour {
	
	GameObject cello; 
	GameObject violin;
	GameObject guitar;
	GameObject sax;
	GameObject flute;
	GameObject drum;

	GameObject virtualHand;
	VirtualHandInteraction vH_Interaction;

	Hashtable selectedInstruments;

	AudioSource celloAudioSource, violinAudioSource, guitarAudioSource,
	saxAudioSource, fluteAudioSource, drumAudioSource;

	int celloTouchCount, violinTouchCount, guitarTouchCount,
	saxTouchCount, fluteTouchCount, drumTouchCount;

	bool celloPlay, violinPlay, guitaPlay,
	saxPlay, flutePlay, drumPlay;

	float volumeReductionFactor;


	// Use this for initialization
	void Start () {
		// 1) hol dir alle instrumente
		// g-instruments -> play when g is pressed
		cello = GameObject.Find("Cello"); 
		violin = GameObject.Find("Violin"); 
		guitar = GameObject.Find("Guitar"); 
		// b-instruments -> play when b is pressed
		sax = GameObject.Find("Sax"); 
		flute  = GameObject.Find("Flute"); 
		drum = GameObject.Find("Drum"); 
	


		selectedInstruments = new Hashtable();

		//audioSources
		celloAudioSource = cello.GetComponent<AudioSource> ();
		violinAudioSource = violin.GetComponent<AudioSource> (); 
		guitarAudioSource = guitar.GetComponent<AudioSource> ();
		saxAudioSource = sax.GetComponent<AudioSource> ();
		fluteAudioSource = flute.GetComponent<AudioSource> ();
		drumAudioSource = drum.GetComponent<AudioSource> ();

		// touch counters
		// wenn gerade Zahl -> not in selected Instruments hashtable
		// wenn ungerade Zahl -> in selected Instruments hashtable
		celloTouchCount = 0;
		violinTouchCount = 0;
		guitarTouchCount = 0;
		saxTouchCount = 0;
		fluteTouchCount = 0;
		drumTouchCount = 0;

		//play bools
		celloPlay = false;
		violinPlay = false;
		guitaPlay = false;
		saxPlay = false; 
		flutePlay = false;
		drumPlay = false;

		volumeReductionFactor = 1;

	}
	
	// Update is called once per frame
	void Update () {

		virtualHand = GameObject.Find ("VirtualHand(Clone)");
		
		vH_Interaction = virtualHand.GetComponent<VirtualHandInteraction> ();

		// 2) 
		// check & update selected Instruments hashtable
		// wenn Instrumente einmal selektiert/beruehrt -> in die selected Instruments hashtable gespeichert
		Hashtable collidees = vH_Interaction.getCollidees ();

		if (collidees.ContainsKey (cello.GetInstanceID ())) {

			celloTouchCount++;
			if(celloTouchCount % 2 == 1){
				selectedInstruments.Add (cello.GetInstanceID (), cello);
			}else{
				// 6) 
				// nochmal instrument beruehren/"selektieren" 
				// -> audio stop
				celloAudioSource.Stop();

				// -> not in selected Instru anymore (kann getauscht werden etc)
				selectedInstruments.Remove(cello.GetInstanceID());
			}
		}
		if (collidees.ContainsKey (violin.GetInstanceID ())) {

			violinTouchCount++;
			if(violinTouchCount % 2 == 1){
				selectedInstruments.Add (violin.GetInstanceID (), violin);
			}else{
				violinAudioSource.Stop();
				selectedInstruments.Remove(violin.GetInstanceID());
			}
		}
		if (collidees.ContainsKey (guitar.GetInstanceID ())) {

			guitarTouchCount++;
			if(guitarTouchCount % 2 == 1){
				selectedInstruments.Add (guitar.GetInstanceID (), guitar);
			}else{
				guitarAudioSource.Stop();
				selectedInstruments.Remove(guitar.GetInstanceID());
			}
		}
		if (collidees.ContainsKey (sax.GetInstanceID ())) {

			saxTouchCount++;
			if(saxTouchCount % 2 == 1){
				selectedInstruments.Add (sax.GetInstanceID (), sax);
			}else{
				saxAudioSource.Stop();
				selectedInstruments.Remove(sax.GetInstanceID());
			}
		}
		if (collidees.ContainsKey (flute.GetInstanceID ())) {

			fluteTouchCount++;
			if(fluteTouchCount % 2 == 1){
				selectedInstruments.Add (flute.GetInstanceID (), flute);
			}else{
				fluteAudioSource.Stop();
				selectedInstruments.Remove(flute.GetInstanceID());
			}
		}
		if (collidees.ContainsKey (drum.GetInstanceID ())) {

			drumTouchCount++;
			if(drumTouchCount % 2 == 1){
				selectedInstruments.Add (drum.GetInstanceID (), drum);
			}else{
				drumAudioSource.Stop();
				selectedInstruments.Remove(drum.GetInstanceID());
			}
		}

		// 3) 
		// fetch alle input tasten, die fuer den Kinect in Faast gut funktionieren
		if (Input.GetKeyDown ("g")) {
			celloPlay = true;
			violinPlay = true;
			guitaPlay = true;

		} else if (Input.GetKeyUp ("g")) {
			celloPlay = false;
			violinPlay = false;
			guitaPlay = false;
		}
		if (Input.GetKeyDown ("b")) {
			saxPlay = true;
			flutePlay = true;
			drumPlay = true;
		
		} else if (Input.GetKeyUp ("b")) {
			saxPlay = false;
			flutePlay = false;
			drumPlay = false;
		}

		// 4) 
		// bring damit alle selektierte instrumente zum singen - audio start
		if (celloPlay) {
			celloAudioSource.Play ();
			celloAudioSource.volume = 1.0f;
		} else {
			// 5) 
			// wenn lange nix mehr - instrum werden leiser -> audio leiser
			celloAudioSource.volume = celloAudioSource.volume- (volumeReductionFactor * Time.deltaTime);
		}

		if (violinPlay) {
			violinAudioSource.Play ();
			violinAudioSource.volume = 1.0f;
		} else {
			violinAudioSource.volume = violinAudioSource.volume- (volumeReductionFactor * Time.deltaTime);
		}

		if (guitaPlay) {
			guitarAudioSource.Play ();
			guitarAudioSource.volume = 1.0f;
		} else {
			guitarAudioSource.volume = guitarAudioSource.volume- (volumeReductionFactor * Time.deltaTime);
		}

		if (saxPlay) {
			saxAudioSource.Play();
			saxAudioSource.volume = 0.3f;
		} else {
			saxAudioSource.volume = saxAudioSource.volume- (volumeReductionFactor * Time.deltaTime);
		}

		if (flutePlay) {
			fluteAudioSource.Play();
			fluteAudioSource.volume = 1.0f;
		} else {
			fluteAudioSource.volume = fluteAudioSource.volume- (volumeReductionFactor * Time.deltaTime);
		}

		if (drumPlay) {
			drumAudioSource.Play();
			drumAudioSource.volume = 1.0f;
		} else {
			drumAudioSource.volume = drumAudioSource.volume- (volumeReductionFactor * Time.deltaTime);
		}
	
	}
}
