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
            Camera.main.GetComponent<CameraFollow>().enabled = false;
            Player2.Instance.transform.parent = Train.Instance.transform;
            transform.position -= transform.right*30*Time.fixedDeltaTime;
        }
      
        else if(closeDoor){
          leftDoor.position = Vector3.MoveTowards(leftDoor.transform.position,new Vector3((closePoint.position.x/2)+closePoint.position.x,leftDoor.position.y,leftDoor.position.z),0.4f);
            rightDoor.position = Vector3.MoveTowards( rightDoor.transform.position,new Vector3(closePoint.position.x/2, rightDoor.position.y, rightDoor.position.z),0.4f);
           if(leftDoor.position.x == (closePoint.position.x/2)+closePoint.position.x)
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
