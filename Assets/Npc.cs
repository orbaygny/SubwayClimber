using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    public static bool _NpcStart = false;
   
   public LayerMask ignoreLayer;
    Animator anim;
    Rigidbody rb;
    CapsuleCollider col;
     bool firstStart =true;

     public bool faceFront =true;

     float startPos;

    // Start is called before the first frame update
    void Awake(){
         _NpcStart = false;
    }
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        startPos = transform.position.x;
        switch(startPos){
            case -2.5f:
            transform.rotation = Quaternion.Euler(0,180,0);
            break;

            case -7.5f:
            transform.rotation = Quaternion.Euler(0,0,0);
            break;
        }
       
    }
    void Update(){
        if(_NpcStart){rb.constraints = RigidbodyConstraints.None; rb.constraints = RigidbodyConstraints.FreezeRotation;}
           RaycastHit hit;
        float distance = 100f;
        if(Physics.Raycast(transform.position, Vector3.down, out hit, distance,~ignoreLayer)) {
          if(hit.distance>0){  transform.position = new Vector3(transform.position.x,hit.point.y,transform.position.z); }
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(_NpcStart){
        transform.position += transform.forward*6*Time.fixedDeltaTime;

        // Bölüm başlangıcında ilk defa hareket animasyonu çalıştırmak için
        if(firstStart){anim.SetBool("Start",true); firstStart = false; }
            
            
           
        }
        
    }


   void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player")){
      switch(faceFront)
      {
          case true:
          
          if(startPos ==-2.5){
              col.enabled =false;
          //rb.constraints = RigidbodyConstraints.None;
          transform.rotation = Quaternion.Euler(0,230,0);
          anim.applyRootMotion = true;
          transform.position += new Vector3(0,0,1)*40*Time.fixedDeltaTime;
          anim.SetTrigger("HitBack");
          }

           if(startPos ==-7.5){
              col.enabled =false;
          //rb.constraints = RigidbodyConstraints.None;
          transform.rotation = Quaternion.Euler(0,-30,0);
          anim.applyRootMotion = true;
          transform.position += transform.forward*40*Time.fixedDeltaTime;
          anim.SetTrigger("HitFront");
          }
           
          break;

          case false:
            if(transform.position.x<collision.gameObject.transform.position.x){
                Debug.Log("ColliderGirdi");
                col.enabled = false;
                anim.applyRootMotion = true;
                anim.SetTrigger("HitBack");
            }
            else
            {
                   col.enabled =false;
          //rb.constraints = RigidbodyConstraints.None;
          transform.rotation = Quaternion.Euler(0,60,0);
          anim.applyRootMotion = true;
          transform.position += transform.forward*40*Time.fixedDeltaTime;
          anim.SetTrigger("HitFront");
            }

          break;
      }
    }

    }

   private void OnTriggerEnter(Collider other){

       if(other.gameObject.CompareTag("Stair")){
          
           if(startPos==-7.5f){

               anim.SetBool("Start",false);
           }
       }
       
       if(other.gameObject.CompareTag("Floor")){
           if(startPos==-7.5f){

               anim.SetBool("Start",true);
           }
       }
   }


 
}
