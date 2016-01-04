using UnityEngine;
using System.Collections;

public class WheelControl : MonoBehaviour {
	public bool Working;
	public float RotateTime;

	public GameObject[] bridges;


	private Vector3 origin;
	private Vector3 ZAxis = new Vector3 (0f, 0f, 1f);
	private GameObject player;
	private float TimePassed;

	private PlayerControl playerScript;




	// Use this for initialization
	void Start () {

		player = GameObject.Find ("Player");

		//bridge = GameObject.Find ("bridge");

		playerScript = player.GetComponent<PlayerControl> ();

		origin = transform.position;

		Working = false;
		RotateTime = 1.5f;
		TimePassed = 0f;


	
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{

		if (Working) 
		{
			transform.RotateAround (origin, ZAxis, 40f * Time.deltaTime);

			if (TimePassed < RotateTime) {
				TimePassed += 0.02f;
			} else {
				TimePassed = 0f;
				Working = false;
			}

	
		}
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.CompareTag ("Player")) 
		{
			if (playerScript.Operating)
			{
				Working = true;

				foreach (GameObject b in bridges) {
					ImproveBridge bridgeScript = b.gameObject.GetComponent<ImproveBridge> ();
					bridgeScript.Working = true;
				}
			}
		}
	}
}
