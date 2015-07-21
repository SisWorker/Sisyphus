using UnityEngine;
using System.Collections;

public class BridgeControl : MonoBehaviour {

	public bool Working;
	public float RotateTime;
	public float TimePassed;

	Vector3 origin = new Vector3 (-4f, -8.1f, 0f);
	Vector3 ZAxis = new Vector3 (0f, 0f, 1f);

	private GameObject player;
	private PlayerControl playerScript;

	private int position;

	private float rotation1=-33.25f;
	private float rotation2=-(38.79505f/1.5f-0.75f);
	private float rotation3=44.1f;



	// Use this for initialization
	void Start () {

		player = GameObject.Find ("Player");
		playerScript = player.GetComponent<PlayerControl> ();

		Working = false;
		RotateTime = 1.5f;
		TimePassed = 0f;

		position = 1;
	
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{

		if (Working) 
		{
			Rotate (position);
		}
	}

	void Rotate( int pos)
	{			
		if (pos == 1) 
		{
			RotateTime = 1.5f;
			transform.RotateAround (origin, ZAxis, rotation1 * Time.deltaTime);
			
			if (TimePassed < RotateTime)
			{
				TimePassed += 0.02f;
			} 
			else
			{
				TimePassed = 0f;
				Working = false;
				position = 2;
				gameObject.tag="Slope";
			}
		}

	 	if (pos == 2) 
		{
			RotateTime = 1.5f;

			transform.RotateAround (origin, ZAxis, rotation2 * Time.deltaTime);
			
			if (TimePassed < RotateTime)
			{
				TimePassed += 0.02f;
			} 
			else
			{
				TimePassed = 0f;
				Working = false;
				position = 3;
				gameObject.tag="Ground";
			}
		}

		if (pos == 3) 
		{
			transform.RotateAround (origin, ZAxis, rotation3 * Time.deltaTime);
			RotateTime=2f;

			if (TimePassed < RotateTime)
			{
				TimePassed += 0.02f;
			} 
			else
			{
				TimePassed = 0f;
				Working = false;
				position = 1;
			}
		}

	}

}
