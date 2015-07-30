using UnityEngine;
using System.Collections;

//now the script controlls the rotation of the pivot of the bridge, and the bridge is now a child of this point and rotates accordingly.
//a bridge can have multipal position to stop

public class ImproveBridge : MonoBehaviour {

	public bool Working;
	public float[] rotateSpeed;
	//public bool[] stops;

	public int curStop;

	public bool rockOnBridge = false;
	public bool throwRock = false;

	//origin of rotation.
	public Transform pivot;

	private Vector3 origin;

	private Rigidbody2D Rock;

	private Vector3 ZAxis = new Vector3 (0f, 0f, 1f);
	private int position;
	private GameObject NoFricBridge;


	
	// Use this for initialization
	void Start () 
	{
		Rock = GameObject.Find ("rock").GetComponent<Rigidbody2D> ();

		NoFricBridge = transform.Find ("NoFricBridge").gameObject;
		NoFricBridge.SetActive (false);

		origin = pivot.transform.position;
		curStop = 0;
		Working = false;

		float bridgeRotation=transform.eulerAngles.z;

		if ((bridgeRotation>80f)&&(bridgeRotation<100f))
		{
			NoFricBridge.SetActive(true);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{

		if (Working) 
		{
			Rotate (curStop);


		}

		if (throwRock == true) 
		{
			Rock.AddForce(new Vector2(500f,1000f));
			throwRock = false;
			
		}



	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.CompareTag("RockContact"))
			
		{
			rockOnBridge = true;
			
		}
	}
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("RockContact"))
		{
			rockOnBridge = false;
		}
	}



	void Rotate(int pos)
	{	
		NoFricBridge.SetActive(false);

		this.tag = "Bridge";
		transform.RotateAround (origin, ZAxis, rotateSpeed[pos] * Time.deltaTime);

	}

}
