using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player2 : MonoBehaviour
{
    public float blendSpeed = 0f;
    public float forwardSpeed = 6f;
    public float tmpSpeed = 0;
    public float angle = 0f;
    public bool startLeft = false;
    public bool startRight = false;

    public bool accLeft =false;
    public bool dccRight = false;

    public bool camPan = false;

    public bool changeAnim = true;
    public bool _stair = false;

    // Bu bölüm Finish'i açmak için
    public GameObject finish;

    //Finish zamanı kod durdurmak için
    public bool FinishStart = false;

   public Animator anim;
    // Start is called before the first frame update

  public static Player2 Instance { get; private set; }

    void Awake(){
    Instance = this;
}

void Start(){
      QualitySettings.vSyncCount = 0;
         Application.targetFrameRate = 60;
    anim = GetComponent<Animator>();
}
   void FixedUpdate(){

     if(anim.GetBool("Start") && !FinishStart)
     {
         transform.position += transform.forward*forwardSpeed*Time.fixedDeltaTime;
                
       if(startLeft){
           dccRight = false;
           // transform.position += transform.forward*15*Time.fixedDeltaTime;
           anim.SetBool("Stair",false);
           if(transform.position.x<=-7)
           { 
               
               
               
                   transform.rotation = Quaternion.Euler(0,angle,0);
                angle +=400*Time.fixedDeltaTime;
              
              
            if(angle>0){startLeft = false;

                    accLeft = true;
                }
                
               
           }
            else if(angle> -45){
                transform.rotation = Quaternion.Euler(0,angle,0);
                angle -=200*Time.fixedDeltaTime;
           }
       }

            if(startRight){
            
            accLeft = false;
            
          // transform.position += transform.forward*15*Time.fixedDeltaTime;
           
           if(transform.position.x>-3.3f)
           {
                   
                   transform.rotation = Quaternion.Euler(0,angle,0);
                angle -=400*Time.fixedDeltaTime;
              
              
    if(angle<0){startRight = false;
    dccRight = true;
    
    }
           }
            else if(angle< 45){
                transform.rotation = Quaternion.Euler(0,angle,0);
                angle +=200*Time.fixedDeltaTime;
           }
       }
     
       else
       {
             if(accLeft)
       {
           if(forwardSpeed<15){ forwardSpeed += 6*Time.fixedDeltaTime;}
          if(blendSpeed<1){blendSpeed+= 0.4f*Time.fixedDeltaTime;}
          
           anim.SetFloat("Blend",blendSpeed);
          
       }

       else if(dccRight )
       {
          if(forwardSpeed>6){ forwardSpeed -= 6*Time.fixedDeltaTime;}
          if(blendSpeed>0){blendSpeed-= 0.4f*Time.fixedDeltaTime;}
           anim.SetFloat("Blend",blendSpeed);
       }

           //transform.position += transform.forward*forwardSpeed*Time.fixedDeltaTime;
       }

     }

     else if(FinishStart)
     {
         transform.position += transform.forward*60*Time.fixedDeltaTime;
         anim.SetFloat("Blend",1);
     }
       
   }

   void Update(){
       if(anim.GetBool("Start")&& !FinishStart){
               if(_stair&&changeAnim)
       {
           anim.SetBool("Stair",true);
       }
       if(!_stair || !changeAnim)
       {
           anim.SetBool("Stair",false);
       }
 if (Input.touchCount >0)
            {
                Touch touch = Input.GetTouch(0);
                
                if (touch.phase == TouchPhase.Began)
                {
                    angle = 0;
                    startRight = false;
                    startLeft = true;
                }

                if (touch.phase == TouchPhase.Ended)
                {
                    angle =0;  
                    startLeft = false;
                    startRight = true;
                }
            }
            blendSpeed = Mathf.Clamp(blendSpeed,0.1f,1);
       }   
   }



    private void OnTriggerEnter(Collider other)
    {
         if(other.gameObject.CompareTag("Side"))
        {   Debug.Log("Girdi");
            if(changeAnim)
            {
                changeAnim = false;
            }
            else if(!changeAnim)
            {
                changeAnim = true;
            }
        }

         if(other.gameObject.CompareTag("Floor")){
            Debug.Log("Floor Collide");
            //anim.SetBool("Stair",false);
            _stair = false;
            camPan = false;
        }
        if(other.gameObject.CompareTag("Stair")){
            Debug.Log("Stair Collide");
           // anim.SetBool("Stair",true);
           _stair =true;
            camPan = true;
        }

        if(other.gameObject.CompareTag("Finish")){
            Debug.Log("Finish");
            foreach(Transform child in finish.transform){
                child.gameObject.SetActive(true); 
                 FinishStart = true;
            }
            
            StartCoroutine(WaitAndPrint());
        }

        if(other.gameObject.CompareTag("GameEnd")){
          SceneManager.LoadScene(0);
        }

    } 

    IEnumerator WaitAndPrint()
    { Debug.Log("Trennnn");
        // suspend execution for 5 seconds
        yield return new WaitForSeconds(2);
        
       finish.transform.GetChild(0).transform.position =  Vector3.MoveTowards( finish.transform.GetChild(0).transform.position,  finish.transform.GetChild(0).transform.position+ new Vector3(360,0,0), 5);
    }
}
