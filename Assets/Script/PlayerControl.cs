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
	private bool onSlope;

	private bool facingRight;

	private int speedLog;

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
		onSlope = false;
		facingRight = false;

		Vector3 jump = new Vector3 (0.0f, jumpForce, 0.0f);
		speedLog = speed;

	}



	// Update is called once per frame
	void FixedUpdate () 
	{

		animator.SetBool ("PushRock", false);


	 //  player jump when key is pressed and is on ground.
		if ((Input.GetAxis ("Jump") != 0) & ((onGround) || (contactRock))) 
		{
			float xSpeed = Rbody.velocity.x;
			Rbody.velocity = new Vector3 (xSpeed, jumpForce, 0.0f);
		}

		//get move input
		float ySpeed = Rbody.velocity.y;
		float moveHorizontal = Input.GetAxis ("Horizontal");

		move = new Vector3 ((moveHorizontal)*speed,(ySpeed),0.0f);


		//flip accoring to move direction
		if (moveHorizontal > 0 && !facingRight)
			Flip ();
		else if (moveHorizontal < 0 && facingRight)
			Flip ();

		if (onGround) 
		{
			int myLayer = 1 << 8;
			Vector2 origin= new Vector2 (transform.position.x, (transform.position.y-1.1f));
			Vector2 Direction= new Vector2(moveHorizontal, 0.0f);

			//Ray2D myRay=new Ray2D(origin,Direction);

			Debug.DrawRay(new Vector3 (transform.position.x, (transform.position.y-1.1f),0.1f), new Vector3(moveHorizontal*10f,0.0f,0.0f));



			//go down 30 degree when going down a slope;
			if (onSlope)
			{
				bool ray=Physics2D.Raycast(origin,Direction, 10f,myLayer);
				Debug.Log(ray);
				if (( !ray) && (moveHorizontal != 0))
				{
					move = new Vector3((moveHorizontal*speed/1.16f),(-(0.75f)*speed*0.3f+ySpeed), 0.0f);

					//Debug.Log(hit.collider.tag);
					Debug.Log ("going down");
				}
				else 
					move = new Vector3((moveHorizontal*speed/1.16f),ySpeed,0.0f);

			}  

		}

		if (moveHorizontal!=0.0f)
			animator.SetBool("Walking", true);
		else
			animator.SetBool("Walking",false);

		//move=move.normalized;
		Rbody.velocity = move;

	}




	// when colliding with other objects, change state bools
	void OnTriggerStay2D(Collider2D other)
	{

		if (other.gameObject.layer == 8) 
		{
			onGround = true;

			if (other.gameObject.CompareTag ("Slope"))
				onSlope=true;

		}
		if (other.gameObject.CompareTag ("RockContact"))
		{	
			if (onGround)
			{
				animator.SetBool ("PushRock", true);
				speed=speedLog/8*5;
			}
			//if stand on stone
			else
				//Rock.constraints=RigidbodyConstraints2D.FreezeRotation;

			contactRock = true;

		
		}
	}

	// change state bools back when leaving other objects
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.layer == 8)
		{
			onGround = false;
			onSlope=false;

		}

		if (other.gameObject.CompareTag ("RockContact"))
		{
			contactRock=false;
			speed=speedLog;
		}

	}

	//flip the sprite
	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}	
}
