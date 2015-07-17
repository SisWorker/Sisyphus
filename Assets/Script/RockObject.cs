using UnityEngine;
using System.Collections;

public class RockObject : MonoBehaviour {

	public Rigidbody2D Rock;
	public UIDialog ms;
	private float MaxSpeed =8.0f;
	private float MaxSpeedY =5.5f;
	private Vector3 ejectRock;
	private bool playerContact;
	private bool onSlope;
	private bool onGround;



	void Start () {
		Rock = GetComponent<Rigidbody2D>();
		ejectRock = new Vector3 (40f, 60f, 0.0f);
		playerContact = false;
		onGround = false;
	}
	
	// Update is called once per frame
	void Update () {
		float ySpeed = Rock.velocity.y;
		float xSpeed = Rock.velocity.x;


		if (Rock.velocity.x > MaxSpeed) 
		{
			Rock.velocity = new Vector2(MaxSpeed, ySpeed);
		}
		if (Rock.velocity.x < (-MaxSpeed) )
		{
			Rock.velocity = new Vector2(-MaxSpeed, ySpeed);
		}
		if ((playerContact))
		{
			if (Rock.velocity.y > MaxSpeedY)
			{
				Rock.velocity = new Vector2 (xSpeed, MaxSpeedY);
			}
			if (Rock.velocity.y < (-MaxSpeedY)) 
			{
				Rock.velocity = new Vector2 (xSpeed, -MaxSpeedY);
			}

		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		//eject rock when reach top
		if (other.gameObject.CompareTag ("Top")) {
			Rock.velocity = ejectRock;
			ms.Proceed ();
		}

		//slow when touching player
		if (other.gameObject.CompareTag ("Player"))
		{
			MaxSpeed=4.5f;
			playerContact=true;
			//Debug.Log("withP");
		}

		if (other.gameObject.layer == 8)
		{
			onGround = true;
		}
		if (other.gameObject.CompareTag ("MovingPlatform"))
		{
			transform.parent = other.transform;
		}


	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("Player")) {
			MaxSpeed = 8f;
			playerContact = false;
			//Debug.Log ("outP");
		}

		if (other.gameObject.layer == 8)
		{
			onGround = false;
		}
		if (other.gameObject.CompareTag ("MovingPlatform"))
		{
			transform.parent = null;
		}


	}


}
