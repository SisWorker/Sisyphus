using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	public int speed;
	public float x;
	public float y;
	public int jumpForce;
	public Rigidbody2D Rock;

	private Rigidbody2D Rbody;
	private Animator animator;

	//private Vector3 antiGravity;
	//private bool canJump;

	private bool onGround;
	private bool contactRock;

	private Vector3 groundEulerAngle;
	private Vector3 move;


	// Use this for initialization
	void Start () 
	{
		Rbody = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator>();
		x = 0.1f;
		y = -0.0f;
		onGround = false;
		contactRock = false;

	}




	// Update is called once per frame
	void FixedUpdate () 
	{

		animator.SetBool ("PushRock", false);
		//Rbody.transform.rotation = Quaternion.identity;
		Vector3 jump = new Vector3 (0.0f, jumpForce, 0.0f);
		//Debug.Log ("angle:"+ groundEulerAngle.z);


//	Freeze player if on slope
		/*if (groundEulerAngle.z != 0.0f) 
		{
			Rbody.velocity = new Vector3 (0.0f, 0.0f, 0.0f);
			Debug.Log ("OnSlope");
		}*/


//  player jump when key is pressed and is on ground.
		if ((Input.GetAxis ("Jump") != 0) & ((onGround)||(contactRock)))
		{
			float xSpeed= Rbody.velocity.x;
			Rbody.velocity = new Vector3(xSpeed,jumpForce,0.0f);
			Debug.Log ("jump");
		}

		float ySpeed = Rbody.velocity.y;
		float moveHorizontal = Input.GetAxis ("Horizontal");


//	Move according to gound angle when is grounded

		/*if (onGround)
		{
			move = new Vector3 ((moveHorizontal * speed), (moveHorizontal * (Mathf.Sin (groundEulerAngle.z)) * speed),0.0f);

		}*/

		//move normally when is in air

		move = new Vector3 ((moveHorizontal * speed),(ySpeed),0.0f);
		Rbody.velocity = move;

//	Animation change according to action
		if (moveHorizontal!=0.0f)
			animator.SetBool("Walking", true);
		else
			animator.SetBool("Walking",false);
	}




	// when colliding with other objects, change state bools
	void OnTriggerStay2D(Collider2D other)
	{

		if (other.gameObject.CompareTag ("Ground")) 
		{
			onGround = true;
			groundEulerAngle = other.transform.eulerAngles;
			//print(groundEulerAngle);
		}
		if (other.gameObject.CompareTag ("RockContact"))
		{	
			if (onGround)
			{
				animator.SetBool ("PushRock", true);
			}
			contactRock = true;
			Debug.Log ("contact");
		}
	}

	// change state bools back when leaving other objects
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("Ground"))
		{
			onGround = false;
			groundEulerAngle = new Vector3(0.0f,0.0f,0.0f);
			print (groundEulerAngle);
		}

		if (other.gameObject.CompareTag ("RockContact"))
		{
			contactRock=false;
		}

	}
		
}
