using UnityEngine;
using System.Collections;

public class SkyControll : MonoBehaviour {

	private GameObject Camera;
	protected Vector3 Offset;
	protected float OffsetX;
	protected float OffsetY;
	protected float CameraLastX;
	protected float CameraLastY;
	
	// Use this for initialization
	void Start ()
	{
		Camera = GameObject.Find ("Main Camera");
		//Offset = transform.position - Camera.transform.position;
		//transform.position = Camera.transform.position + Offset;
		CameraLastX = Camera.transform.position.x;
		CameraLastY = Camera.transform.position.x;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		OffsetX = (CameraLastX - Camera.transform.position.x)* 0.8f ;
		OffsetY = (CameraLastY - Camera.transform.position.y) * 0.5f;

		Offset = new Vector3 (-OffsetX, -OffsetY , 0f);

		transform.position = transform.position + Offset;

		CameraLastX=Camera.transform.position.x;
		CameraLastY=Camera.transform.position.y;
	}
}
