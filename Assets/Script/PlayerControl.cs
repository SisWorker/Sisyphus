using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	public int speed;
	public float x;
	public float y;
	public int jumpVelocity;
	public Rigidbody2D Rock;

	private Rigidbody2D Rbody;
	private Animator animator;
	private Vector3 antiGravity;
	private bool canJump;
	private bool onGround;
	private Vector3 groundEulerAngle;



	// Use this for initialization
	void Start () 
	{
		Rbody = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator>();
		x = 0.1f;
		y = -0.0f;
		onGround = false;

	}




	// Update is called once per frame
	void FixedUpdate () 
	{

		animator.SetBool ("PushRock", false);
		Rbody.transform.rotation = Quaternion.identity;

		Vector2 jump = new Vector2 (0.0f, jumpVelocity);
//	Freeze player if on slope
		if (groundEulerAngle.z != 0.0f)
			Rbody.velocity = new Vector3(0.0f,0.0f,0.0f);
//	When can player jump
		if ((Input.GetAxis ("Jump")!=0)&(onGround))
			Rbody.velocity = jump;

		float moveHorizontal = Input.GetAxis ("Horizontal");

//	Move according to gound angle

		Vector3 move = new Vector3 ((moveHorizontal/(Mathf.Cos(groundEulerAngle.z))* speed), (moveHorizontal/(Mathf.Sin(groundEulerAngle.z)) * speed), 0.0f);
		Rbody.transform.position += move;

//	Animation change according to action
		if (moveHorizontal!=0.0f)
			animator.SetBool("Walking", true);
		else
			animator.SetBool("Walking",false);
	}





	void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("RockContact"))
			animator.SetBool("PushRock",true);

		if (other.gameObject.CompareTag ("Ground")) 
		{
			onGround = true;
			groundEulerAngle = other.transform.eulerAngles;
			print(groundEulerAngle);

		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("Ground"))
		{
			onGround = false;
			groundEulerAngle = new Vector3(0.0f,0.0f,0.0f);
			print (groundEulerAngle);
		}

	}
		
}
