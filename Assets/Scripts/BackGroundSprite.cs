using UnityEngine;
using System.Collections;

public class BackGroundSprite : MonoBehaviour {

    private GameObject Camera;
    private Camera cam;
    private float relativeScale;
    private float z;
	// Use this for initialization
	void Start () {
        Camera = GameObject.Find("Main Camera");
        cam = Camera.gameObject.GetComponent<Camera>();
        relativeScale = transform.localScale.x / cam.orthographicSize;
        z = transform.position.z;
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(cam.transform.position.x,cam.transform.position.y,z);
        transform.localScale = new Vector3(cam.orthographicSize * relativeScale, cam.orthographicSize * relativeScale, cam.orthographicSize * relativeScale);
	}
}
