using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void LevelSelect()
	{
		Application.LoadLevel ("LevelSelect");
	}
    public void loadLevel(string s) {
        Application.LoadLevel(s);
    }


	public void Quit()
	{
		Debug.Log ("Game Exit");
		Application.Quit ();
	}
}
