using UnityEngine;
using System.Collections;

//now the script controlls the rotation of the pivot of the bridge, and the bridge is now a child of this point and rotates accordingly.
//a bridge can have multipal position to stop

public class ImproveBridge : MonoBehaviour {

	public bool Working;
	public float[] rotateSpeed;
	//public bool[] stops;

	public int curStop;
	public bool throwRock = false;

	//origin of rotation.
	public Transform pivot;

	private Vector3 origin;

	private Rigidbody2D Rock;

	private Vector3 ZAxis = new Vector3 (0f, 0f, 1f);
	private int position;
	private GameObject NoFricBridge;

	private GameObject curStopObject;
	private GameObject[] throwList;
	
	// Use this for initialization
	void Start () 
	{

		NoFricBridge = transform.Find ("NoFricBridge").gameObject;
		NoFricBridge.SetActive (false);

		origin = pivot.transform.position;
		curStop = 0;
		Working = false;
		throwRock = true;
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









	}

	void Throw(Rigidbody2D obj)
	{

		float deltax = obj.transform.position.x - pivot.transform.position.x;
		float deltay = obj.transform.position.y - pivot.transform.position.y;
		float diag = Mathf.Sqrt (Mathf.Pow (deltax, 2) + Mathf.Pow (deltay, 2));
		float sin = Mathf.Abs (deltay / diag);
		float cos = Mathf.Abs (deltax / diag);
		//sin-->throwing direction x component
		//cos-->throwing y component
		if (deltax >= 0f) {
			if (deltay >= 0f) {
				sin = -sin;
			}
		
		} else {
			if (deltay >= 0f) {
				sin = -sin;
				cos = -cos;
			} else {
				cos = -cos;
			}
		}
		int CounterClock = -1;
	
		int curPos;
		if (curStop != 0) {
			curPos = curStop - 1;
		} else {
			curPos = rotateSpeed.Length - 1;
		}
	
		if (rotateSpeed [curPos] > 0) {
			CounterClock = 1;
		} else {
			CounterClock = -1;
		}
	
		float throwforce = Mathf.Abs (rotateSpeed [curPos] * diag * 0.5f);
	
		float throwx = throwforce * sin * CounterClock;
		float throwy = throwforce * cos * CounterClock;
	
		obj.AddForce (new Vector2 (throwx, throwy));
	
		throwRock = false;
		


	}
	
	
	void OnTriggerEnter2D(Collider2D other)
	{


	}
	void OnTriggerStay2D(Collider2D other)
	{
		if(other.gameObject.CompareTag("PickUp")||other.gameObject.CompareTag("Player")||other.gameObject.CompareTag("RockContact"))
			
		{
			if(Working){
			
				other.transform.RotateAround (origin, ZAxis, rotateSpeed [curStop] * Time.deltaTime*0.3f);
			}
			if (throwRock) {
				Throw (other.attachedRigidbody);
			}
		}

	}
	void OnTriggerExit2D(Collider2D other)
	{
	
	}



	void Rotate(int pos)
	{	
		NoFricBridge.SetActive(false);

		this.tag = "Bridge";
		transform.RotateAround (origin, ZAxis, rotateSpeed[pos] * Time.deltaTime);
	}

}
