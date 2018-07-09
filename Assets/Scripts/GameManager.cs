using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public GameObject pillarPrefab;
	public GameObject pillarHolder; 
	public GameObject cube;
	public GameObject loseUI;
	public Text scoreText, finalScoreText, bestScoreText, jumpsText;
	public int pillarsToMake = 20;
	public float minDistance = 3f, maxDistance = 5f;
	public int score, jumps;
	Vector3 spawnPosition;
	bool gameOver;

	// Use this for initialization
	void Start () {
		spawnPosition = transform.position;
		makingPillars();
		score = 0;
		gameOver = false;
	}

	void FixedUpdate(){
		if(cube.transform.position.y < -13 && !gameOver){
			gameOver = true;
			Camera.main.gameObject.GetComponent<CameraBehaviour>().gameOver = true;
			StartCoroutine(loseGame());
		}
	}

	public void scoreUp(int points){
		score+= points;
		jumps++;
		scoreText.text = ""+points;
	}

	void makingPillars(){
		for(int i = 0; i < pillarsToMake; i++){
			GameObject tempPillar = Instantiate(pillarPrefab, spawnPosition, pillarPrefab.transform.rotation);
			tempPillar.transform.SetParent(pillarHolder.transform);
			tempPillar.GetComponent<PillarBehaviour>().gameManager = this;
			tempPillar.GetComponent<PillarBehaviour>().cube = cube;
			tempPillar.GetComponent<PillarBehaviour>().score = i+1;
			spawnPosition = new Vector3(spawnPosition.x, Random.Range(-8,-4), 
							spawnPosition.z + Random.Range(minDistance, maxDistance));
		}	
	}

	IEnumerator loseGame(){
		cube.GetComponent<CubeBehaviour>().loseGame = true;
		for(int i = 0; i >= 0; i--){
			float k = (float) i/10;
			//pillarHolder.transform.localScale = new Vector3(k,k,k);
			yield return new WaitForSeconds(.05f);
		}
		//pillarHolder.SetActive(false);
		if(score > PlayerPrefs.GetInt("bestScore",0)) PlayerPrefs.SetInt("bestScore",score);
		loseUI.SetActive(true);
		finalScoreText.GetComponent<Text>().text = ""+ score;
		bestScoreText.GetComponent<Text>().text = "Best score:"+ PlayerPrefs.GetInt("bestScore",0);
		jumpsText.GetComponent<Text>().text = "Jumps:"+ jumps;
	}

	public void restart(){
		SceneManager.LoadScene("Main");
	}
}
