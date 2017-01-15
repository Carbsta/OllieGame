using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Interact : MonoBehaviour {

	public Transform player;
	public float interactDistance = 0.2f;

	public bool interact = false;

	//public Image presstext;

	public bool Interaction = false;

	//void Start ()
	//{
	//	StartCoroutine(DistanceCheck(presstext));
	//	StartCoroutine (KeyCheck ());
	//}


	public IEnumerator DistanceCheck (Transform item, Image presstext)
	{
		while (Vector3.Distance (item.position, player.position) > interactDistance) 
		{
			presstext.enabled = false;
			yield return null;
			interact = false;
		}
		presstext.enabled = true;
		interact = true;
		yield return null;
		StartCoroutine (DistanceCheck (item, presstext));

	}

	public IEnumerator KeyCheck()
	{
		while (interact == true) 
		{
			if(Input.GetKeyDown(KeyCode.E)){
				print ("interacted");
				Interaction = true;
			}
			yield return null;
		}
		Interaction = false;
		yield return null;
		StartCoroutine (KeyCheck ());
	}




}
