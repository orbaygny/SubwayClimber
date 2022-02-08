using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    #region  Ddeğişkenler
    // Merdivenden çıkış değerleri
    public float forwardSpeed = 0f;
    public float upwardSpeed = 0f;

    public float angle;
    public float y; // Demo Sonrası Sil

   

    // Jump Bool
    public bool jump;

    public bool acceleration;
    #endregion

    #region  Component Section
     Animator animator;

    #endregion
    /// ABC



    public static Player Instance { get; private set; }

    void Awake(){
    Instance = this;
}
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        
    }

    void Update()
    {
        if (Input.touchCount >0)
            {
                Touch touch = Input.GetTouch(0);
                

                if (touch.phase == TouchPhase.Began)
                {
                    angle = -90;
                    jump = true;
                    acceleration = true;

                }

                if (touch.phase == TouchPhase.Ended)
                {
                    angle = 90;
                    jump = true;
                    acceleration = false;
                }
            }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();

        
    }



    
    private void Movement()
    {   
        
        if(jump)
        {   
            y = transform.position.y;
            animator.SetTrigger("Jump");
            animator.applyRootMotion = true;
            transform.rotation = Quaternion.Euler(0,angle,0);
            
            jump = false;
        }
        
        else if(!jump)
        {
            // Merdivenden Yukarı Çıkış
        transform.position += transform.forward *forwardSpeed*Time.fixedDeltaTime;
        transform.position += transform.up*upwardSpeed*Time.fixedDeltaTime;
        }
        if(acceleration&& animator.hasRootMotion== false)
        {
            forwardSpeed+= 0.01f;
            upwardSpeed+=0.01f;

        }
        else if( !acceleration&& animator.hasRootMotion == false && forwardSpeed > 2 && upwardSpeed>1.6)
        {
            forwardSpeed-= 0.02f;
            upwardSpeed-=0.02f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Finish"))
        {
            Debug.Log("Collide");
            forwardSpeed = 0;
            upwardSpeed = 0;
        }
    }

}
