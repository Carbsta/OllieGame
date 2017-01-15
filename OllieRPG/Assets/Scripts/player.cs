using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class player : MonoBehaviour {

	public LayerMask collisionLayer;
	private GameObject colliding;
	private string colltag;
	private bool isColliding = false;

	[SerializeField]
	private Animator ui;
	[SerializeField]
	private Text ui_text;


	[SerializeField]
	private fader Fadescript;
	[SerializeField]
	private CombatScript combat;



	[SerializeField]
	private SpriteRenderer render;

	[SerializeField]
	private Rigidbody2D rb2d;

	[SerializeField]
	private Collider2D playercollider;

	[SerializeField]
	private float tilescale = 1f;
//	[SerializeField]
//	private float holdtime = 0.5f;

	private int horizontal = 0;
	private int vertical = 0;

	//private Vector3 startPos;
	//private Vector3 endPos;

	private float walktime = 0f;
	[SerializeField]
	private float walkSpeed = 3f;
	private bool isMoving = false;
	private bool isLeftFoot = true;

	private Direction currentDirection;

	[Header("Sprites:")]

	[Header("Still:")]

	[SerializeField]
	private Sprite UpStill;
	[SerializeField]
	private Sprite DownStill;
	[SerializeField]
	private Sprite LeftStill;
	[SerializeField]
	private Sprite RightStill;

	[Header("LeftFoot:")]

	[SerializeField]
	private Sprite UpLeft;
	[SerializeField]
	private Sprite DownLeft;
	[SerializeField]
	private Sprite LeftLeft;
	[SerializeField]
	private Sprite RightLeft;

	[Header("RightFoot:")]

	[SerializeField]
	private Sprite UpRight;
	[SerializeField]
	private Sprite DownRight;
	[SerializeField]
	private Sprite LeftRight;
	[SerializeField]
	private Sprite RightRight;


	
	// Update is called once per frame
	void Update () {

		if(isColliding == true){
			return;
		}

		horizontal = (int) (Input.GetAxisRaw ("Horizontal"));//gets input and rounds it to an int

		vertical = (int) (Input.GetAxisRaw ("Vertical"));

		if (horizontal != 0)//we don't want diagonal movement, so this means both x and y can never = 1 at the same time, as y will be set to 0 if x is 1.
		{
			vertical = 0;
		}

		if (horizontal == 1) {
			currentDirection = Direction.Right;
		}
		if (horizontal == -1) {
			currentDirection = Direction.Left;
		}
		if (vertical == 1) {
			currentDirection = Direction.Up;
		}
		if (vertical == -1) {
			currentDirection = Direction.Down;
		}


		if((horizontal != 0 || vertical != 0) && (!isMoving))
		{
			AttemptMove (horizontal, vertical, currentDirection, isLeftFoot);
			isLeftFoot = !isLeftFoot;
		}

	
	}

	void AttemptMove(int _horizontal, int _vertical, Direction _currentDirection, bool _isLeftFoot)
	{
		RaycastHit2D hit;

		//Store start position to move from, based on objects current transform position.
		Vector2 start = rb2d.position;

		// Calculate end position based on the direction parameters passed in.
		Vector2 end = new Vector2 (start.x + (_horizontal * tilescale), start.y + (_vertical * tilescale));

		//Disable the boxCollider so that linecast doesn't hit this object's own collider.
		playercollider.enabled = false;

		//Cast a line from start point to end point checking collision on collisionLayer.
		hit = Physics2D.Linecast (start, end, collisionLayer);

		//Re-enable boxCollider after linecast
		playercollider.enabled = true;

		switch (_currentDirection) {
		case Direction.Up:
			render.sprite = UpStill;
			break;
		case Direction.Down:
			render.sprite = DownStill;
			break;
		case Direction.Left:
			render.sprite = LeftStill;
			break;
		case Direction.Right:
			render.sprite = RightStill;
			break;
		}


		//Check if anything was hit
		if (hit.transform == null) {
			StartCoroutine (Move (start, end, _currentDirection, _isLeftFoot));
		} else {
			if (isColliding == false) {
				isColliding = true;
				colliding = hit.transform.gameObject;
				colltag = colliding.tag;
				if (colltag == "Enviroment") {
					isColliding = false;
				} else if (colltag == "Box") {
					StartCoroutine (Fadescript.SwitchToCombat ());
					combat.StartCombat();
				} else if (colltag == "Dialogue") {
					StartCoroutine (Dialogue ());
				}
			}

		}
			
	}

	IEnumerator Dialogue(){
		ObjectMessages uitext = colliding.GetComponent<ObjectMessages>();
		ui.SetBool("Shown",true);
		yield return new WaitForSeconds (0.1f);
		foreach (message mes in uitext.messages) {
			if (mes.conditional == true) {
				ui_text.text = mes.text;
				yield return StartCoroutine (WaitForKeyDown (KeyCode.Space));
			}
		}
		ui.SetBool("Shown",false);
		yield return new WaitForSeconds (0.1f);
		isColliding = false;
	}

	IEnumerator WaitForKeyDown(KeyCode code){
		do
		{
			yield return null;
		} while (!Input.GetKeyDown(code));
	}
		
//	IEnumerator KeyHeld()
//	{
//		keyHeld = false;
//		float keytimer = 0f;
//		while (keytimer < 1f)
//		{
//			keytimer += Time.deltaTime * holdtime;
//			yield return null;
//		}
//		keyHeld = true;
//		yield return 0;
//	}

	IEnumerator Move(Vector2 startPos, Vector2 endPos, Direction finalDirection, bool finalIsLeftFoot)
	{
		isMoving = true;
		startPos = rb2d.position;
		walktime = 0f;
		//endPos = new Vector3 (startPos.x + (_horizontal * tilescale), startPos.y + (_vertical * tilescale), startPos.z);

		if (finalIsLeftFoot) {
			switch (finalDirection) {
			case Direction.Up:
				render.sprite = UpLeft;
				break;
			case Direction.Down:
				render.sprite = DownLeft;
				break;
			case Direction.Left:
				render.sprite = LeftLeft;
				break;
			case Direction.Right:
				render.sprite = RightLeft;
				break;
			}
		} else 
		{
			switch (finalDirection) {
			case Direction.Up:
				render.sprite = UpRight;
				break;
			case Direction.Down:
				render.sprite = DownRight;
				break;
			case Direction.Left:
				render.sprite = LeftRight;
				break;
			case Direction.Right:
				render.sprite = RightRight;
				break;
			}
		}


		while (walktime < 1f) 
		{
			walktime += Time.deltaTime * walkSpeed;
			rb2d.position = Vector2.Lerp (startPos, endPos, walktime);
			yield return null;
		}

		switch (finalDirection) {
		case Direction.Up:
			render.sprite = UpStill;
			break;
		case Direction.Down:
			render.sprite = DownStill;
			break;
		case Direction.Left:
			render.sprite = LeftStill;
			break;
		case Direction.Right:
			render.sprite = RightStill;
			break;
		}

		//yield return new WaitForSeconds(0.1f);
		isMoving = false;
		yield return 0;

	}

}
enum Direction 
{
	Up,
	Down,
	Left,
	Right
}

