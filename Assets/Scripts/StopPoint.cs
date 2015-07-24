using UnityEngine;
using System.Collections;

public class StopPoint : MonoBehaviour {
	public GameObject TheBridge;
	public int Number;


	private SpriteRenderer SR;
	private ImproveBridge bridgeScript;


	// Use this for initialization
	void Start () {
		bridgeScript = TheBridge.GetComponent<ImproveBridge>();
		SR = GetComponent<SpriteRenderer> ();
		SR.enabled = false;

	}
	
	// Update is called once per frame

	void Update () {

	}

	void OnTriggerStay2D(Collider2D other)
	{


		if (other.CompareTag ("Bridge")) 

		{


			if (Number == bridgeScript.curStop) 
			{
				bridgeScript.Working = false;
				bridgeScript.curStop ++;
				if (bridgeScript.curStop == bridgeScript.rotateSpeed.Length) 
				{
					bridgeScript.curStop = 0;
				}

			}
		}

	}

}
