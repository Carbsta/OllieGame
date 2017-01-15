using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class displaymessage : MonoBehaviour {

	[SerializeField]
	private Animator animator;

	public string[] messages;

	public Image presstext;

	private Interact interactionscript;

	void Start ()
	{
		interactionscript = GameObject.Find("Game Manager").GetComponent<Interact>();
		StartCoroutine(interactionscript.DistanceCheck(transform, presstext));
		StartCoroutine (interactionscript.KeyCheck ());
	}

	void Update ()
	{
		if(interactionscript.Interaction == true) {
			animator.SetBool ("Shown", true);
		}
	}

}
