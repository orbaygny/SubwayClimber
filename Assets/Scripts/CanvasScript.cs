using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CanvasScript : MonoBehaviour
{
    public  GameObject timer;
   public bool gameEnd = false;
   private float startTime;
     public static CanvasScript Instance { get; private set; }

    void Awake(){
    Instance = this;
}
    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = ("Level "+ (PlayerPrefs.GetInt("level")+1));
        transform.GetChild(3).GetChild(1).GetComponent<TextMeshProUGUI>().text = ("Level "+ (PlayerPrefs.GetInt("level")+1));
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Npc._NpcStart){
            float t = Time.time-startTime;
       // string min =((int)t/60).ToString();
        string sec = (t%60).ToString("f1");
        timer.GetComponent<TextMeshProUGUI>().text = sec;
        }
        if(Input.touchCount >0)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
            {
                foreach(Transform child in transform)
                {
                    if(child.gameObject.CompareTag("GameStart"))
                    {
                        
                        child.gameObject.SetActive(false);
                        Player2.Instance.anim.SetBool("Start",true);
                        Npc._NpcStart = true;
                        StartCoroutine(InputStarter());
                       
                    }
                }
            }
        }

        if(gameEnd){
            foreach(Transform child in transform)
                {
                    if(child.gameObject.CompareTag("GameEnd"))
                    {
                        child.gameObject.SetActive(true);
                       
                    }
                }
        }


    }

    public void RestartLevel(){
        SceneManager.LoadScene(0);
    }

   
     private IEnumerator InputStarter()
     {
         yield return new WaitForSeconds(0.50f);
          Player2.Instance.InputStarter = true;
     }
}
