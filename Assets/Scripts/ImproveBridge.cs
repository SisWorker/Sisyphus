using UnityEngine;
using System.Collections;

//now the script controlls the rotation of the pivot of the bridge, and the bridge is now a child of this point and rotates accordingly.
//a bridge can have multipal position to stop

public class ImproveBridge : MonoBehaviour {

	public bool Working;
	//public float RotateTime;
	//public Transform[] stops;
	public float[] rotateSpeed;
	//public bool[] stops;

	public int curStop;


	//origin of rotation.
	public Transform pivot;


	private Vector3 origin;

	private Vector3 ZAxis = new Vector3 (0f, 0f, 1f);
	private int position;
	


	
	// Use this for initialization
	void Start () 
	{

		origin = pivot.transform.position;
		curStop = 0;
		Working = false;



	}
	
	// Update is called once per frame
	void Update () 
	{

		if (Working) 
		{
			Rotate (curStop);
		}



	}

	void OnTriggerEnter2D(Collider2D other)
	{
	

	}
	void OnTriggerExit2D(Collider2D other)
	{

	}


	void Rotate(int pos)
	{			
		transform.RotateAround (origin, ZAxis, rotateSpeed[pos] * Time.deltaTime);


	}

}
