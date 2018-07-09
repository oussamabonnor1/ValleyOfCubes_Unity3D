using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CubeBehaviour : MonoBehaviour {

	Rigidbody rb;
	public float jumpForce = 10f;
	public float time;
	//public GameObject line;
	public Transform endPoint,startPoint,highestPoint;
	public bool loseGame;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();	
	}
	
	// Update is called once per frame
	void Update () {
		if(!loseGame){
			if(Input.GetMouseButton(0)){
				Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,15));
				startPoint.position = highestPoint.position = transform.position;
				Vector3 startVelocity = (transform.position - mousePosition) * jumpForce;
				settingLineEndPoint(transform.position,startVelocity,time);
				//settingLineHighestPoint(startVelocity);
			}
			if(Input.GetMouseButtonUp(0) && Mathf.Approximately(rb.velocity.x,0) && Mathf.Approximately(rb.velocity.y,0)){
				startPoint.gameObject.SetActive(false);
 			endPoint.gameObject.SetActive(false);
				Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,15));
				rb.AddForce((transform.position - mousePosition) * jumpForce, ForceMode.Impulse);
			}
		}
	}

	 public void PlotTrajectory (Vector3 start, Vector3 startVelocity, float timestep, float maxTime) {
     Vector3 prev = start;
     for (int i=1;;i++) {
         float t = timestep*i;
         if (t > maxTime) break;
         Vector3 pos = PlotTrajectoryAtTime (start, startVelocity, t);
         if (Physics.Linecast (prev,pos)) break;
         Debug.DrawLine (prev,pos,Color.red);
         prev = pos;
     }
 	}

	public Vector3 PlotTrajectoryAtTime (Vector3 start, Vector3 startVelocity, float time) {
     return start + startVelocity*time + Physics.gravity*time*time*0.5f;
 	}

 	public void settingLineEndPoint(Vector3 start, Vector3 startVelocity, float time){
 			endPoint.position = PlotTrajectoryAtTime(start,startVelocity,time);
 			startPoint.gameObject.SetActive(true);
 			endPoint.gameObject.SetActive(true);
 	}

 	public void settingLineHighestPoint(Vector3 startVelocity){
 	 float g = -9.8f;
     float v0 = startVelocity.magnitude;
     float maxDistance = (endPoint.position.z - transform.position.z);
     highestPoint.position = new Vector3(transform.position.x, Mathf.Abs(endPoint.position.y *1.5f), transform.position.z + maxDistance/2);
 	}
 	
}
