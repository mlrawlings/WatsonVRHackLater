using UnityEngine;
using System.Collections;


public class EnfluxHumaniodAttachment : MonoBehaviour {

    public GameObject eyeLocation;
    public GameObject HMD;

	// Use this for initialization
	void Start () {
    }
	    
	// Update is called once per frame
	void Update () {
	
	}

    void LateUpdate ()
    {
        Vector3 difference = HMD.transform.position - eyeLocation.transform.position;
        transform.Translate(difference);

    }

}
