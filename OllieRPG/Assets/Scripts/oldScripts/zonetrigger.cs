using UnityEngine;
using System.Collections;

public class zonetrigger : MonoBehaviour {

	public Camera MainCamera;
	public Camera CombatCamera;

	IEnumerator OnTriggerEnter2D (Collider2D other){
		//have to use an IEnumerator as a function that returns void cannot contain blocking (the yield line is a blocking line)
		float fadeTimer = GameObject.Find("Game Manager").GetComponent<fader>().BeginFade (1); 
		//calls the BeginFade function and sets the fade direction to 1 to fade out to black
		yield return new WaitForSeconds (fadeTimer); //Yield waits for fadespeed to be returned.
		MainCamera.enabled = false; //disables the main camera
		CombatCamera.enabled = true; //enables the camera which views the combat screen.
		GameObject.Find("Game Manager").GetComponent<fader>().BeginFade (-1); //sets the fade direction to -1 to fade in the camera view from black.
	}
}
