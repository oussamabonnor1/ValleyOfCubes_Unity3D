using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarBehaviour : MonoBehaviour
{

    public GameManager gameManager;
    public GameObject cube;
    public int score = 0;
    public bool isLast = false;
    bool visited = false;

    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag.Equals("Cube"))
        {
            if (!visited)
            {
                visited = true;
                gameManager.scoreUp(score);
                if (isLast)
                {
                    StartCoroutine(gameManager.nextLevelPanel());
                }
            }
        }
    }


}
