using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Material movingPlatform;
    
    Vector2 _a;
    // Start is called before the first frame update
    void Start()
    {
        _a = new Vector2(0,0);
    }

    // Update is called once per frame
    void Update()
    {
        _a.x -= 2.5f*Time.deltaTime;
        foreach(Transform child in transform){
            child.GetComponent<MeshRenderer>().materials[1].mainTextureOffset = _a;
        }
    }
}
