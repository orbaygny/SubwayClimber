using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    public Transform leftDoor;
    public Transform rightDoor;

    public bool closeDoor=false;
    public bool gameEnd = false;

    public Vector3 endPos;
    // Start is called before the first frame update
    Animator anim;
     public static Train Instance { get; private set; }

    void Awake(){
    Instance = this;
}
    void Start()
    {
        transform.parent = null;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameEnd){
            Camera.main.GetComponent<CameraFollow>().enabled = false;
            Player2.Instance.transform.parent = Train.Instance.transform;
            transform.position += transform.right*30*Time.fixedDeltaTime;
        }
      
        else if(closeDoor){
            anim.SetTrigger("CloseDoor");
            closeDoor =false;
            
        }

        else{
              transform.position = Vector3.MoveTowards(transform.position,new Vector3(-8,transform.position.y,transform.position.z),3f);
        }
        

    }

     
         
    
}
