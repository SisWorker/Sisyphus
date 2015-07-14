using UnityEngine;
using System.Collections;
using System;

public class PlayerMovement : MonoBehaviour {

	private Rigidbody2D rb;
	public float speed=30.0f;
	public float maxSpeed= 30.0f;
	public bool grounded;

	public Transform GroundCheck;
	 
	public LayerMask groundLayer;

	// Use this for initialization
	void Start () 
	{

		rb = GetComponent<Rigidbody2D> ();
		Console.WriteLine ("start");

	}
	

	void FixedUpdate () 
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");

		/*float curSpeed = rb.velocity.magnitude;

		if(rb.velocity.magnitude > maxSpeed)
		{
			rb.velocity = rb.velocity.normalized * maxSpeed;
		}*/
		float ySpeed = 2f*rb.velocity.y;

		Vector3 movement = new Vector3 ( speed*moveHorizontal,ySpeed, 0.0f);

		if (ySpeed>=0.0f)

			movement = new Vector3 ( speed*moveHorizontal,0.0f, 0.0f);



		//rb.MovePosition (transform.position + movement);
		rb.velocity = movement;

		//rb.AddForce (movement);

		if (Input.GetButtonDown ("Jump"))
			Jump ();

	}

	void Jump()
	{
		Vector2 playerPos = new Vector2(transform.position.x, transform.position.y);
		Vector2 groundPos = new Vector2(GroundCheck.position.x, GroundCheck.position.y);
		
		grounded = Physics2D.Linecast(playerPos, groundPos, 1 << LayerMask.NameToLayer("Ground"));

		Debug.Log ("yes");

		if (grounded) 
		{
			Vector3 jumpForce = new Vector3 (0f, 1500f, 0f);
			rb.AddForce (jumpForce);
		}
		else
			return;
	}

}
