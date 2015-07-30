using UnityEngine;
using System.Collections;

public class StopPoint : MonoBehaviour {
	public GameObject TheBridge;
	public int Number;


	private SpriteRenderer SR;
	private ImproveBridge bridgeScript;
	private float bridgeRotation;

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


				bridgeRotation=TheBridge.transform.eulerAngles.z;

				if ((bridgeRotation>80)&&(bridgeRotation<100))
				{
					TheBridge.transform.Find("NoFricBridge").gameObject.SetActive(true);
				}
				else 
				{
					TheBridge.transform.Find("NoFricBridge").gameObject.SetActive(false);

					if(((bridgeRotation>170)&&(bridgeRotation<190))||((bridgeRotation>350)&&(bridgeRotation<360))||((bridgeRotation>0)&&(bridgeRotation<10)))
					{
						TheBridge.tag="Ground";
					}
					else
					{
						TheBridge.tag="Slope";
					}
				}
					
			}
		}

	}

}
