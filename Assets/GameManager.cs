using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<GameObject> levels = new List<GameObject>();

[Space]
    [Space]
    public GameObject CurrentLevel;
    public bool isTesting = false;

    
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
    /*public IEnumerator LevelUp()
    {
       
    }*/
}
