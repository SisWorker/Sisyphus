using UnityEngine;
using System.Collections;

public class RockContact : MonoBehaviour {

	public GameObject Rock;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Rock.transform.position;
	}
}
