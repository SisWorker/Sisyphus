using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	private GameObject Player;
	protected Vector3 Offset;

	// Use this for initialization
	void Start ()
	{
		Player = GameObject.Find ("Player");

		Offset = transform.position - Player.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () 
	{
		transform.position = Player.transform.position + Offset;
	}
}
