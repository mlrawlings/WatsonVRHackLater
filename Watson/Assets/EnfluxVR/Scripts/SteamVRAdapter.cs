//========= Copyright 2016, Enflux Inc. All rights reserved. ===========
//
// Purpose: Demo adapter for use with SteamVR and HTC Vive
//
//======================================================================

using UnityEngine;

public class SteamVRAdapter : MonoBehaviour {

    public GameObject hmd;    
    public GameObject waist;
    private Quaternion waistStart;

	// Use this for initialization
	void Start () {
        if(hmd != null)
        {
            gameObject.transform.SetParent(hmd.transform);
            GameObject.Find("[EnfluxVRHumanoid]").transform.SetParent(gameObject.transform);
            gameObject.transform.position = hmd.transform.position;
            gameObject.transform.rotation = Quaternion
                .AngleAxis(hmd.transform.rotation.eulerAngles.y, Vector3.up);
        }

        if( waist != null)
        {
            waistStart = Quaternion
                .AngleAxis(hmd.transform.rotation.eulerAngles.y, Vector3.up);
        }
    }

    void Update()
    {
        if (waist != null && hmd != null)
        {
            waist.transform.localRotation = Quaternion.Inverse(hmd.transform.localRotation) * 
                waistStart;
        }
    }

    public GameObject getHmd()
    {
        return hmd;
    }
}
