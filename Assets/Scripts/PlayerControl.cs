 using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	public int speed;
	public int jumpForce;
	private Rigidbody2D Rbody;
	private Animator animator;

	public bool Operating;

	//pick up object related variables

	public bool pickingUp;
	private bool withAPickUpObj;
	public bool canInteract;
	private float pickUpCounter;
	public bool holdingSomething;
	public bool canHoldMore;

	//private Vector3 antiGravity;
	//private bool canJump;

	private bool onGround;
	private bool onSlope;
	private bool withWheel;
	private bool withPlatformController;
	private bool pushing;
	public bool facingRight;
	private bool canJump;
	private bool jumping;
	private bool onRock;

	private int speedLog;
	private float moveHorizontal;
	private float xSpeed;
	private float ySpeed;

	private float downPressing;
	private float jumpPressed;
	private float jumpPressing;
	private int jumpsAvailable;

	private Vector3 groundEulerAngle;
	private Vector3 move;
	private RaycastHit2D hit;

	// Use this for initialization
	void Start () 
	{
		Rbody = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator>();
		onGround = false;
		onSlope = false;
		facingRight = false;
		speedLog = speed;
		jumpsAvailable = 1;
		pickingUp = false;
		canInteract = true;
		canHoldMore = true;
	}
	

	// Update is called once per frame
	void Update () 
	{
		//Debug.Log (1 / Time.deltaTime);

		Operating = false;

		// detetermine whether is onground and whether canjump
		IsOnGround ();
		KeyCheck ();
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
			if (Input.GetAxis ("Interact") != 0&&!pickingUp)
			{
				if (withWheel&&!withAPickUpObj)
					Operating=true;
				canInteract = false;
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

		PickUpTimer (0.5f);
	



	}
	void KeyCheck()
	{
		//go down
		if (Input.GetAxis ("Vertical") < 0&&onGround) {
			downPressing += 0.02f;
		} 
		else
		{
			downPressing = 0;
		}
		if (downPressing > 0.52f) {
			downPressing = 0;
		}

		//jump
		if (Input.GetAxis ("Jump")==0 && (onGround||onRock)) {
			jumpsAvailable = 1;
	
		}

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


	void PickUpTimer(float time)
	{
		if (!canInteract) 
		{	
			pickUpCounter += 0.02f;
		}
		if (pickUpCounter >= time) 
		{
			canInteract = true;
			pickUpCounter = 0f;
		}

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
		//int GroundLayer = 1 << 11;
		
		RaycastHit2D hit1;
		RaycastHit2D hit2;

		
		hit1=Physics2D.Raycast(origin1, Down , -0.5f, playerLayer);
		hit2=Physics2D.Raycast(origin2, Down , -0.5f, playerLayer);
		canJump = hit1 || hit2;

		if (hit1.collider != null) {
			onRock = hit1.collider.gameObject.layer == 10 ? true : false;
		}
		if (hit2.collider != null) {
			onRock = hit2.collider.gameObject.layer == 10 ? true : false;
		}
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
		if (Input.GetAxis("Jump")!=0) 
		{
			//Debug.Log ("pushing:"+pushing);
			//Debug.Log ("jumpsAvailable:"+jumpsAvailable);
			//Debug.Log ("canJump:"+canJump);
			//Debug.Log ("onground:"+onGround);
			//cannot jump while pushing 
			animator.SetBool("Jump",true);
			if (!pushing&&jumpsAvailable>0&&canJump)
			{
				xSpeed = Rbody.velocity.x;
				move = new Vector3 (xSpeed, jumpForce, 0.0f);

				jumpsAvailable -= 1;
				jumpPressing = 0;
				//lastJump = Time.fixedTime;
			}
			else if(!onGround)
			{
				jumpPressing += 0.02f;
				if(jumpPressing>0.1f&&jumpPressing<0.4f&&Rbody.velocity.y>0)
				{
					move = new Vector3(xSpeed,ySpeed+0.59f-jumpPressing,0.0f);
				}
			}
		}
	}


	void PushRock()
	{


		Vector2 Direction= new Vector2(moveHorizontal, 0.0f);
		int RockLayer = 1 << 10;
		RaycastHit2D hitRock = Physics2D.Raycast (new Vector2 ((transform.position.x + (0.5f * moveHorizontal)), (transform.position.y + 0.6f)), Direction, 1f, RockLayer);
		Debug.DrawRay (new Vector3 ((transform.position.x + (0.5f * moveHorizontal)), (transform.position.y + 0.6f), 0.1f), new Vector3 (moveHorizontal * 1f, 0.0f, 0.0f));
		
		if (hitRock&&!pickingUp) 
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
		if (onGround||onRock) {
			animator.SetBool ("Jump", false);
		} 


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

		if (other.gameObject.CompareTag ("PickUp")) 
		{
			withAPickUpObj = true;
			if(Input.GetAxis("Interact")!=0&&canInteract&&!pickingUp)
			{
				canInteract = false;
				pickingUp = true;

			}
		
			if(Input.GetAxis("Interact")!=0&&canInteract&&pickingUp)
			{
				pickingUp = false;
				canInteract = false;
				canHoldMore = true;
			}
		
		}



		if (other.gameObject.layer == 11) 
		{	
			//PlatformGround
			if(downPressing>0.5f)
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
			onRock = true;
			if (onGround)
			{
				speed=speedLog/8*5;
			}
		}
		
		if (other.gameObject.CompareTag ("Wheel")) 
		{
			withWheel = true;
			//Debug.Log ("withWheel");
		}
		if (other.gameObject.CompareTag ("PlatformController")) 
		{
			if (Input.GetAxis ("Interact") != 0&&!pickingUp)
				
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
		if (other.gameObject.CompareTag ("PickUp")) 
		{
			withAPickUpObj = false;
		}


		if (other.gameObject.layer == 11)
		{
			onSlope=false;
		}
		
		if (other.gameObject.CompareTag ("RockContact")||other.gameObject.CompareTag ("PickUp"))
		{
			speed=speedLog;
			onRock = false;
		}
		
		if (other.gameObject.CompareTag ("Wheel"))
			withWheel = false;
		
		if (other.gameObject.CompareTag ("MovingPlatform"))
		{
			transform.parent = null;
		}

	}
}
