﻿using UnityEngine;
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
	private bool canJump;

	private int speedLog;
	private float moveHorizontal;
	private float xSpeed;
	private float ySpeed;

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

		// detetermine whether is onground and whether canjump
			IsOnGround ();

		//get move input
		ySpeed = Rbody.velocity.y;
		xSpeed = Rbody.velocity.x;
        moveHorizontal = Input.GetAxis ("Horizontal");
		move = new Vector3 ((moveHorizontal)*speed,(ySpeed),0.0f);

		//flip accoring to move direction
		if ((moveHorizontal > 0 && !facingRight)||(moveHorizontal < 0 && facingRight) ){
			Flip ();
		}
		if (onGround) 
		{
			//determine whether to push rock;
			PushRock();

			// operate wheel if has the input and is onground.
			if (Input.GetAxis ("Interact") != 0)
			{
				if (withWheel)
					Operating=true;
			}

			//go down 30 degree when going down a slope;
			if (onSlope)
			{
				OnSlopeMovement();
			}  
		}

		Jump ();

		SetAnimation ();

		//move=move.normalized;
		Rbody.velocity = move;


	}

	void OnSlopeMovement()
	{
		int myLayer = 1 << 11;
		Vector2 origin= new Vector2 (transform.position.x, (transform.position.y-1.1f));
		Vector2 Direction= new Vector2(moveHorizontal, 0.0f);
		//Debug.DrawRay(new Vector3 (transform.position.x, (transform.position.y-1.1f),0.1f), new Vector3(moveHorizontal*5f,0.0f,0.0f));
		bool ray=Physics2D.Raycast(origin,Direction, 5f,myLayer);
		if (( !ray) && (moveHorizontal != 0))
		{
			move = new Vector3((moveHorizontal*speed/1.15f),(-(0.75f)*speed*0.3f+ySpeed), 0.0f);
		}
		else 
			move = new Vector3((moveHorizontal*speed/1.15f),ySpeed,0.0f);
	}


	void IsOnGround()
	{
		onGround = false;
		pushing = false;
		//use raycast to determine whether is onGruond
		//use two raycasts to assure accuracy

		Vector2 origin1= new Vector2 ((transform.position.x-0.4f), (transform.position.y-1.3f));
		Vector2 origin2= new Vector2 ((transform.position.x+0.4f), (transform.position.y-1.3f));
		Vector2 Down= new Vector2(0.0f, -0.5f);
		Debug.DrawRay(new Vector3 ((transform.position.x-0.4f), (transform.position.y-1.3f),0.1f), new Vector3 (0f,(-0.5f),0f));
		Debug.DrawRay(new Vector3 ((transform.position.x+0.4f), (transform.position.y-1.3f),0.1f), new Vector3 (0f,(-0.5f),0f));
		
		int playerLayer = 1 << 9;
		playerLayer = ~playerLayer;
		int GroundLayer = 1 << 11;
		
		RaycastHit2D hit1;
		RaycastHit2D hit2;
		
		hit1=Physics2D.Raycast(origin1, Down , -0.5f, playerLayer);
		hit2=Physics2D.Raycast(origin2, Down , -0.5f, playerLayer);
		canJump = hit1 || hit2;

		//hit = Physics2D.Raycast (new Vector2 (transform.position.x, (transform.position.y-1.35f)), Down, -0.9f, GroundLayer);
		//Debug.DrawRay(new Vector3 ((transform.position.x), (transform.position.y-1.35f),0.1f), new Vector3 (0f,(-0.9f),0f));
		
		//Debug.Log ("onground:"+onGround);
		if (canJump) 
		{
			//Debug.Log("hit");
			if (hit1)
			{
				if (hit1.collider.gameObject.layer == 11) 
				{
					onGround = true;
				}
			}
			if (hit2)
			{
				if (hit2.collider.gameObject.layer == 11) 
				{
					onGround = true;
				}
			}
		}
	}


	void Jump()

	{
		//  player jump when key is pressed and is on ground or on rock.
		if ((Input.GetAxis ("Jump") != 0) && (canJump)) 
		{

			//cannot jump while pushing 
			if (!pushing)
			{
				xSpeed = Rbody.velocity.x;
				move = new Vector3 (xSpeed, jumpForce, 0.0f);
			}
		}
	}


	void PushRock()
	{


		Vector2 Direction= new Vector2(moveHorizontal, 0.0f);
		int RockLayer = 1 << 10;
		RaycastHit2D hitRock = Physics2D.Raycast (new Vector2 ((transform.position.x + (0.5f * moveHorizontal)), (transform.position.y + 0.6f)), Direction, 1f, RockLayer);
		Debug.DrawRay (new Vector3 ((transform.position.x + (0.5f * moveHorizontal)), (transform.position.y + 0.6f), 0.1f), new Vector3 (moveHorizontal * 1f, 0.0f, 0.0f));
		
		if (hitRock) 
		{
			pushing = true;
		}
	}


	void SetAnimation()
	{
		if (pushing == false) 
		{
			if (moveHorizontal!=0.0f)
				animator.SetBool("Walking", true);
			else
				animator.SetBool("Walking",false);
		}
		animator.SetBool ("PushRock", pushing);
	}


	//flip the sprite
	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.position = new Vector3(transform.position.x,transform.position.y+0.01f,transform.position.z);
		transform.localScale = theScale;
	}	

	void OnTriggerEnter2D(Collider2D other){
	}
	// when colliding with other objects, change state bools
	void OnTriggerStay2D(Collider2D other)
	{
		
		if (other.gameObject.layer == 11) 
		{	
			//PlatformGround
			if(Input.GetAxis ("Vertical")<0)
			{
				
				other.gameObject.layer = 8;
			}
			if (other.gameObject.CompareTag ("Slope"))
			{
				onSlope=true;
			}


		}

		if (other.gameObject.CompareTag ("RockContact"))
		{	
			if (onGround)
			{
				speed=speedLog/8*5;
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
		if (other.gameObject.layer == 11)
		{
			onSlope=false;
		}
		
		if (other.gameObject.CompareTag ("RockContact"))
		{
			contactRock=false;
			speed=speedLog;
		}
		
		if (other.gameObject.CompareTag ("Wheel"))
			withWheel = false;
		
		if (other.gameObject.CompareTag ("MovingPlatform"))
		{
			transform.parent = null;
		}

	}
}
