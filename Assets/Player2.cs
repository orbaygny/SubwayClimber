using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    
    // Start is called before the first frame update

   void FixedUpdate(){

       transform.position += transform.forward*3*Time.fixedDeltaTime;
   }
}
