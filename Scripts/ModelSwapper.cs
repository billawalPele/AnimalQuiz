
using UnityEngine;
using Vuforia;
using System.Collections;
using System.Collections.Generic;

public class ModelSwapper : MonoBehaviour    
{
    public GameObject[] Animals;
    
   //List<GameObject> objects = new List<GameObject>();
    public int currentObject;
    private int i;

    void Start()
    {
       
        for (int i = 0; i <Animals.Length; i++){
            if (Animals[i].activeSelf)
            {
                currentObject = i;
            }
        }
    }

    public void DisplayNextObject()
    {
       
        Animals[i].SetActive(false);
        i++;
        if (i>Animals.Length)
        {
            i = 0;
        }
        Animals[i].SetActive(true);
    
        //Score.scoreValue += 10;
        

        
    }

}


