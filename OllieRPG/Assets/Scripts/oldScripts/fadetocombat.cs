using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class fadetocombat : MonoBehaviour {

	public Image presstext;

	private Interact interactionscript;
	private fader fadingscript;

	void Start ()
	{
		interactionscript = GameObject.Find("Game Manager").GetComponent<Interact>();
		fadingscript = GameObject.Find ("Game Manager").GetComponent<fader> ();
		StartCoroutine(interactionscript.DistanceCheck(transform, presstext));
		StartCoroutine (interactionscript.KeyCheck ());
		StartCoroutine (action());
	}

	IEnumerator action()
	{
		if (interactionscript.Interaction == true) {
			StartCoroutine(fadingscript.SwitchToCombat ());
		}
		yield return null;
		StartCoroutine (action ());
	}
}
