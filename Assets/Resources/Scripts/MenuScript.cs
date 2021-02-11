using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class MenuScript : MonoBehaviour {
    void Start (){
        //Display previous highscore if it exists
        if (PlayerPrefs.HasKey("HighScore")){
            TMP_Text highScoreText = GameObject.Find("HighScoreText").GetComponent<TMP_Text>();
            highScoreText.text = "Previous Highscore: " + PlayerPrefs.GetInt("HighScore");
        }
    }

    public void StartGame(){
        SceneManager.LoadScene("GameScene");
    }
}
