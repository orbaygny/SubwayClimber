using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using MoreMountains.NiceVibrations;
//using UnityEngine.UIElements;

public class Player2 : MonoBehaviour
{
    public LayerMask ignoreLayer;
    public GameObject trail;
    public bool failed = false;
    public int health = 3;

    public GameObject hp;
    Rigidbody rb;
    public Vector3 posClamp;
    public float blendSpeed = 0f;
    public float forwardSpeed = 20f;
    public float tmpSpeed = 0;
    public float angle = 0f;
    public bool startLeft = false;
    public bool startRight = false;

    public bool accLeft =false;
    public bool dccRight = false;

    public bool camPan = false;
    public bool r_camPan = false;

    public bool changeAnim = true;
    public bool _stair = false;

    public bool isHold = false;

    public bool inTrain = false;

    // Bu bölüm Finish'i açmak için
    public GameObject finish;

    //Finish zamanı kod durdurmak için
    public bool FinishStart = false;

   public Animator anim;
   public Vector3 moveVector;

   public bool InputStarter = false;

   
   public SkinnedMeshRenderer [] playerMeshes;

    // Start is called before the first frame update

  public static Player2 Instance { get; private set; }

    void Awake(){
    Instance = this;
    trail.SetActive(false);
}

void Start(){
    //Physics.gravity = new Vector3(0, -100F, 0);
     moveVector = new Vector3 (0,0,1);
      QualitySettings.vSyncCount = 0;
         Application.targetFrameRate = 60;
    anim = GetComponent<Animator>();
    rb = GetComponent<Rigidbody>();
}
   void FixedUpdate(){

     if(anim.GetBool("Start") && !FinishStart && !anim.GetBool("End"))
     {
         rb.velocity = moveVector*forwardSpeed;
        }

      if(FinishStart && !anim.GetBool("End"))
     {
         rb.rotation = Quaternion.Euler(0,0,0);
         //transform.position += transform.forward*90*Time.fixedDeltaTime;
         rb.velocity = transform.forward*80;
         anim.SetFloat("Blend",1);
        if(Camera.main.fieldOfView<90){ Camera.main.fieldOfView += 20 *Time.fixedDeltaTime;}
     }
       
   }

   void Update(){

       if(inTrain){transform.rotation = Quaternion.Euler(0,0,0); anim.SetBool("Start",false);}
       RaycastHit hit;
        float distance = 100f;
        if(Physics.Raycast(transform.position, Vector3.down, out hit, distance,~ignoreLayer)) {
        
        
          
     /*     
      * Get the location of the hit.
      * This data can be modified and used to move your object.
      */
        if(hit.distance>0 )
        {   
           transform.position = new Vector3(transform.position.x,hit.point.y,transform.position.z);
        }
 }
       posClamp = transform.position;
       posClamp.x = Mathf.Clamp(transform.position.x,-7.5f,-2.5f);
       transform.position =posClamp;

      

      
       if(anim.GetBool("Start")&& !FinishStart ){
               if(_stair&&changeAnim)
       {
           anim.SetBool("Stair",true);
       }
       if(!_stair || !changeAnim)
       {
           anim.SetBool("Stair",false);
       }

       if(startLeft)
       {
            if(transform.position.x<=-7.5f){moveVector.x = 0; transform.rotation = Quaternion.Euler(0,0,0);  } // Basılı tutma sırasında karşıya geçme işleminin tamamlanmna kontrolü
             if(forwardSpeed<40)  {forwardSpeed += 10*Time.fixedDeltaTime;} // Hız artışını sağlayan kontrol
             if(blendSpeed>0){blendSpeed+= 0.4f*Time.deltaTime;}
             if(CameraFollow.Instance.offset.z>-20){CameraFollow.Instance.offset.z-= 20*Time.deltaTime;}
             if(angle>-30 && transform.position.x>-7.5f)
             {
                 Debug.Log("Rotate Left");
                 transform.rotation = Quaternion.Euler(0,angle,0);
                 angle -= 400*Time.deltaTime;
             }
                 anim.SetFloat("Blend",blendSpeed);
       }

       if(startRight){
            if(transform.position.x>=-2.59999999f){moveVector.x = 0; transform.rotation = Quaternion.Euler(0,0,0); /*startRight =false;*/} 
               if(forwardSpeed>20)  {forwardSpeed -= 10*Time.fixedDeltaTime;}
                if(blendSpeed>0){blendSpeed-= 0.4f*Time.deltaTime;}
               if(CameraFollow.Instance.offset.z<-15){CameraFollow.Instance.offset.z+= 20*Time.deltaTime;}
                 if(angle<30 && transform.position.x<-2.5999999f)
             {
                 Debug.Log("Rotate Right");
                 transform.rotation = Quaternion.Euler(0,angle,0); // Sorunun kaynağı
                 angle += 400*Time.deltaTime;
             }
                 anim.SetFloat("Blend",blendSpeed);
       }
         /*if(InputStarter){
             switch(isHold)
         {
             case true:
             if(transform.position.x<=-7.5f){moveVector.x = 0; transform.rotation = Quaternion.Euler(0,0,0);  } // Basılı tutma sırasında karşıya geçme işleminin tamamlanmna kontrolü
             if(forwardSpeed<40)  {forwardSpeed += 10*Time.fixedDeltaTime;} // Hız artışını sağlayan kontrol
             if(blendSpeed>0){blendSpeed+= 0.4f*Time.deltaTime;}
             if(CameraFollow.Instance.offset.z>-25){CameraFollow.Instance.offset.z-= 20*Time.deltaTime;}
             if(angle>-30 && transform.position.x>-7.5f)
             {
                 Debug.Log("Rotate Left");
                 transform.rotation = Quaternion.Euler(0,angle,0);
                 angle -= 400*Time.deltaTime;
             }
                 anim.SetFloat("Blend",blendSpeed);

                 //if(forwardSpeed>30 &&transform.position.x == -7.5f ) { trail.SetActive(true); }
                 // if(forwardSpeed<30 ) { trail.SetActive(false);}
       
             break;

             case false:
              if(transform.position.x>=-2.5f){moveVector.x = 0; transform.rotation = Quaternion.Euler(0,0,0); } 
               if(forwardSpeed>20)  {forwardSpeed -= 10*Time.fixedDeltaTime;}
                if(blendSpeed>0){blendSpeed-= 0.4f*Time.deltaTime;}
               if(CameraFollow.Instance.offset.z<-15){CameraFollow.Instance.offset.z+= 20*Time.deltaTime;}
                 if(angle<30 && transform.position.x<-2.5f)
             {
                 Debug.Log("Rotate Right");
                 transform.rotation = Quaternion.Euler(0,angle,0); // Sorunun kaynağı
                 angle += 400*Time.deltaTime;
             }
                 anim.SetFloat("Blend",blendSpeed);

                  //if(forwardSpeed>30 &&transform.position.x == -2.5f ) { trail.SetActive(true); }
                  //if(forwardSpeed<30 ) { trail.SetActive(false);}
             break;
         }
         }*/
 if (Input.touchCount >0 && !inTrain && !anim.GetBool("End"))
            {
                Touch touch = Input.GetTouch(0);
                Debug.Log("FirstStart");
                if (touch.phase == TouchPhase.Began)
                {
                    isHold = true;
                    angle = 0;
                    startRight = false;
                    startLeft = true;
                   moveVector.x = -0.75f;
                   trail.SetActive(false);
                }

                if (touch.phase == TouchPhase.Ended)
                {
                    isHold = false;
                    angle =0;  
                    startLeft = false;
                    startRight = true;
                    moveVector.x = 0.75f;
                    trail.SetActive(false);
                }
            }
            blendSpeed = Mathf.Clamp(blendSpeed,0.1f,1);
       }   
       
      if(transform.position.x<=-4){
                changeAnim = false;

            }
            else if(transform.position.x>-4){
                
                changeAnim = true;
            }
   }

