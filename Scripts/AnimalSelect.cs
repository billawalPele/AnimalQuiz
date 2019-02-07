using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;





public class AnimalSelect : MonoBehaviour {


    public GameObject[] hewan;
    public GameObject[] questions;
    public Result result;
  //  public Text ScoreText;
    public GameObject PanelTrue;
    public GameObject PanelFalse;
    public Text timeText;
    

    private int objek;
  //  public int totalScore = 0;
    public float timeLimit;


   

    public List<GameObject> listQuestions = new List<GameObject>();
    //public GameObject objNameText;

    void Awake()
    {
        PanelFalse.gameObject.SetActive(false);
        PanelTrue.gameObject.SetActive(false);


    }

    void Start()
    {
        result.totalScore = PlayerPrefs.GetInt("score");
        listQuestions = new List<GameObject>();
        RandomQuestion();
        StartCoroutine(TimeLimit());
    }

    // Update is called once per frame
    void Update ()
    {
        
        TouchObject();
    }

   //merandom pertanyaan yang keluar
   void RandomQuestion()
    {
        GameObject canvas = GameObject.Find("Panel");
        objek = Random.Range(0, questions.Length);
        GameObject currentText = questions[objek];

        Vector2 pos = new Vector2(18, -47);
        GameObject pertanyaan = Instantiate(currentText, pos, Quaternion.identity) as GameObject;
        pertanyaan.transform.SetParent(canvas.transform, false);
        listQuestions.Add(pertanyaan);
    }

    //select objek hewan
    void TouchObject()
    {
       
        if (Input.GetMouseButtonDown(0))
        {

           // Collider[] ob = Physics.OverlapSphere(transform.position, 1, 1 << LayerMask.NameToLayer("hewan"));
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
                if (Physics.Raycast(ray, out hit))
                {

                        if (hit.collider.name == hewan[objek].name)
                        {
                            PanelTrue.gameObject.SetActive(true);
                            StartCoroutine(RemoveAfterSecond(2));
                            result.totalScore = PlayerPrefs.GetInt("score") + 10;
                            result.totalScoreText.text = result.totalScore.ToString();
                            Debug.Log("kena" + hewan[objek].name);
                            StartCoroutine(ChangeScenes());
                        }
                        else
                        {
                            PanelFalse.gameObject.SetActive(true);
                            StartCoroutine(RemoveAfterSecond(2));
                            Debug.Log("Gagal");
                        }
                    
                    
                        /*if (hit.collider.tag == "Jerapah" || questions[1] == hewan[1])
                        {
                            Debug.Log("kena" + hewan[1].name);
                        }
                    
                   
                        if (hit.collider.tag == "Harimau" || questions[2] == hewan[2])
                        {
                            Debug.Log("kena" + hewan[2].name);
                        }
                    
                   
                        if (hit.collider.tag == "Gajah" || questions[3] == hewan[3])
                        {
                            Debug.Log("kena" + hewan[3].name);
                        }*/
                    
    
                }
        }
    }

    IEnumerator ChangeScenes()
    {
        yield return new WaitForSeconds(3);
        PlayerPrefs.SetInt("score", result.totalScore);
        //PlayerPrefs.SetFloat("timer", timeLimit);
        SceneManager.LoadScene("ScoreBoard");
    }

    IEnumerator RemoveAfterSecond(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        PanelTrue.gameObject.SetActive(false);
        PanelFalse.gameObject.SetActive(false);
    }

    IEnumerator TimeLimit()
    {
        timeText.text = Mathf.RoundToInt(timeLimit).ToString();

        yield return new WaitForSeconds(1.5f);

        while (timeLimit > 0)
        {
            timeLimit -= Time.deltaTime;

            timeText.text = Mathf.RoundToInt(timeLimit).ToString();

           

            yield return null;
        }
        if (timeLimit <= 0)
        {
            PanelFalse.gameObject.SetActive(true);

            SceneManager.LoadScene("ScoreBoard");
            yield break;
        }

        
    }

}
