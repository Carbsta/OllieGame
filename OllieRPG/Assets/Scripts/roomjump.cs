using UnityEngine;
using System.Collections;

public class roomjump : MonoBehaviour {
	
	public Transform warpTarget;

	IEnumerator OnTriggerEnter2D(Collider2D other)
	{
		float fadeTimer = GameObject.Find("Game Manager").GetComponent<fader>().BeginFade (1);
		yield return new WaitForSeconds (fadeTimer);
		other.gameObject.transform.position = warpTarget.position;
		Camera.main.transform.position = warpTarget.position;
		GameObject.Find("Game Manager").GetComponent<fader>().BeginFade (-1);
	}
}
