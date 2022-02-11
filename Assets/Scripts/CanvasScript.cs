using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasScript : MonoBehaviour
{
   
     public static CanvasScript Instance { get; private set; }

    void Awake(){
    Instance = this;
}
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
                    }
                }
            }
        }
    }

}
