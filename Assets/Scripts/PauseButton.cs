using UnityEngine;
using System.Collections;

public class PauseButton : MonoBehaviour {

	public GameObject pauseMenuCanvas;
	public void Pause()
	{
		this.gameObject.SetActive(false);
	}
}
