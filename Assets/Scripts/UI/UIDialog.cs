using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class UIDialog : MonoBehaviour {

	public GameObject DialogCanvas;
	private Text dialogText;
	private int dialogCount;
	private bool done;
	// Use this for initialization
	void Start ()
	{
		dialogCount = -1;
		dialogText = GameObject.Find ("DialogContent").GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		bool dialogOn = true;
		if (dialogCount == 0) {
			dialogText.text = "What";
		}
		else if (dialogCount == 1) {
			dialogText.text = "What?";
		}
		else if (dialogCount == 2) {
			dialogText.text = "What??";
		}
		else if (dialogCount == 3) {
			dialogText.text = "What???";
		}
		else if (dialogCount == 4) {
			dialogText.text = "What????";
		} else {
			dialogOn = false;
		}
		DialogCanvas.SetActive(dialogOn);
	
	}
	public void Proceed()
	{
		dialogCount++;
	}
}
