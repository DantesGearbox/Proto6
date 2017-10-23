using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public UnityEngine.UI.Text timer;
	public UnityEngine.UI.Text doneText;
	public UnityEngine.UI.Button restartButton;

	private int initialTimerValue = 30;
	private int UITimer;
	private float discreteTimer;
	public bool playerFinishedStage = false;

	// Use this for initialization
	void Start () {
		doneText.gameObject.SetActive (false);
		UITimer = initialTimerValue;
		timer.text = "" + UITimer;
		restartButton.onClick.AddListener(RestartGame);
	}

	void Update(){
		discreteTimer += Time.deltaTime;

		if(discreteTimer > 1 && UITimer > 0 && !playerFinishedStage){
			UITimer--;
			timer.text = "" + UITimer;
			discreteTimer = 0;
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		playerFinishedStage = true;
		doneText.gameObject.SetActive (true);
	}

	void RestartGame()
	{
		SceneManager.LoadScene("main1");
	}
}
