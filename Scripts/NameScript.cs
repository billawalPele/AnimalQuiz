using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameScript : MonoBehaviour {

    public GameObject[] Hewan;
    private int i;
    public GameObject tombolKeluar;
    public GameObject tombolGanti;



    public void DisplayNextObject()
    {

             Hewan[i].SetActive(false);
        tombolKeluar.SetActive(false);
            
            i++;
            if (i < Hewan.Length )
            {
                Hewan[i].SetActive(true);
            }
            if (i == Hewan.Length)
            {
                tombolGanti.SetActive(false);
                tombolKeluar.SetActive(true);
            i = 0;

            Debug.Log("Hewan Habis");
            }
                
    }

    public void BacktoMenu()
    {
       
    }

  
}
