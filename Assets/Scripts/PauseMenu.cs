using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

	public bool isPaused;
	public GameObject pauseMenuCanvas;
	// Update is called once per frame
	void Update ()
	{
		if (isPaused) {
			pauseMenuCanvas.SetActive (true);
			Time.timeScale = 0f;
		} else {
			pauseMenuCanvas.SetActive (false);
			Time.timeScale = 1f;
		}

	}
	public void Pause()
	{
		isPaused = true;
	}
	public void Resume()
	{
		isPaused = false;
	}
	public void MainMenu()
	{
		Application.LoadLevel ("Menu");
	}
	public void Quit()
	{
		Debug.Log ("Game Exit");
		Application.Quit ();
	}

}
