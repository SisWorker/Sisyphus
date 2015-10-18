using UnityEngine;
using System.Collections;

public class StoryTellingPoint : MonoBehaviour {

	private bool hasPlayed;
	private GameObject MCamera;
	private GameObject Player;
	private GameObject Rock;
	private float TimePassed;
	private Vector3 Distance;
	private float CameraSize;
	private bool Working;
	private int OnAction=0;
	private Vector3 CameraPos;
	private Camera cam;
	private float SizeDif;
	private CameraController camScript;
	public float[] DoTime;

	public string[] ToDos;

	public Vector3 [] Targets;

	public float BackTime;


	// Use this for initialization
	void Start () 
	{
		hasPlayed = false;
		Working = false;
		Player = GameObject.Find ("Player");
		Rock = GameObject.Find ("Rock");
		MCamera = GameObject.Find ("Main Camera");
		cam= MCamera.GetComponent<Camera>();
		TimePassed = 0f;

		camScript= MCamera.GetComponent<CameraController>();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if (Working)
		{
			camScript.Working=true;


			//hold player and rock still while camera is moving
			Rigidbody2D PRB=Player.GetComponent<Rigidbody2D>();
			PRB.velocity= new Vector3(0,0,0);
			RockObject RockScript=Rock.GetComponent<RockObject>();
			RockScript.playerContact=true;

			if (OnAction>= DoTime.Length)
			{
				BackToPlayer(BackTime);
				//ZoomBack(BackTime);
			}
			else 
			{
				if (ToDos[OnAction]=="Move")
				{
					MoveTo (DoTime[OnAction],Targets[OnAction]);
				}

				if (ToDos[OnAction]=="Zoom")
				{
					ZoomTo (DoTime[OnAction],Targets[OnAction]);
				}

				if (ToDos[OnAction]=="Hold")
				{
					Hold(DoTime[OnAction]);
				}
			}

		}
	}

	void BackToPlayer(float time)
	{
		//Debug.Log ("Back");

		if (TimePassed == 0) 
		{
			Distance= (CameraPos-MCamera.transform.position)/time/50f;

			SizeDif= (CameraSize-cam.orthographicSize)/time/50f;
		}

		//move camera Backto player
		MCamera.transform.position = MCamera.transform.position+Distance;
		cam.orthographicSize += SizeDif;

		TimePassed += 0.02f;


		if (TimePassed >= time) 
		{
			//Debug.Log ("StopWork");

			Working = false;
			camScript.Working=false;

			TimePassed=0;
		}
	}


	void MoveTo(float time, Vector3 Point)
	{
		//Debug.Log ("MoveTo");

		if (TimePassed == 0) 
		{	
			Distance = (Point - MCamera.transform.position) / time / 50f;
		}

		MCamera.transform.position = MCamera.transform.position+Distance;
		TimePassed += 0.02f;

		if (TimePassed >= time) 
		{
			OnAction+=1;
			TimePassed=0;
		}
	}

	void ZoomTo(float time, Vector3 Size)
	{
		//Debug.Log ("ZoomTo");

		if (TimePassed == 0) 
		{	
			SizeDif= (Size.z-cam.orthographicSize)/time/50f;
		}
		
		cam.orthographicSize += SizeDif;
		TimePassed += 0.02f;
		
		if (TimePassed >= time) 
		{
			OnAction+=1;
			TimePassed=0;
		}
	}

	void Hold(float time)
	{
		//Debug.Log ("Hold");

		TimePassed += 0.02f;

		if (TimePassed >= time) 
		{
			OnAction+=1;
			TimePassed=0;
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag ("Player") && (!hasPlayed)) 
		{
			//Debug.Log ("start to work");

			//record camera's position and size before move
			CameraPos=MCamera.transform.position;
			CameraSize=cam.orthographicSize;

			Working=true;
			hasPlayed=true;
		}

	}
}
