using UnityEngine;
using System.Collections;

public class RockObject : MonoBehaviour {

	private Rigidbody2D Rock;
	public UIDialog ms;

	public float SecondsToB=5f;
	private float Counter;
	private Vector3 BackForce;
	private bool backDirection;

	private float MaxSpeed =8.0f;
	private float MaxSpeedY =5.5f;
	private Vector3 ejectRock;
	public bool playerContact;
	private bool onSlope;
	private bool onGround;
	private bool onPlatform;


	private Vector3 offset;



	void Start ()
	{
		Rock = GetComponent<Rigidbody2D>();
		ejectRock = new Vector3 (40f, 60f, 0.0f);
		playerContact = false;
		onGround = false;
		Counter = 0;

	}
	
	// Update is called once per frame
	void Update () {

		float ySpeed = Rock.velocity.y;
		float xSpeed = Rock.velocity.x;


		if (onGround == true)
		{
			Rock.drag = 1.5f;

			if (Rock.velocity.x > MaxSpeed) {
				Rock.velocity = new Vector2 (MaxSpeed, ySpeed);
			}
			if (Rock.velocity.x < (-MaxSpeed)) {
				Rock.velocity = new Vector2 (-MaxSpeed, ySpeed);
			}
			if ((playerContact)) {
				if (Rock.velocity.x > MaxSpeed) {
					Rock.velocity = new Vector2 (MaxSpeed, MaxSpeedY);
				}
				if (Rock.velocity.y < (-MaxSpeed)) {
					Rock.velocity = new Vector2 (MaxSpeed, -MaxSpeedY);
				}
				
			}
		} 
		else 
		{
			Rock.drag = 0f;
		}

	}

	void FixedUpdate()
	{
		//Rollback
		BackForce = new Vector3 (-4, 0, 0);
		if (backDirection) 
		{
			//Debug.Log ("rightward!");
			BackForce = new Vector3 (4, 0f, 0);
		}

		if ((Counter >= SecondsToB) &&((Rock.velocity.x<MaxSpeed)&&(Rock.velocity.x>(-MaxSpeed))))
		{
			//Debug.Log ("Burst!");

			Rock.AddForce(BackForce);
		}

		if ((playerContact))
		{
			//counter clears to 0 if being touched or moved
			Counter = 0f;
		}
		else 
		{
			if (onGround&&(Rock.velocity.y==0))
			{
				//Counter accumulates when is alone and onground
				Counter += 0.02f;
				//Debug.Log ("Accel!");
			}

		}

	}


	void LateUpdate()
	{
		if (onPlatform) {
			transform.position = transform.position - offset;
		}
	}



	void OnTriggerEnter2D(Collider2D other)
	{
		//eject rock when reach top
		if (other.gameObject.CompareTag ("Bridge")) {

		}
		if (other.gameObject.CompareTag ("Top")) {
			Rock.velocity = ejectRock;
			ms.Proceed ();
		}

		//slow when touching player
		if (other.gameObject.CompareTag ("Player"))
		{
			MaxSpeed=5.5f;
			playerContact=true;
		}

		if (other.gameObject.layer == 11)
		{
			onGround = true;
			if (other.gameObject.tag!="Slope")
			{
				backDirection=other.gameObject.GetComponent<OneWayPlatform>().RightDirect;
			}
		}
		if (other.gameObject.CompareTag ("MovingPlatform"))
		{
			onPlatform = true;
			offset = other.gameObject.GetComponentInParent<MovingPlatform>().GetMoveDir();
		}
	
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("MovingPlatform"))
		{
			offset = other.gameObject.GetComponentInParent<MovingPlatform>().GetMoveDir();
		}
	}


	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("Player")) {
			MaxSpeed = 8f;
			playerContact = false;
		}

		if (other.gameObject.layer == 11)
		{
			onGround = false;
		}
		if (other.gameObject.CompareTag ("MovingPlatform"))
		{
			onPlatform = false;
		}

	}



}
