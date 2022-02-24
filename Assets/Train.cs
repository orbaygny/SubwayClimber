using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    public Transform leftDoor;
    public Transform rightDoor;

    public Transform closePoint;

    public bool closeDoor=false;
    public bool gameEnd = false;

    public Vector3 endPos;

    Vector3 closeVector;
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
            closeDoor = false;
//            Camera.main.GetComponent<CameraFollow>().enabled = false;
            Player2.Instance.transform.parent = Train.Instance.transform;
            transform.position -= transform.right*30*Time.fixedDeltaTime;
        }
      
        else if(closeDoor){
          leftDoor.localPosition = Vector3.MoveTowards(leftDoor.localPosition,new Vector3(-189.9f,leftDoor.localPosition.y,leftDoor.localPosition.z),0.07f);
            rightDoor.localPosition = Vector3.MoveTowards( rightDoor.localPosition,new Vector3(-183.5f, rightDoor.localPosition.y, rightDoor.localPosition.z),0.07f);
           if(rightDoor.localPosition.x ==-183.5f )
           {
                CanvasScript.Instance.gameEnd = true;
        Player2.Instance.CloseMeshes();
            gameEnd = true;
           
               
           }
            
        }

        else{
              transform.position = Vector3.MoveTowards(transform.position,new Vector3(181,transform.position.y,transform.position.z),3f);
        }
        

    }

     
         
    
}
