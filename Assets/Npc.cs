using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    public static bool _NpcStart = false;
   
    Animator anim;
    Rigidbody rb;
    CapsuleCollider col;
     bool firstStart =true;

     float startPos;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        startPos = transform.position.x;
       
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
          if(startPos ==-2.5){
              col.enabled =false;
          //rb.constraints = RigidbodyConstraints.None;
          transform.rotation = Quaternion.Euler(0,30,0);
          anim.applyRootMotion = true;
          transform.position += transform.forward*40*Time.fixedDeltaTime;
          anim.SetTrigger("GetHit");
          }

           if(startPos ==-7.5){
              col.enabled =false;
          //rb.constraints = RigidbodyConstraints.None;
          transform.rotation = Quaternion.Euler(0,-30,0);
          anim.applyRootMotion = true;
          transform.position += transform.forward*40*Time.fixedDeltaTime;
          anim.SetTrigger("GetHit");
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
