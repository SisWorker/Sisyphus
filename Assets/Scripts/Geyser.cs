using UnityEngine;
using System.Collections;

public class Geyser : MonoBehaviour {
	public bool Active;
	public float activeTime;
	public float inactiveTime;
	public float pushForceFactor;
	public float heightLimit;


	private float horAdj;
	private float actCounter;
	private Animator animator;
	private Rigidbody2D Rock;
	private float relativeHeight;
	private float pushForceScale;
	// Use this for initialization
	void Start () {

		Rock = GameObject.Find ("Rock").GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
		Active = false;
		animator.SetBool ("Active", Active);
		relativeHeight = 0f;

	}
	
	// Update is called once per frame
	void Update () {

	}
	void FixedUpdate()

	{
		actCounter += 0.02f;
		if(actCounter>=activeTime && Active==true)
		{
			Active=!Active;
			actCounter=0f;
			animator.SetBool ("Active", Active);
		}
		if(actCounter>=inactiveTime &&Active==false)
		{
			Active=!Active;
			actCounter=0f;
			animator.SetBool ("Active", Active);
		}





	}

	void OnTriggerStay2D(Collider2D other)
	{
		if(Active)
		{

			if ((other.transform.position.y - transform.position.y) >= 0f) {
				relativeHeight = other.transform.position.y - transform.position.y;
			} else {
				relativeHeight = 0f;
			}
			
			if (relativeHeight >= heightLimit) {
				pushForceScale = 0f;
			} else {
				pushForceScale = Mathf.Pow(1-(relativeHeight/heightLimit),8);
			}


			if((other.transform.position.x - transform.position.x) > 0f)
			{
				horAdj = -1f;
			}
			else if((other.transform.position.x - transform.position.x) < 0f)
			{
				horAdj = 1f;
			}
			else
			{
				horAdj = 0f;
			}



			other.attachedRigidbody.velocity = new Vector2(horAdj,heightLimit*0.5f*pushForceScale);

		}
	}
	void OnTriggerExit2D(Collider2D other)
	{

	}




}
