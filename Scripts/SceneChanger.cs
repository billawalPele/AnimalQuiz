using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {

    public string SceneName;
    
   public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneName);
    }

  
    public void ExitGame()
    {
        Application.Quit();
    }

}
