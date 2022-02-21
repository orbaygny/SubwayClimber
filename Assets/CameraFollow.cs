using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

	

	public float smoothSpeed = 0.125f;
	public Vector3 offset;

	public float angleX = 0f;

	public static CameraFollow Instance { get; private set; }	

	   void Awake(){ Instance = this;}
	void Start(){
		offset = new Vector3(0,5,-15);
	}
	
	void LateUpdate ()
	{

		if(Player2.Instance.camPan == false && Player2.Instance.r_camPan == false)
		{
			transform.rotation = Quaternion.Euler(angleX,transform.rotation.eulerAngles.y,transform.rotation.eulerAngles.z);
			if(offset.y<5){
				offset += new Vector3(0,0.2f,0);
			}

			if(offset.y>5){
				offset -= new Vector3(0,0.2f,0);
			}

			if(angleX < 0)
			{
				angleX+= 0.75f;
			}

			if(angleX > 0)
			{
				angleX-= 0.75f;
			}
		}

		if(Player2.Instance.camPan == true){
			
			transform.rotation = Quaternion.Euler(angleX,transform.rotation.eulerAngles.y,transform.rotation.eulerAngles.z);
			if(offset.y>1){
				offset -= new Vector3(0,0.2f,0);
				
			}
			if(angleX > -15)
			{
				angleX-= 0.75f;
			}
		}
		if(Player2.Instance.r_camPan == true){
			
			transform.rotation = Quaternion.Euler(angleX,transform.rotation.eulerAngles.y,transform.rotation.eulerAngles.z);
			if(Player2.Instance.isHold)
			{
				if(offset.y<17){
				offset += new Vector3(0,0.6f,0);
				
			}
			if(angleX <15)
			{
				angleX+= 0.75f;
			}
			}

			else if(!Player2.Instance.isHold && offset.y>11f)
			{	
				Debug.Log("Küçültme");
				
				offset -= new Vector3(0,0.3f,0);
				if(angleX <15){angleX+= 0.75f;}
			}

			else if(offset.y<10)
			{
				Debug.Log("Büyütme");
					offset += new Vector3(0,0.3f,0);
				if(angleX <15){angleX+= 0.75f;}
			}
		}
		
		Vector3 desiredPosition = target.position + offset;
		Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
		transform.position = smoothedPosition;

		//transform.LookAt(target);
	}
}
