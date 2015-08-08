using UnityEngine;
using System.Collections;

public class Gyser : MonoBehaviour {

	private bool gyserActive = true;
	private float gyserInactiveCounter = 0f;
	private float gyserActiveCounter = 0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
//		if(gyserActive==true)
//		{
//			gyserActiveCounter+=0.02f;
//			if(gyserActiveCounter == 5f)
//			{
//				gyserActive = false;
//				Debug.Log("Inactive");
//				gyserActiveCounter = 0f;
//			}
//		}
//		else
//		{
//			gyserInactiveCounter+=0.02f;
//			if(gyserInactiveCounter == 5f)
//			{
//				gyserActive = true;
//				Debug.Log("Active");
//				gyserInactiveCounter = 0f;
//			}
//		}
		
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if (gyserActive) {
			other.attachedRigidbody.AddForce (new Vector2 (0f, 30f));
	
		}
	}



}
