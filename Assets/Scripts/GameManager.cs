using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	//private int currentLevel;
	public int score = 0;

	//public float currentTime = 15.0f;
	public GameObject player;
	//public GameObject mainCanvas;
	//public Text mainTimeDisplay;
	//public Text mainScoreDisplay;
	//public GameObject gameOverCanvas;

	public static GameManager gm;

	// Use this for initialization
	void Start () {
		if (gm == null) {
			gm = GetComponent<GameManager>();
		}

		if (player == null) {
			player = GameObject.FindGameObjectWithTag("Player");
		}

		//mainCanvas.SetActive (true);
		//gameOverCanvas.SetActive (false);

		//mainTimeDisplay.text = currentTime.ToString ("0.0");
	}

	// Update is called once per frame
	void Update () {
		//Debug.Log (currentTime);
		//currentTime -= Time.deltaTime;
		//mainTimeDisplay.text = currentTime.ToString ("0.0");

		/*if (currentTime <= 0) {
			mainTimeDisplay.text = "0.0";
			gameOverCanvas.SetActive(true);
			player.SetActive(false);
		}*/
	}

	public void CollectCake (int value){
		score += value;
		//mainScoreDisplay.text = score.ToString();
		Debug.Log (score);
	}
	/*
	public void CompleteLevel (){
		Application.LoadLevel (++currentLevel);
	}*/
}
