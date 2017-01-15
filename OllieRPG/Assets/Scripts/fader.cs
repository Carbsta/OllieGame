using UnityEngine;
using System.Collections;

//script for changing between the player camera and the combat camera, with a fade to black inbetween
public class fader : MonoBehaviour {

	public Camera MainCamera;
	public Camera CombatCamera;

	public Texture2D FadeOutTexture; //The texture which will be set to a black image in the editor
	public float FadeSpeed = 0.8f; //the speed at which the transition between the black screen and camera will happen

	private int drawDepth = -1000; //The depth at which the texture is drawn, -1000 will be above everything else
	private float alpha = 1.0f; //The alpha value of the texture, 1 is opaque and 0 is clear.
	private int fadeDir = -1; //-1 = fade in (black to clear), 1 = fade out (clear to black)


	void OnGUI(){ //OnGUI updates the gui everytime there is an action, we use it to draw the texture over the screen.
		alpha += fadeDir * FadeSpeed * Time.deltaTime;//time.delta.time used to convert it to seconds rather than per frame.
		//this changes the alpha value by +1 or -1 multiplied by 0.8 and The time in seconds it took to complete the last frame.
		alpha = Mathf.Clamp01 (alpha);
		//This line forces the aplha value to between 0 and 1, Clamp is one of the common maths functions included in Mathf - a struct in the Unity Engine.

		//Below we set the alpha value of the texture to the new alpha value (the rgb stays the same) and draw the texture
		GUI.color = new Color (GUI.color.r, GUI.color.b, GUI.color.g, alpha);
		GUI.depth = drawDepth;//sets the depth to draw at to our draw depth
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), FadeOutTexture);
		//Draws the texture to start at 0,0 and fill the screen 
	}

	//this is a public funtion that sets the fade direction and returns the fade speed as a float so other scripts can use it. It takes the parameter direction.
	public float BeginFade (int direction) {
		fadeDir = direction; //sets the fade direction
		return (FadeSpeed); //returns our float Fade Speed
	}

	public IEnumerator SwitchToCombat(){
		BeginFade(1);
		yield return new WaitForSeconds (FadeSpeed);
		MainCamera.enabled = false; //disables the main camera
		CombatCamera.enabled = true; //enables the camera which views the combat screen.
		BeginFade(-1);
	}

}
