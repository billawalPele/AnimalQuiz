using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpawnItems : MonoBehaviour
{
  
    public GameObject[] spawnPositions;
    public GameObject[] spawnObjects;
    public Terrain tanah;
    public LayerMask mask;
  //  public int totalScore = 0;
    public Text timeText;
    public float timeLimit;
   // public Text totalScoreText;
    public Text text;
    public Button btnDecrease; // added reference to buttons so can disable them
    public Button btnIncrease;
    public GameObject PanelTrue;
    public Result result;
    public GameObject PanelFalse;



    public int howMany;
    public int max = 10;
    public int min = 1;
    int increasePerClick = 1;

    int currentAmount;

    //private List<int> spawns;
    public List<GameObject> animals = new List<GameObject>();


    //private List<int> spawns;
    // public Quaternion putaran = Quaternion.Euler(new Vector3(0, 30, 0));


    void Awake()
    {
        PanelFalse.gameObject.SetActive(false);
        PanelTrue.gameObject.SetActive(false);

        
    }

    // Use this for initialization
    void Start()
    {
        
        //fill possible spawn with spawn position
        animals = new List<GameObject>();
        //currentAmount = animals.Count;
        foreach (GameObject tagHewan in GameObject.FindGameObjectsWithTag("hewan"))
        {
            animals.Add(tagHewan);
        }
        //spawnObjects = GameObject.FindGameObjectsWithTag("hewan");

        Spawn();
        StartCoroutine(TimeLimit());

    }

    // Update is called once per frame
    void Update()
    {
       
      Debug.Log(animals.Count);

        
       for (int i = 0; i < animals.Count; i++)
       {

           animals[i].transform.parent = tanah.transform;
           
                //animals[i].transform.rotation = tanah.transform.rotation;

                //  animals[i].transform.rotation = tanah.transform.rotation;
           
        }
        

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
    //for panel "answer is correct" or "answer is false"
    public void CheckAnswwer()
    {

        if (animals.Count == currentAmount)
        {
            PanelTrue.gameObject.SetActive(true);
            StartCoroutine(RemoveAfterSeconds(2));
            result.totalScore = PlayerPrefs.GetInt("score") + 20;
            //result.totalScore += 20;
           result.totalScoreText.text = result.totalScore.ToString();
            StartCoroutine(SceneChanges(2));
            Debug.Log("sukses");

        }
        else if (animals.Count != currentAmount)
        {
            PanelFalse.gameObject.SetActive(true);
            StartCoroutine(RemoveAfterSeconds(2));
            Debug.Log("failed");
        }
        else
        {
            Debug.LogError("");
        }

    }

    IEnumerator RemoveAfterSeconds(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        PanelTrue.gameObject.SetActive(false);
        PanelFalse.gameObject.SetActive(false);


    }
    //random spawn object hewan 
    void Spawn( )
    {

        // animals = new List<GameObject>();

        int numberOfObjectsToSpawn = Random.Range(1, 11);
       for (int j = 0; j < numberOfObjectsToSpawn; j++)
       {
            //int objindex = Random.Range(0, spawnObjects.Length);
            // int spawnindex = Random.Range(0, spawnPositions.Length);
            GameObject objectindex = spawnObjects[Random.Range(0, spawnObjects.Length)];
            Vector3 pos = spawnPositions[Random.Range(0, spawnPositions.Length)].transform.position;
            Quaternion rot = spawnPositions[Random.Range(0, spawnPositions.Length)].transform.rotation;
            Collider[] obj;
            obj = Physics.OverlapSphere(pos, 0.5f, 1<<LayerMask.NameToLayer("hewan"));
            GameObject an = Instantiate(objectindex, pos, rot) as GameObject;
            animals.Add(an);
            foreach (Collider item in obj)
            {
                
                if (item != null)
                {
                    Debug.Log("kena");
                    animals.Remove(an);
                    Destroy(an);
                }
                if (item == null)
                {
                    continue;
                }

            }     
           
       }

    }
    IEnumerator SceneChanges(int sec)
    {
        yield return new WaitForSeconds(sec);
        PlayerPrefs.SetInt("score", result.totalScore);
       // PlayerPrefs.SetFloat("timer", timeLimit);
        SceneManager.LoadScene("LoadScene3");
    }

    IEnumerator TimeLimit()
    {
        timeText.text = Mathf.RoundToInt(timeLimit).ToString();

        yield return new WaitForSeconds(5);
       
        while (timeLimit > 0)
        {
            timeLimit -= Time.deltaTime;

            timeText.text = Mathf.RoundToInt(timeLimit).ToString();

            
            yield return null;
        }

        if (timeLimit <= 0)
        {
            SceneManager.LoadScene("LoadScene3");
            yield break;
        }


    }

}

