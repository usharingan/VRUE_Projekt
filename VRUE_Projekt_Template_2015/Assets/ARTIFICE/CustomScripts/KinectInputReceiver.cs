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

	GameObject orchestra;

	Hashtable selectedInstruments;

	AudioSource celloAudioSource, violinAudioSource, guitarAudioSource,
	saxAudioSource, fluteAudioSource, drumAudioSource;

/*	int celloTouchCount, violinTouchCount, guitarTouchCount,
	saxTouchCount, fluteTouchCount, drumTouchCount;
*/
	bool celloCurrentlyTouched, violinCurrentlyTouched, guitarCurrentlyTouched,
	saxCurrentlyTouched, fluteCurrentlyTouched, drumCurrentlyTouched;

	bool celloPrevTouched, violinPrevTouched, guitarPrevTouched,
	saxPrevTouched, flutePrevTouched, drumPrevTouched;

	bool celloSelected, violinSelected, guitarSelected,
	saxSelected, fluteSelected, drumSelected;

	bool celloPlay, violinPlay, guitaPlay,
	saxPlay, flutePlay, drumPlay;

	float volumeReductionFactor;

	//TEST
	int countUpdates;


	// Use this for initialization
	void Start () {
		// 1) hol dir alle instrumente
		// g-instruments -> play when g is pressed
//		cello = GameObject.Find("Cello"); 
//		violin = GameObject.Find("Violin"); 
		guitar = GameObject.Find("Guitar"); 
		// b-instruments -> play when b is pressed
//		sax = GameObject.Find("Sax"); 
//		flute  = GameObject.Find("Flute"); 
//		drum = GameObject.Find("Drum"); 
	


		selectedInstruments = new Hashtable();

		//audioSources
//		celloAudioSource = cello.GetComponent<AudioSource> ();
//		violinAudioSource = violin.GetComponent<AudioSource> (); 
		guitarAudioSource = guitar.GetComponent<AudioSource> ();
//		saxAudioSource = sax.GetComponent<AudioSource> ();
//		fluteAudioSource = flute.GetComponent<AudioSource> ();
//		drumAudioSource = drum.GetComponent<AudioSource> ();

		// touch counters
		// wenn gerade Zahl -> not in selected Instruments hashtable
		// wenn ungerade Zahl -> in selected Instruments hashtable
/*		celloTouchCount = 0;
		violinTouchCount = 0;
		guitarTouchCount = 0;
		saxTouchCount = 0;
		fluteTouchCount = 0;
		drumTouchCount = 0;
*/
		// previously touched bools
		celloPrevTouched = false;
		violinPrevTouched = false;
		guitarPrevTouched = false;
		saxPrevTouched = false;
		flutePrevTouched = false;
		drumPrevTouched = false;

		// currently touched bools
		celloCurrentlyTouched = false;
		violinCurrentlyTouched = false;
		guitarCurrentlyTouched = false;
		saxCurrentlyTouched = false;
		fluteCurrentlyTouched = false;
		drumCurrentlyTouched = false;

		// selected bools
		celloSelected = false;
		violinSelected = false; 
		guitarSelected = false;
		saxSelected = false;
		fluteSelected = false;
		drumSelected = false;

		//play bools
		celloPlay = false;
		violinPlay = false;
		guitaPlay = false;
		saxPlay = false; 
		flutePlay = false;
		drumPlay = false;

		volumeReductionFactor = 1;

		//TEST
		countUpdates = 0;

	}
	
	// Update is called once per frame
	void Update () {

		// TEST 2
		orchestra = GameObject.Find ("Orchestra");

		if (orchestra == null) {
			Debug.Log ("Waiting for Orchestra to exist...");
		} else {
			AudioSource[] soundsOfAllinstruments = orchestra.GetComponentsInChildren<AudioSource>();

			// TASTENABFRAGE
			// fetch alle input tasten, die fuer den Kinect in Faast gut funktionieren

	//		if (Input.GetKeyDown (/*"g"*/KeyCode.G)) {

	//		if (Input.GetButtonDown ("gPressed")) {

			//if(Input.GetKey("g")){
			if(Input.GetKeyUp("g")){

				Debug.Log ("PRESSED G");

				celloPlay = true;
				violinPlay = true;
				guitaPlay = true;
				
			}/* else if (Input.GetKeyUp ("g")) {

				Debug.Log ("UN-PRESSED G");

				celloPlay = false;
				violinPlay = false;
				guitaPlay = false;
			}
			if (Input.GetButtonDown ("bPressed")) {

				Debug.Log ("PRESSED B");

				saxPlay = true;
				flutePlay = true;
				drumPlay = true;
				
			} else if (Input.GetKeyUp ("b")) {

				Debug.Log ("UN-PRESSED B");

				saxPlay = false;
				flutePlay = false;
				drumPlay = false;
			}*/
			
			for(int i = 0; i < soundsOfAllinstruments.Length; i++){

				GameObject parent = soundsOfAllinstruments[i].gameObject;
	/*			if(string.Compare(parent.name, "Guitar") && soundsOfAllinstruments[i].enabled){
					if (guitaPlay) {
						guitarAudioSource.Play ();
						guitarAudioSource.volume = 1.0f;
					} else {
						guitarAudioSource.volume = guitarAudioSource.volume- (volumeReductionFactor * Time.deltaTime);
					}
				}
				if(string.Compare(parent.name, "Guitar") && soundsOfAllinstruments[i].enabled){
					if (guitaPlay) {
						guitarAudioSource.Play ();
						guitarAudioSource.volume = 1.0f;
					} else {
						guitarAudioSource.volume = guitarAudioSource.volume- (volumeReductionFactor * Time.deltaTime);
					}
				} */
				if((string.Compare(parent.name, "Guitar") == 0) && (soundsOfAllinstruments[i].enabled)){
					if (guitaPlay) {
						guitarAudioSource.Play ();

						Debug.Log ("GUITAR PLAY");

						guitarAudioSource.volume = 1.0f;

						Debug.Log ("GUITAR VOLUME 1");

					} else {

						Debug.Log ("GUITAR SLOWLY LEISER");

						guitarAudioSource.volume = guitarAudioSource.volume- (volumeReductionFactor * Time.deltaTime);
					}
				}
	/*			if(string.Compare(parent.name, "Guitar") && soundsOfAllinstruments[i].enabled){
					if (guitaPlay) {
						guitarAudioSource.Play ();
						guitarAudioSource.volume = 1.0f;
					} else {
						guitarAudioSource.volume = guitarAudioSource.volume- (volumeReductionFactor * Time.deltaTime);
					}
				}
				if(string.Compare(parent.name, "Guitar") && soundsOfAllinstruments[i].enabled){
					if (guitaPlay) {
						guitarAudioSource.Play ();
						guitarAudioSource.volume = 1.0f;
					} else {
						guitarAudioSource.volume = guitarAudioSource.volume- (volumeReductionFactor * Time.deltaTime);
					}
				}
				if(string.Compare(parent.name, "Guitar") && soundsOfAllinstruments[i].enabled){
					if (guitaPlay) {
						guitarAudioSource.Play ();
						guitarAudioSource.volume = 1.0f;
					} else {
						guitarAudioSource.volume = guitarAudioSource.volume- (volumeReductionFactor * Time.deltaTime);
					}
				}  */

			}   // END FOR



		}   // END ELSE


		
	}  // END UPDATE

}