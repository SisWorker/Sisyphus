using UnityEngine;
using System.Collections;

public class RockObject : MonoBehaviour {

	public Rigidbody2D Rock;
	public UIDialog ms;
	private float MaxSpeed =7.0f;
	private Vector3 ejectRock;
	private bool playerContact;
	private bool onSlope;


	void Start () {
		Rock = GetComponent<Rigidbody2D>();
		ejectRock = new Vector3 (30f, 40f, 0.0f);
		playerContact = false;
	}
	
	// Update is called once per frame
	void Update () {
		float ySpeed = Rock.velocity.y;

		if (Rock.velocity.x > MaxSpeed) 
		{
			Rock.velocity = new Vector2(MaxSpeed, ySpeed);
		}
		if (Rock.velocity.x < (-MaxSpeed) )
		{
			Rock.velocity = new Vector2(-MaxSpeed, ySpeed);
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
			MaxSpeed=3.7f;
			playerContact=true;
			//Debug.Log("withP");
		}

	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("Player")) {
			MaxSpeed = 7.5f;
			playerContact = false;
			//Debug.Log ("outP");
		}


	}


}
