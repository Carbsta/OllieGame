using UnityEngine;
using System.Collections;

public class playermovement : MonoBehaviour {

	Rigidbody2D rb2d;
	Animator anim;
	public float speed;


	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D>();
		//anim = GetComponent<Animator>();

	}

	// Update is called once per frame
	void Update () {
		Vector2 moveVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

//		if (moveVector != Vector2.zero)
//		{
//			anim.SetBool("isWalking", true);
//			anim.SetFloat("float_x", moveVector.x);
//			anim.SetFloat("float_y", moveVector.y);
//		}
//		else
//		{
//			anim.SetBool("isWalking", false);
//		}

		rb2d.MovePosition(rb2d.position + moveVector * Time.deltaTime * speed);
	}
}