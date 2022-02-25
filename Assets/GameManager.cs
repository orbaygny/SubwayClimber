using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ElephantSDK;

public class GameManager : MonoBehaviour
{
    public List<GameObject> levels = new List<GameObject>();

[Space]
    [Space]
    public GameObject CurrentLevel;
    public bool isTesting = false;


   public static bool apkStart = true;
   public static bool apkSuccess = true;
    public static bool apkFail = true;

    
    void Awake()
    {
        
        if (isTesting == false)
        {
            if (levels.Count == 0)
            {
                foreach (Transform level in transform)
                {
                    levels.Add(level.gameObject);
                }
            }
            CurrentLevel = levels[PlayerPrefs.GetInt("level") % levels.Count];
            levels[PlayerPrefs.GetInt("level") % levels.Count].SetActive(true);
        }
        else
        {
            CurrentLevel.SetActive(true);
        }

        apkStart = true;
        apkSuccess = true;
        apkFail = true;
    }
    
     public void NextLevel()
    {
         if ((levels.IndexOf(CurrentLevel) + 1) == levels.Count)
        {
            PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level") + 1);
          
            //  GameHandler.Instance.Appear_TransitionPanel();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            CurrentLevel = levels[(PlayerPrefs.GetInt("level") + 1) % levels.Count];
           
            levels[(PlayerPrefs.GetInt("level")) % levels.Count].SetActive(false);
            PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level") + 1);
            
            levels[PlayerPrefs.GetInt("level") % levels.Count].SetActive(true);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    
    public static void ApkGameStart()
    {
        if(apkStart)
        {
             Elephant.LevelStarted(PlayerPrefs.GetInt("level")+1);
            apkStart = false;
        }
    }

   public static void ApkGameSuccess()
    {
        if(apkSuccess)
        {   
            Elephant.LevelCompleted(PlayerPrefs.GetInt("level")+1);
            apkSuccess = false;

        }
    }

    public static void ApkGameFail()
    {
        if(apkFail)
        {
            Elephant.LevelFailed(PlayerPrefs.GetInt("level")+1);
            apkFail = false;
        }
    }
}
