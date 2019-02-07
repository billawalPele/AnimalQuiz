using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Score : MonoBehaviour
{
    public int scoreValue = 0;
    public Text scoreText;

    void Awake()
    {
        scoreText = GetComponent<Text>();
        scoreValue = 0;
        scoreValue = PlayerPrefs.GetInt("ScoreValue");
    }

    // Use this for initialization
    void Start ()
    {
       
	}
	
	// Update is called once per frame
	void Update ()
    {
        scoreText.text = "Score: " + scoreValue;
        PlayerPrefs.SetInt("ScoreValue", scoreValue);
    }

    
}
