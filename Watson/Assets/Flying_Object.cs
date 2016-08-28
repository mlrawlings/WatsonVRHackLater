using UnityEngine;
using System.Collections;

public class Flying_Object : MonoBehaviour {

    private float shrinkAmount = 0.03f;
    private float Cutoff = 0.1f;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (gameObject.transform.position.z < -5.5f)
            Destroy(gameObject);
        else
            gameObject.transform.position = gameObject.transform.position - new Vector3(0, 0, Time.deltaTime);
	
	}


    void OnTriggerStay(Collider other)
    {
        Vector3 currentScale = gameObject.transform.localScale;
        if (currentScale.x > Cutoff && currentScale.y > Cutoff && currentScale.z > Cutoff)
            gameObject.transform.localScale = new Vector3(currentScale.x - shrinkAmount, currentScale.y - shrinkAmount, currentScale.z - shrinkAmount);
        else
        {
            GameObject.Find("GameLogic").GetComponent<GameLogic>().increaseScore(1);
            Destroy(gameObject);
        }
    }
}
