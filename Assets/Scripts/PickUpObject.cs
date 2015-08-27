using UnityEngine;
using System.Collections;

public class PickUpObject : MonoBehaviour {

	public bool pickedUp;
	public Vector3 offSet;

	private GameObject player;
	private PlayerControl playerScript;




	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player");
		playerScript = player.GetComponent<PlayerControl> ();
		pickedUp = false;

	}
	
	// Update is called once per frame
	void Update () {
	



		if(playerScript.pickingUp)
		{

			GetComponent<Rigidbody2D>().isKinematic = true;
			if(playerScript.facingRight)
			{	
				transform.position = new Vector3(player.transform.position.x + 2f,player.transform.position.y+0.5f,0f);
			}
			else
			{
				transform.position = new Vector3(player.transform.position.x - 2f,player.transform.position.y+0.5f,0f);
			}



			
		}
		if (!playerScript.pickingUp) 
		{

			GetComponent<Rigidbody2D>().isKinematic = false;
		}



	}



		

	void OnTriggerStay2D(Collider2D other)
	{

		if (other.gameObject.CompareTag("Ground")) 
		{

			GetComponent<Rigidbody2D>().isKinematic = true;
		}
	}
	void OnTriggerExit2D(Collider2D other)
	{


	}
}
