using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    
    public int score = 0;
    public int[] highScores = new int[5];
    int highScore;
    string highScoreKey = "";

    void Start()
    {
        //Get the highScore from player prefs if it is there, 0 otherwise.

        for (int i = 0; i < highScores.Length; i++)
        {
            highScoreKey = "brickScore" + (i+1).ToString();
            highScores[i] = PlayerPrefs.GetInt(highScoreKey, 0);
        }
        
    }


    void Update(){
        GameObject.FindGameObjectWithTag("ScoresText").GetComponent<Text>().text = highScores[0].ToString();
        GameObject.FindGameObjectWithTag("ScoresText2").GetComponent<Text>().text = highScores[1].ToString();
        GameObject.FindGameObjectWithTag("ScoresText3").GetComponent<Text>().text = highScores[2].ToString();
        GameObject.FindGameObjectWithTag("ScoresText4").GetComponent<Text>().text = highScores[3].ToString();

    }

    Text scores_txt;

    public void OnDisable(){
         
         //If our scoree is greter than highscore, set new higscore and save.
         for (int i = 0; i<highScores.Length; i++){
 
             //Get the highScore from 1 - 5
             highScoreKey = "brickScore"+(i+1).ToString();
             highScore = PlayerPrefs.GetInt(highScoreKey,0);

             if(score>highScore){
                 int temp = highScore;
                PlayerPrefs.SetInt(highScoreKey, score);
                 score = temp;
             }
         }
     }
 
 }


