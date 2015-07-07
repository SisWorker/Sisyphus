using UnityEngine;
using System.Collections;

public class RockObject : MonoBehaviour {

	public Rigidbody2D Rock;
	private Vector3 ejectRock;
	// Use this for initialization
	void Start () {
		Rock = GetComponent<Rigidbody2D>();
		ejectRock = new Vector3 (30f, 40f, 0.0f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("Top"))
			Rock.velocity = ejectRock;
	}





}
