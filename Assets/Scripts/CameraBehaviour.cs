using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour {

	public Transform target;
	public float resizeSpeed = 3f; 
	public bool gameOver;
	float previousZ;

	// Update is called once per frame
	void LateUpdate () {
		if(target.position.z > transform.position.z - 3f && !gameOver){
			transform.position = new Vector3(transform.position.x,transform.position.y,target.position.z + 3f);
		}
	}

	void FixedUpdate(){
		//setCameraSize();
	}

	void setCameraSize(){
		float desiredSize = findSize();
		GetComponent<Camera>().orthographicSize = Mathf.SmoothDamp(Camera.main.orthographicSize, desiredSize,
													 ref resizeSpeed, Time.fixedDeltaTime);
	}

	float findSize(){
		Vector3 camPosition = transform.InverseTransformPoint(transform.position);
		Vector3 targetPosition = transform.InverseTransformPoint(target.position);
		return targetPosition.y - camPosition.y;
	}
}
