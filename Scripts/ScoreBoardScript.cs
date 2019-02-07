using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreBoardScript : MonoBehaviour {

    public Result result;
    //public float timeLimit;
   // public Text timeText;
	// Use this for initialization
	void Start () {
        result.totalScore = PlayerPrefs.GetInt("score");
        result.totalScoreText.text = result.totalScore.ToString();
      // timeLimit = PlayerPrefs.GetFloat("timer");
      // timeText.text =Mathf.RoundToInt(timeLimit).ToString();
    }

    public void OnMouseDown()
    {
        SceneManager.LoadScene("LoadScene1");
        //sult result = GameObject.Find("totalScoreText").GetComponent<Result>();
        PlayerPrefs.SetInt ("score", 0);
    }

    public void ExitApp()
    {
        Application.Quit();
    }
}
