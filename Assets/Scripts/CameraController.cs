using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	private GameObject Player;
	protected Vector3 Offset;
	public bool Working;


	// Use this for initialization
	void Start ()
	{
		Player = GameObject.Find ("Player");

		Offset = transform.position - Player.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () 
	{
		if (!Working) 
		{
			transform.position = Player.transform.position + Offset;
		}
	}


}
