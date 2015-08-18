using UnityEngine;
using System.Collections;

public class PoleScript : MonoBehaviour {

	public GameObject target;
	private bool moving;
	private bool activated;
	private int moveCount;
	// Use this for initialization
	void Start () {
		moving = false;
		activated = false;
		moveCount = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (moving && moveCount < 100) {
			transform.position = new Vector3 (transform.position.x, transform.position.y - 0.05f, transform.position.z);
			moveCount++;
		} else if (activated) {
			gameObject.SetActive(false);
		}
	
	}
	void OnCollisionEnter2D(Collision2D other)
	{
		if(!activated&&other.gameObject.CompareTag("Player")&&(other.rigidbody.velocity.y > -1 &&other.rigidbody.velocity.y<1))
		{
			moving = true;
			activated = true;
			Proceed ();
		}
	}

	void Proceed()
	{
		Debug.Log ("Hello");
		target.GetComponent<ImproveBridge> ().Working = true;
	}
}
