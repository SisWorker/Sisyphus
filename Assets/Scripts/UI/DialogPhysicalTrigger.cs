using UnityEngine;
using System.Collections;

public class DialogPhysicalTrigger : MonoBehaviour {

	public bool isActive;
	public int dialogIndex;
	public GameObject DialogManager;
	private DialogControl dc;
	// Use this for initialization
	void Start () {
		activate (isActive);
		dc = DialogManager.GetComponent<DialogControl> ();
	}
	
	// Update is called once per frame
	void Update () {
		activate (isActive);
	}
	void activate(bool isActive){
		gameObject.SetActive (isActive);
	}
	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.name == "Player") {
			if (dialogIndex != null) {
				dc.invokeDialog (dialogIndex);
				isActive = false;
			}
		}
	}

}
