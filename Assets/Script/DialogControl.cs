using UnityEngine;
using System.Collections;

public class DialogControl : MonoBehaviour {

	public bool dialogOn;
	public GameObject DialogCanvas;
	// Use this for initialization

	// Update is called once per frame
	void Update () {
		if (dialogOn) {
			Time.timeScale = 0f;
			Debug.Log ("Paused");
			DialogCanvas.SetActive (true);
		} else {
			Debug.Log ("resume");
			DialogCanvas.SetActive (false);
			Time.timeScale = 1f;
		}
	
	}
	void showDialog()
	{
		dialogOn = true;
	}
	void hideDialog()
	{
		dialogOn = false;
	}
}
