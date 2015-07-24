using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {

	public GameObject platform;

	public float moveSpeed;

	public Transform[] points;

	public int pointSelection;
	public bool Moving;
	private Transform currentPoint;
	private Rigidbody2D Rbody;
	private Vector3 MovingDir;

	private Transform WallR;
	private Transform WallL;
	private int WallCount;

	// Use this for initialization
	void Start () {
		for(int i = 0; i < points.Length;i++)
		{
			points[i].GetComponent<SpriteRenderer>().enabled=false;
		}
		currentPoint = points [pointSelection];
		Moving = false;
		WallR = transform.Find ("Platform").transform.Find ("Walls").transform.Find ("WallR");
		WallL = transform.Find ("Platform").transform.Find("Walls").transform.Find("WallL");
		WallCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
		MovingDir = new Vector3(0,0,0);
		if (Moving) 
		{
			MovingDir = platform.transform.position - Vector3.MoveTowards (platform.transform.position, currentPoint.position, Time.deltaTime * moveSpeed);
			platform.transform.position = Vector3.MoveTowards (platform.transform.position, currentPoint.position, Time.deltaTime * moveSpeed);
		}
		if (platform.transform.position == currentPoint.position) 
		{
			Moving = false;
			pointSelection++;
			if(pointSelection == points.Length)
			{
				pointSelection = 0;
			}
			currentPoint = points[pointSelection];
		}

	}
	void FixedUpdate()
	{
		if (Moving) {
			if (WallCount < 50) {
				WallR.transform.position += new Vector3 (0, 0.05f, 0);
				WallL.transform.position += new Vector3 (0, 0.05f, 0);
				WallCount++;
			}
		
		} else {
			if(WallCount >0)
			{
				WallR.transform.position -= new Vector3 (0, 0.05f, 0);
				WallL.transform.position -= new Vector3 (0, 0.05f, 0);
				WallCount--;
			}
		}
	}
	public void MoveOn()
	{
		if (Moving == false) {
			Moving = true;
		}
	}
	public Vector3 GetMoveDir()
	{
		return MovingDir;
	}
}
