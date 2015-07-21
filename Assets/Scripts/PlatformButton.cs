using UnityEngine;
using System.Collections;

public class PlatformButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("RockContact")||other.gameObject.CompareTag ("Player"))
		{
			transform.GetComponent<SpriteRenderer>().enabled=false;
			transform.GetComponentInParent<MovingPlatform>().MoveOn();
		}

	}
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("RockContact")||other.gameObject.CompareTag ("Player"))
		{
			transform.GetComponent<SpriteRenderer>().enabled=true;
		}
	}
}
