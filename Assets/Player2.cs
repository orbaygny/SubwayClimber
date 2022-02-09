using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    public float blendSpeed = 0f;
    public float forwardSpeed = 3f;
    public float tmpSpeed = 0;
    public float angle = 0f;
    public bool startLeft = false;
    public bool startRight = false;

    public bool accLeft =false;
    public bool dccRight = false;

   public Animator anim;
    // Start is called before the first frame update

  public static Player2 Instance { get; private set; }

    void Awake(){
    Instance = this;
}

void Start(){
    anim = GetComponent<Animator>();
}
   void FixedUpdate(){

     //sssss
                
       if(startLeft){

            transform.position += transform.forward*15*Time.fixedDeltaTime;
           
           if(transform.position.x<=-6.3)
           { 
               
               
               if(angle<0){
                   transform.rotation = Quaternion.Euler(0,angle,0);
                angle +=400*Time.fixedDeltaTime;
               }
              
            else{startLeft = false;

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
           transform.position += transform.forward*15*Time.fixedDeltaTime;
           
           if(transform.position.x>-3.3f)
           {
                    if(angle>0){
                   transform.rotation = Quaternion.Euler(0,angle,0);
                angle -=400*Time.fixedDeltaTime;
               }
              
    else{startRight = false;
    dccRight = true;}
           }
            else if(angle< 45){
                transform.rotation = Quaternion.Euler(0,angle,0);
                angle +=200*Time.fixedDeltaTime;
           }
       }
     
       else
       {
             if(accLeft && forwardSpeed<15)
       {
           forwardSpeed += 2*Time.fixedDeltaTime;
           blendSpeed+= 0.2f*Time.fixedDeltaTime;
           anim.SetFloat("Blend",blendSpeed);
          
       }

       else if(dccRight && forwardSpeed>6)
       {
           forwardSpeed -= 2*Time.fixedDeltaTime;
           blendSpeed-= 0.2f*Time.fixedDeltaTime;
           anim.SetFloat("Blend",blendSpeed);
       }

           transform.position += transform.forward*forwardSpeed*Time.fixedDeltaTime;
       }

       
   }

   void Update(){

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
            
           /* if(startLeft){

            transform.position += transform.forward*15*Time.deltaTime;
           
           if(transform.position.x<=-7.5)
           { 
               transform.rotation = Quaternion.Euler(0,0,0);
               startLeft = false;
               
               angle =0;
           }
            else if(angle> -45){
                transform.rotation = Quaternion.Euler(0,angle,0);
                angle -=200*Time.fixedDeltaTime;
           }
       }

            if(startRight){
            
           transform.position += transform.forward*15*Time.deltaTime;
           if(transform.position.x>-2.5f)
           {
               transform.rotation = Quaternion.Euler(0,0,0);
               
                angle =0;
               startRight = false;
           }
            else if(angle< 45){
                transform.rotation = Quaternion.Euler(0,angle,0);
                angle +=200*Time.fixedDeltaTime;
           }
       }

       else
       {
           transform.position += transform.forward*forwardSpeed*Time.deltaTime;
       }*/
   }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Floor")){
            Debug.Log("Floor Collide");
            anim.SetBool("Stair",false);
        }
        if(other.gameObject.CompareTag("Stair")){

            anim.SetBool("Stair",true);
        }
    }
}
