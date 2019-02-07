using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Tambah : MonoBehaviour
{

    public Text text;
    public Button btnDecrease; // added reference to buttons so can disable them
    public Button btnIncrease;
    public GameObject PanelTrue;
    public GameObject PanelFalse;
  

   
 

    public int howMany;

    public int max = 10;
    public int min = 1;

    int currentAmount;
    int increasePerClick = 1;

  


    void Awake()
    {
        PanelFalse.gameObject.SetActive(false);
        PanelTrue.gameObject.SetActive(false);
      

    }

    void Start()
    {
       
       

    }

    

    // from button click event, call AdjustValue(true) if want to increase or AdjustValue(false) to decrease
    public void AdjustValue(bool increase)
    {
        // clamp current value between min-max
        currentAmount = Mathf.Clamp(currentAmount + (increase ? increasePerClick : -increasePerClick), min, max);
        text.text = currentAmount.ToString();

        // disable buttons if cannot increase/decrease
        btnDecrease.interactable = currentAmount > min;
        btnIncrease.interactable = currentAmount < max;
    }

    /*public void CheckAnswwer()
    {
        
            if ( spawnitem.animals.Count == currentAmount)
            {
                PanelTrue.gameObject.SetActive(true);
                StartCoroutine(RemoveAfterSeconds(2));
                Debug.Log("sukses");
            }
            else if (spawnitem.animals.Count != currentAmount)
            {
                PanelFalse.gameObject.SetActive(true);
                StartCoroutine(RemoveAfterSeconds(2));
                Debug.Log("failed");
            }
            else
            {
                Debug.LogError("");
            }
        }*/


    IEnumerator RemoveAfterSeconds(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        PanelTrue.gameObject.SetActive(false);
        PanelFalse.gameObject.SetActive(false);


    }
}

  

  

    /*  public void InstantiateRandomPosition(string Resource, int Amount, float AddedHeight)
      {
          var i = 0;
          float terrainHeight = 0f;
          RaycastHit hit;
          float randomPositionX, randomPositionY, randomPositionZ;
          Vector3 randomPosition = Vector3.zero;
          do
          {
              i++;
              randomPositionX = Random.Range(TerrainLeft, TerrainRight);
              randomPositionZ = Random.Range(TerrainBottom, TerrainTop);

              if (Physics.Raycast(new Vector3(randomPositionX, 9999f, randomPositionZ), Vector3.down, out hit, Mathf.Infinity, TerrainLayer))
              {
                  terrainHeight = hit.point.y;
              }

              randomPositionY = terrainHeight + AddedHeight;
              randomPosition = new Vector3(randomPositionX, randomPositionY, randomPositionZ);

              GameObject unit = Instantiate(Resources.Load(Resource, typeof(GameObject)), randomPosition, Quaternion.identity) as GameObject;
              unit.transform.eulerAngles = new Vector3(unit.transform.eulerAngles.x, Random.Range(0, 360), unit.transform.eulerAngles.z);

          } while (i < Amount);
      }*/
