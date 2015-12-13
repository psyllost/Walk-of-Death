﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {

	public int high, score;

	//public List<Image> lives = new List<Image>(3);

	Text txt_score, txt_high, txt_level;
	
	// Use this for initialization
	void Start () 
	{
		txt_score = GetComponentsInChildren<Text>()[1];
		txt_high = GetComponentsInChildren<Text>()[0];
        txt_level = GetComponentsInChildren<Text>()[2];

	}
	
	// Update is called once per frame
	void Update () 
	{

        high = PlayerPrefs.GetInt("brickScore1", 0); ;

        // update score text
        score = GameManager.score;
		txt_score.text = "Score\n" + score;
		txt_high.text = "High Score\n" + high;


	}


}