     void OnCollisionEnter(Collision collision){
         if(collision.gameObject.CompareTag("NPC") && !FinishStart){
               
                   
                
                
                  if(health <= 1)
             {     
                    anim.SetBool("End",true);
                     hp.transform.GetChild(2).GetChild(0).gameObject.SetActive(true);
                    hp.transform.GetChild(2).GetComponent<Image>().color = new Color32(255,255,255,60);
                  anim.SetTrigger("Struggle");
                  CanvasScript.Instance.transform.GetChild(2).gameObject.SetActive(true);
                  trail.SetActive(false);
                  MMVibrationManager.Haptic(HapticTypes.Failure);
             }
             else
             {
                 anim.SetTrigger("Struggle");
                 hp.transform.GetChild(3-health).GetChild(0).gameObject.SetActive(true);
                 hp.transform.GetChild(3-health).GetComponent<Image>().color = new Color32(255,255,255,60);
                 
                 health--;
                 forwardSpeed = 20;
                 blendSpeed = 0;
                 MMVibrationManager.Haptic(HapticTypes.MediumImpact);

             }
             
         }
          if(collision.gameObject.CompareTag("GameEnd")){
        inTrain = true;
          anim.SetBool("Start",false);
          FinishStart = false;
          rb.velocity = Vector3.zero;
         // transform.parent = train;
          Train.Instance.closeDoor = true;
          trail.SetActive(false);
        }
     }

    


    private void OnTriggerEnter(Collider other)
    {
      
         if(other.gameObject.CompareTag("Floor")){
           
            //anim.SetBool("Stair",false);
            moveVector.z = 1;
            _stair = false;
            camPan = false;
            r_camPan = false;
        }
        if(other.gameObject.CompareTag("Stair")){
           
           // anim.SetBool("Stair",true);
           moveVector.z = 2;
           _stair =true;
            camPan = true;
        }
        if(other.gameObject.CompareTag("r_Stair"))
        {
            _stair =true;
            r_camPan = true;
        }

        if(other.gameObject.CompareTag("Tunel")){
            
            finish.transform.GetChild(0).gameObject.SetActive(true);
        }

        if(other.gameObject.CompareTag("Finish")){
            
             anim.SetBool("Stair",false);
            camPan = false;
            transform.position = new Vector3(-5,transform.position.y,transform.position.z);
            FinishStart = true;
            trail.SetActive(true);
            
           //finish.transform.GetChild(0).gameObject.SetActive(true);
            
          
        }

       

    } 

    public void CloseMeshes()
    {
        foreach(SkinnedMeshRenderer mesh in playerMeshes)
        {
            mesh.enabled = false;
        }
    }
}
