using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    string levelName; 
	// Use this for initialization
	void Start () {
	    levelName = Application.loadedLevelName; 
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    public void Quit() {
        Application.Quit();
    }
    public void restartLevel() {
        Application.LoadLevel(levelName);
    }
    public void levelFailed() {
        Application.LoadLevel(levelName);
    }

    public void levelVictory() {

    }


}
