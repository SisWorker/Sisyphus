using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject Player;
	protected Vector3 Offset;

	// Use this for initialization
	void Start ()
	{
		Offset = transform.position - Player.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () 
	{
		transform.position = Player.transform.position + Offset;
	}
}
