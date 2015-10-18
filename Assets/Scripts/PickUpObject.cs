using UnityEngine;
using System.Collections;

public class PickUpObject : MonoBehaviour {

	public bool pickedUp;
	public Vector3 offSet;
	public bool canThrow=true;

	private GameObject player;
	private PlayerControl playerScript;
	private bool playerContact;
	public bool onGround;

	private float timer=0;




	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player");
		playerScript = player.GetComponent<PlayerControl> ();
		pickedUp = false;
		//canThrow = true;

	}

	void FixedUpdate()
	{
		if (canThrow = false) 
		{
			timer += 0.02f;
		}
		if ((timer>=1)&&!canThrow)  //make the pickup only throwable once in each second
		{
			canThrow=true;
			timer=0f;
		}
	}
	
	// Update is called once per frame
	void Update () {
	



		if (playerScript.pickingUp && playerContact&&playerScript.canHoldMore) {
			Debug.Log("Picked UP");
			pickedUp = true;
			GetComponent<Rigidbody2D> ().isKinematic = true;
			playerScript.canHoldMore = false;

		}
		if (pickedUp) {
			if (playerScript.facingRight) {	
				transform.position = new Vector3 (player.transform.position.x + 2f, player.transform.position.y + 0.5f, 0f);
			} else {
				transform.position = new Vector3 (player.transform.position.x - 2f, player.transform.position.y + 0.5f, 0f);
			}
		}


			
		
		if (!playerScript.pickingUp) 
		{
			pickedUp = false;
			if(!onGround){
			GetComponent<Rigidbody2D>().isKinematic = false;
			}
		}



	}



		

	void OnTriggerStay2D(Collider2D other)
	{

		if (other.gameObject.layer==11&&other.gameObject.name!="bridge"&&!other.gameObject.CompareTag("PickUp")) 
		{
			onGround = true;
			GetComponent<Rigidbody2D>().isKinematic = true;
		}
		if (other.gameObject.CompareTag ("Player")) 
		{
			playerContact = true;
		}
	}
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("Player")) 
		{
			playerContact = false;
		}
		if (other.gameObject.layer==11) {
			onGround = false;
		}
	}
}
