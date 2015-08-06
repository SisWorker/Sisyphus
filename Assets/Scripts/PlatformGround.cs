using UnityEngine;
using System.Collections;

public class PlatformGround : MonoBehaviour {

	public bool RightDirect;

	private int ExitCount;
	private bool Exit;
	// Use this for initialization
	void Start () {
		ExitCount = 0;
		Exit = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void FixedUpdate()
	{
		if (Exit) {
			ExitCount++;
			if(ExitCount == 50)
			{
				gameObject.layer = 11;
				ExitCount = 0;
				Exit = false;
			}
		}

	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.CompareTag ("Player"))
		{	
			Debug.Log ("Enter");
		}
	}
	void OnTriggerStay2D(Collider2D other)
	{
		if(other.gameObject.CompareTag ("Player"))
		{
			if(Input.GetAxis ("Vertical")<0)
			{

				gameObject.layer = 8;
			}
		}
	}
	void OnTriggerExit2D(Collider2D other)
	{

		if(other.gameObject.CompareTag ("Player"))
		{	
			Exit = true;
		}
	}
}
