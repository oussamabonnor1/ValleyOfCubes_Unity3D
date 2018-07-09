using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarBehaviour : MonoBehaviour {

	public GameManager gameManager;
	public GameObject cube; 
	public int score=0;
	Rigidbody cubeRB;
	bool visited;

	void Start(){
		cubeRB = cube.GetComponent<Rigidbody>();
		visited = false;
	}

	void OnCollisionStay(Collision other){
		if(other.gameObject.tag.Equals("Cube")){
			if(!visited){
				if(Mathf.Approximately(cubeRB.velocity.y,0) && Mathf.Approximately(cubeRB.velocity.z,0)){
					visited = true;
					gameManager.scoreUp(score);
				}
			}
		}
	}


}
