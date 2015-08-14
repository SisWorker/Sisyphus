using UnityEngine;
using System.Collections;

public class SkyControll : MonoBehaviour {

	public float FloatSpeed;
	public float RelativeSpeedX;
	public float RelativeSpeedY;
	public string LoadName;

	public bool hasCreatedCopyL=false;
	public bool hasCreatedCopyR=false;

	public bool canCreateCopy;
	private bool withPlayer;

	private float CopyDistance;
	private float scale;
	private GameObject Camera;
	private GameObject CopyL;
	private GameObject CopyR;

	protected Vector3 Offset;
	protected float OffsetX;
	protected float OffsetY;
	protected float CameraLastX;
	protected float CameraLastY;
	
	// Use this for initialization
	void Start ()
	{
		Camera = GameObject.Find ("Main Camera");

		CameraLastX = Camera.transform.position.x;
		CameraLastY = Camera.transform.position.y;
		canCreateCopy = false;

		scale = transform.localScale.x;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{

		Float ();
		Follow ();
		Undraw ();
		Detect ();


	}

	void Follow ()
	{
		OffsetX = (CameraLastX - Camera.transform.position.x) * RelativeSpeedX ;
		OffsetY = (CameraLastY - Camera.transform.position.y) * RelativeSpeedY;
		
		Offset = new Vector3 (-OffsetX, -OffsetY , 0f);
		
		transform.position = transform.position + Offset;
	}

	void Float()
	{
		transform.position = (transform.position + new Vector3 ((FloatSpeed/200), 0, 0));
	}


	void Detect()
	{

		canCreateCopy = withPlayer;


			//if about to use up horizontal distance, copy
		if(canCreateCopy)
		{
			if (((transform.position.x - Camera.transform.position.x) > 15f) && (!hasCreatedCopyL))
			// if player is going left
			{

				CopyL = (GameObject)Instantiate(Resources.Load(LoadName), new Vector3( (transform.position.x - (98.9f*scale)),(transform.position.y) ,transform.position.z),transform.rotation);

				hasCreatedCopyL=true;

				SkyControll CopyScript =CopyL.GetComponent<SkyControll>();
				CopyScript.hasCreatedCopyR=true;
			}

			if (((Camera.transform.position.x - transform.position.x) > 15f)&&(!hasCreatedCopyR))
			//if player is going right
			{

				CopyR= (GameObject)Instantiate(Resources.Load(LoadName), new Vector3( (transform.position.x + (98.9f*scale)),(this.transform.position.y) ,transform.position.z),transform.rotation);
				hasCreatedCopyR=true;

				SkyControll CopyScript =CopyR.GetComponent<SkyControll>();
				CopyScript.hasCreatedCopyL=true;
			}
		}

		CameraLastX=Camera.transform.position.x;
		CameraLastY=Camera.transform.position.y;
	}

	void Undraw()
	{
		if ((hasCreatedCopyR)&&(CopyR!=null))
		{
			if ((CopyR.transform.position.x - Camera.transform.position.x) > 100)
			{
				CopyR.SetActive (false);
				hasCreatedCopyR=false;
			}
		}
		if ((hasCreatedCopyL)&&(CopyL!=null))
		{
			if ((CopyL.transform.position.x - Camera.transform.position.x) < -100)
			{
				CopyL.SetActive (false);
				hasCreatedCopyL=false;
			}
		}

	}


	void OnTriggerStay2D(Collider2D other)
	{

		if (other.tag == "Player") 
		{
			Debug.Log("meetplayer");
			withPlayer=true;
		}

	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Player") 
		{
			withPlayer= false;
			//canCreateCopy=true;
		}
	}

}
