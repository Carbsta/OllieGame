using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Interaction : MonoBehaviour {

	public Image presstext;

	void OnTriggerEnter2d(Collider other)
	{
		presstext.enabled = true;
		print ("triggered");
	}
}
