using UnityEngine;
using System.Collections;

public class OneWayPlatform : MonoBehaviour {

	public bool RightDirect;

	private int ExitCount;
	private bool Exit;
	// Use this for initialization
	void Start () {
		ExitCount = 0;
		Exit = false;
		RightDirect = true;
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
	void OnTriggerExit2D(Collider2D other)
	{

		if(other.gameObject.CompareTag ("Player"))
		{	
			Exit = true;
		}
	}
}
