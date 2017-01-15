using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class dialogue : MonoBehaviour {
	
	public IEnumerator ShowUI(Animator anim){
		anim.SetBool ("Shown", true);
		print ("show");
		yield return null;
	}

	void HideUI(Animator anim){
		anim.SetBool ("Shown", false);
	}

}
