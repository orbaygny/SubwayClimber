using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

	public float smoothSpeed = 0.125f;
	public Vector3 offset;
	void Start(){
		offset = new Vector3(0,5,-15);
	}
	void FixedUpdate ()
	{

		if(Player2.Instance.anim.GetBool("Stair") == false)
		{
			if(offset.y<5){
				offset += new Vector3(0,0.2f,0);
			}
		}

		if(Player2.Instance.anim.GetBool("Stair") == true){

			if(offset.y>1){
				offset -= new Vector3(0,0.2f,0);
			}
		}
		Vector3 desiredPosition = target.position + offset;
		Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
		transform.position = smoothedPosition;

		transform.LookAt(target);
	}
}
