using UnityEngine;
using System.Collections;

public class SkyControll : MonoBehaviour {
	
	public float FloatSpeed;
	public float RelativeSpeedX;
	public float RelativeSpeedY;
	public string LoadName;
	
	public bool hasCreatedCopyL;
	public bool hasCreatedCopyR;
	
	public bool canCreateCopy;
	private bool withPlayer;
	
	private float CopyDistance;
	private float scale;
	private GameObject Camera;
	private GameObject CopyL;
	private GameObject CopyR;
	private Vector3 pos;
	
	protected Vector3 Offset;
	protected float OffsetX;
	protected float OffsetY;
	protected float CameraLastX;
	protected float CameraLastY;
    public GameObject Parent;
	// Use this for initialization
	void Start ()
	{
        Debug.Log("Created");
        Parent = GameObject.Find("SkySeperate");
        transform.parent = Parent.transform;
		Camera = GameObject.Find ("Player");
        LoadName = name;
		CameraLastX = Camera.transform.position.x;
		CameraLastY = Camera.transform.position.y;
		canCreateCopy = false;
        RelativeSpeedY = 1;
		scale = transform.localScale.x;
        
    }
	
	// Update is called once per frame
	void LateUpdate () 
	{
		
		Float ();
		Follow ();
		Undraw ();
		Detect ();
		
		transform.position = pos;
		
	}
	
	void Follow ()
	{
		OffsetX = (CameraLastX - Camera.transform.position.x) * RelativeSpeedX ;
		OffsetY = (CameraLastY - Camera.transform.position.y) * RelativeSpeedY;
		
		Offset = new Vector3 (-OffsetX, -OffsetY , 0f);
		
		pos = pos + Offset;
	}
	
	void Float()
	{
		pos = (transform.position + new Vector3 ((FloatSpeed/2*Time.deltaTime), 0, 0));
	}
	
	
	void Detect()
	{
		
		canCreateCopy = withPlayer;
		
		
		//if about to use up horizontal distance, copy
		if(canCreateCopy)
		{
            float distance = transform.position.x - Camera.transform.position.x;
            if ((distance >-100f||distance<0) && (!hasCreatedCopyL))
				// if player is going left
			{
				
				CopyL = (GameObject)Instantiate(Resources.Load(LoadName.Contains("(Clone)")?LoadName.Remove(LoadName.Length-7):LoadName), new Vector3( (transform.position.x - (98.9f*scale)),(transform.position.y) ,transform.position.z),transform.rotation);
                hasCreatedCopyL =true;
				
				SkyControll CopyScript =CopyL.GetComponent<SkyControll>();
				CopyScript.hasCreatedCopyL=false;
			}
			
			if ((distance >0f||distance<100)&&(!hasCreatedCopyR))
				//if player is going right
			{
				CopyR= (GameObject)Instantiate(Resources.Load(LoadName.Contains("(Clone)") ? LoadName.Remove(LoadName.Length - 7) : LoadName), new Vector3( (transform.position.x + (98.9f*scale)),(this.transform.position.y) ,transform.position.z),transform.rotation);
                hasCreatedCopyR =true;
				
				SkyControll CopyScript =CopyR.GetComponent<SkyControll>();
				CopyScript.hasCreatedCopyR=false;
			}
		}
		
		CameraLastX=Camera.transform.position.x;
		CameraLastY=Camera.transform.position.y;
	}
	
	void Undraw()
	{
        if ((hasCreatedCopyR)&&(CopyR!=null))
		    {
			    if ((CopyR.transform.position.x - Camera.transform.position.x) > 500)
			    {
                    Destroy(CopyR);
				    hasCreatedCopyR=false;
			    }
		    }
		    if ((hasCreatedCopyL)&&(CopyL!=null))
		    {
			    if ((CopyL.transform.position.x - Camera.transform.position.x) < -500)
			    {
                    Destroy(CopyL);
                    hasCreatedCopyL =false;
			    }
		    }
		
		
	}
	
	
	void OnTriggerStay2D(Collider2D other)
	{
		
		if (other.tag == "Player") 
		{
			//			Debug.Log("meetplayer");
			withPlayer=true;
		}
		
	}
	
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Player") 
		{
			withPlayer= false;
			//canCreateCopy=true;
		}
	}
	
}
