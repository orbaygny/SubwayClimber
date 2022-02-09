using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player3 : MonoBehaviour
{
    public float forwardSpeed = 6f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Player2.Instance.startLeft || Player2.Instance.startRight)
        {
            transform.position += transform.forward*15*Time.deltaTime;
            
        }
        else {
            transform.position += transform.forward*forwardSpeed*Time.deltaTime;
        }
    }
}
