using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	public int speed;
	public int jumpForce;
	public Rigidbody2D Rock;
	private Rigidbody2D Rbody;
	private Animator animator;

	public bool Operating;

	//private Vector3 antiGravity;
	//private bool canJump;

	private bool onGround;
	private bool contactRock;
	private bool onSlope;
	private bool withWheel;
	private bool withPlatformController;
	private bool pushing;
	private bool facingRight;

	private int speedLog;

	private Vector3 groundEulerAngle;
	private Vector3 move;
	private RaycastHit2D hit;


	// Use this for initialization
	void Start () 
	{
		Rbody = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator>();
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
		Operating = false;

		// detetermine whether to jump
		Jump ();


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
			//Debug.DrawRay(new Vector3 (transform.position.x, (transform.position.y-1.1f),0.1f), new Vector3(moveHorizontal*5f,0.0f,0.0f));

			if (Input.GetAxis ("Interact") != 0)

			{
				//Debug.Log("operating");

				if (withWheel)
					Operating=true;
			}



			//go down 30 degree when going down a slope;
			if (onSlope)
			{
				bool ray=Physics2D.Raycast(origin,Direction, 5f,myLayer);
				//Debug.Log(ray);
				if (( !ray) && (moveHorizontal != 0))
				{
					move = new Vector3((moveHorizontal*speed/1.1f),(-(0.75f)*speed*0.3f+ySpeed), 0.0f);

					//Debug.Log(hit.collider.tag);
					//Debug.Log ("going down");
				}
				else 
					move = new Vector3((moveHorizontal*speed/1.1f),ySpeed,0.0f);

			}  

		}

		if (moveHorizontal!=0.0f)
			animator.SetBool("Walking", true);
		else
			animator.SetBool("Walking",false);

		//move=move.normalized;
		Rbody.velocity = move;

	}



	void Jump()
		//use raycast to determine whether is onGruond
	{
		//use two raycasts to assure accuracy
		Vector2 origin1= new Vector2 ((transform.position.x-0.4f), (transform.position.y-1.3f));
		Vector2 origin2= new Vector2 ((transform.position.x+0.4f), (transform.position.y-1.3f));
		Vector2 Down= new Vector2(0.0f, -0.5f);
		Debug.DrawRay(new Vector3 ((transform.position.x-0.4f), (transform.position.y-1.3f),0.1f), new Vector3 (0f,(-0.5f),0f));
		Debug.DrawRay(new Vector3 ((transform.position.x+0.4f), (transform.position.y-1.3f),0.1f), new Vector3 (0f,(-0.5f),0f));


		int playerLayer = 1 << 9;
		playerLayer = ~playerLayer;
		int GroundLayer = 1 << 8;

		RaycastHit2D hit1;
		RaycastHit2D hit2;

		hit1=Physics2D.Raycast(origin1, Down , -0.5f, playerLayer);
		hit2=Physics2D.Raycast(origin2, Down , -0.5f, playerLayer);
		bool canJump = hit1 || hit2;

		//hit = Physics2D.Raycast (new Vector2 (transform.position.x, (transform.position.y-1.35f)), Down, -0.9f, GroundLayer);
		//Debug.DrawRay(new Vector3 ((transform.position.x), (transform.position.y-1.35f),0.1f), new Vector3 (0f,(-0.9f),0f));

		//Debug.Log ("onground:"+onGround);

		if (canJump) 
		{
			Debug.Log("hit");
			if (hit1)
			{
				if (hit1.collider.gameObject.layer == 8) 
				{
					onGround = true;
				}
			}
			if (hit2)
			{
				if (hit2.collider.gameObject.layer == 8) 
				{
					onGround = true;
				}
			}

		}

		//Debug.Log (pushing);
		//  player jump when key is pressed and is on ground or on rock.
		if ((Input.GetAxis ("Jump") != 0) && (canJump)) 
		{
			//cannot jump while pushing 
			if (!pushing)
			{
				float xSpeed = Rbody.velocity.x;
				Rbody.velocity = new Vector3 (xSpeed, jumpForce, 0.0f);
			}
		}
	}



	// when colliding with other objects, change state bools
	void OnTriggerStay2D(Collider2D other)
	{

		if (other.gameObject.layer == 8) 
		{
			//onGround = true;

			if (other.gameObject.CompareTag ("Slope"))
				onSlope=true;

		}
		if (other.gameObject.CompareTag ("RockContact"))
		{	
			if (onGround)
			{
				animator.SetBool ("PushRock", true);
				speed=speedLog/8*5;
				pushing=true;
			}
			//if stand on stone

			contactRock = true;

		}

		if (other.gameObject.CompareTag ("Wheel")) 
		{
			withWheel = true;
			//Debug.Log ("withWheel");
		}
		if (other.gameObject.CompareTag ("PlatformController")) 
		{
			if (Input.GetAxis ("Interact") != 0)

			{
				other.gameObject.GetComponentInParent<MovingPlatform>().MoveOn ();

			}
		}
		if (other.gameObject.CompareTag ("MovingPlatform"))
		{
			transform.parent = other.transform;
		}
	
					
			
	}

	// change state bools back when leaving other objects
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.layer == 8)
		{
			onGround = false;
			onSlope=false;
			pushing=false;
			animator.SetBool ("PushRock", false);
		}

		if (other.gameObject.CompareTag ("RockContact"))
		{
			contactRock=false;
			speed=speedLog;
			pushing=false;
			animator.SetBool ("PushRock", false);
		}

		if (other.gameObject.CompareTag ("Wheel"))
			withWheel = false;

		if (other.gameObject.CompareTag ("MovingPlatform"))
		{
			transform.parent = null;
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
