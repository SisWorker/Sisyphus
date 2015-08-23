using UnityEngine;
using System.Collections;

public class PickUpObject : MonoBehaviour {

	public bool pickedUp;
	public Vector3 offSet;

	private GameObject player;
	private GameObject pickUpObject;
	private bool playerContact;
	private bool onGround;



	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player");
		pickUpObject = GameObject.Find("PickUpRock");
		pickedUp = false;
		playerContact = false;
	}
	
	// Update is called once per frame
	void Update () {
	



		if(pickedUp)
		{

			transform.position = player.transform.position + offSet;
			if(Input.GetAxis("Interact")!=0)
			{

				pickedUp = false;
				return;
			}
			
		}
		if (!pickedUp) 
		{
			if(onGround==true)
			{
				pickUpObject.GetComponent<Rigidbody2D>().isKinematic = true;
			}


			if (playerContact&&Input.GetAxis ("Interact")!=0) 
			{

				pickedUp = true;

				pickUpObject.GetComponent<Rigidbody2D>().mass = 0f;
				pickUpObject.GetComponent<Rigidbody2D>().isKinematic = false;
				Vector3 tranPickUp = new Vector3(0f,player.transform.position.y - transform.position.y+0.5f,0f);
				transform.position += tranPickUp;
				offSet = transform.position - player.transform.position;



			}


		}

	


	}



		

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("Player")) 
		{
			playerContact = true;
		}
		if (other.gameObject.layer == 11) 
		{
			onGround = true;
		}
	}
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.layer == 11)
		{
			onGround = false;
		}
		if (other.gameObject.CompareTag ("Player")) 
		{
			playerContact = false;
		}
	}
}
