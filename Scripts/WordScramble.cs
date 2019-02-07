using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



[System.Serializable]
public class Word
{
    public string word; //variable huruf
    public float timeLimit; //variable timer
    //mengacak huruf
    public string GetString()
    {
        string result = word;
      
        //Random words
        while(result == word)
        {
            result = "";
            List<char> characters = new List<char>(word.ToCharArray());
            while (characters.Count > 0)
            {
                int indexChar = Random.Range(0, characters.Count - 1);
                result += characters[indexChar];

                characters.RemoveAt(indexChar);
            }
        }
 
        return result;
    }

}

public class WordScramble : MonoBehaviour {

    public Word[] words;
    public Result result;

    public GameObject[] Animals;

    public CharObject prefab;
    public Transform container;
   
    public float space;
    public float lerpSpeed = 5;
    public int currentObject;
    private int i;

    List<CharObject> charObjects = new List<CharObject>();
    CharObject firstSelected;

    public int currentWord;
    float timeLimit;
    public static WordScramble main;

    private void Awake()
    {
        main = this;
    }

    // Use this for initialization
    void Start ()
    {
       
       ShowScrambled(currentWord);
      
       
        
        for (int i = 0; i < Animals.Length; i++)
        {
            if (Animals[i].activeSelf)
            {
                currentObject = i;
            }
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        RepositionObject();
	}

    void RepositionObject ()
    {
        if(charObjects.Count==0)
        {
            return;
        }

        float center = (charObjects.Count - 1) / 2;
        for (int i =0; i<charObjects.Count; i++)
        {
            charObjects[i].rectTransfrom.anchoredPosition = Vector2.Lerp(charObjects[i].rectTransfrom.anchoredPosition,
                new Vector2((i - center) * space, 0), lerpSpeed * Time.deltaTime);

            charObjects[i].index = i;
        }
    }


    /* public void ShowScrambled()
     {
         ShowScrambled(Random.Range(0, words.Length - 1));
     }*/
    //tampilkan huruf yang teracak
    public void ShowScrambled (int index)
    {
        charObjects.Clear();
        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }

        if (index > words.Length - 1)
        {
            SceneChanges();
            //Debug.LogError("index out of range");
                return;
        }

        char[] chars = words[index].GetString().ToCharArray();
        foreach (char c in chars)
        {
            CharObject clone = Instantiate(prefab.gameObject).GetComponent<CharObject>();
            clone.transform.SetParent(container);

            charObjects.Add(clone.Init(c));
        }
        currentWord = index;
        StartCoroutine(Timelimit());
       
    }
    
    public void Swap(int indexA, int indexB)
    {
        CharObject tmpA = charObjects[indexA];
        charObjects[indexA] = charObjects[indexB];
        charObjects[indexB] = tmpA;

        charObjects[indexA].transform.SetAsLastSibling();
        charObjects[indexB].transform.SetAsLastSibling();

        CheckWord();
        
        
    }
    //memilih huruf 
    public void Select(CharObject charObject)
    {
        if(firstSelected)
        {
            Swap(firstSelected.index, charObject.index);

            //Unselect objek
            firstSelected.Select();
            charObject.Select();
        }else
        {
            firstSelected = charObject;
        }
    }

    public void UnSelect ()
    {
        firstSelected = null;
    }

    public void CheckWord ()
    {
        StartCoroutine(WaitCheckWord());
    }

   IEnumerator WaitCheckWord()
    {
      
        yield return new WaitForSeconds(1f);
        
        string word = "";
        foreach (CharObject charObject in charObjects)
        {
            word += charObject.character;
        }

        if (timeLimit <= 0)
        {
            currentWord++;
            DisplayNextObject();
            ShowScrambled(currentWord);
        }

        if (word == words[currentWord].word)
        {
            currentWord++;
            DisplayNextObject();
            result.totalScore += 10;
          
            result.totalScoreText.text = result.totalScore.ToString();
            PlayerPrefs.SetInt("score", result.totalScore);
            
            ShowScrambled(currentWord);

        }
       

    }

    public void DisplayNextObject()
    {

        Animals[i].SetActive(false);
        
        i++;
        if (i > Animals.Length)
        {
            i = 0;
        }
        if (i < Animals.Length)
        {
            Animals[i].SetActive(true);
        }
        
    }
    public void SceneChanges()
    {
       // PlayerPrefs.SetFloat("timer", timeLimit);
        SceneManager.LoadScene("LoadScene2");
    }

    IEnumerator Timelimit()
    {
        timeLimit = words[currentWord].timeLimit;
        result.timeText.text = Mathf.RoundToInt(timeLimit).ToString();

        int perWord = currentWord;
        yield return new WaitForSeconds(1f);
        while (timeLimit > 0)
        {
            if (perWord != currentWord)
            {
                yield break;
            }
            timeLimit -= Time.deltaTime;
          
            result.timeText.text = Mathf.RoundToInt(timeLimit).ToString();
           
            yield return null;
        }
        
        CheckWord();
    }

   

}
