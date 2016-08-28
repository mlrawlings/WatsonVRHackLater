using UnityEngine;
using System.Collections;

public class Menu_Button : MonoBehaviour {

    public GameLogic gameLogic;
    private bool gameStarted = false;
    private float shrinkAmount = 0.01f;

    float Cutoff = 0.1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameLogic.StartGame();
            Destroy(gameObject);
        }
	}

    void OnTriggerStay(Collider other)
    {
        Vector3 currentScale = gameObject.transform.localScale;
        if (currentScale.x > Cutoff && currentScale.y > Cutoff && currentScale.z > Cutoff)
            gameObject.transform.localScale = new Vector3(currentScale.x - shrinkAmount, currentScale.y - shrinkAmount, currentScale.z - shrinkAmount);
        else
        {
            if (!gameStarted)
            {
                gameLogic.StartGame();
                Destroy(gameObject);
            }
        }
    }
}
