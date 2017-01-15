using UnityEngine;
using System.Collections;

public class cameramovement : MonoBehaviour {

	public Transform target;
	public float camSpeed = 0.01f;
	Camera mainCam;

	// Use this for initialization
	void Start () {
		this.mainCam = this.GetComponent<Camera>();
	}

	// Update is called once per frame
	void Update () {

		mainCam.orthographicSize = (Screen.height / 100f) / 16f;

		if (target)
		{
			transform.position = Vector3.Lerp(transform.position, target.position, camSpeed) + new Vector3(0, 0, -10);
		}
	}
}
