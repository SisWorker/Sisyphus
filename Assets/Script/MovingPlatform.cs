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
	private Rigidbody2D rb;
	// Use this for initialization
	void Start () {
		currentPoint = points [pointSelection];
		Moving = false;
		WallR = transform.Find ("Platform").transform.Find ("WallR");
		WallL = transform.Find ("Platform").transform.Find ("WallL");
		rb = transform.Find ("Platform").GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Moving) 
		{
			rb.MovePosition (Vector3.MoveTowards (platform.transform.position, currentPoint.position, Time.deltaTime * moveSpeed));
			//Debug.Log("1 "+ MovingDir);
			//Debug.Log("2 "+ platform.transform.position);
		}
		if (platform.transform.position == currentPoint.position) 
		{
			Moving = false;
			WallsDown ();
			pointSelection++;
			if(pointSelection == points.Length)
			{
				pointSelection = 0;
			}
			currentPoint = points[pointSelection];
		}
	}
	void WallsUp()
	{
			WallR.localScale += new Vector3(0, 1.7F, 0);
			WallL.localScale += new Vector3(0, 1.7F, 0);
	}
	void WallsDown()
	{
		WallR.localScale += new Vector3(0, -1.7F, 0);
		WallL.localScale += new Vector3(0, -1.7F, 0);
	}
	public void MoveOn()
	{
		if (Moving == false) {
			Moving = true;
			WallsUp ();
		}
	}
	public Vector3 GetMoveDir()
	{
		return MovingDir;
	}
}
