using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunEffect : MonoBehaviour
{
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Npc._NpcStart)
        {rb.velocity = transform.forward * Player2.Instance.forwardSpeed;}
    }
}
