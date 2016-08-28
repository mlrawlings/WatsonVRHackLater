//========= Copyright 2016, Enflux Inc. All rights reserved. ===========
//
// Purpose: Handles Initialization timer for EnfluxVR suit
//
//======================================================================

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EVRInitializationCounter : MonoBehaviour {

    public int delay = 5;
    public bool record;
    private Text timerText;
    private EVRSuitManager _manager;
    private EVRHumanoidLimbMap _limbMap;

	// Use this for initialization
	void Start () {
        timerText = gameObject.GetComponent<Text>();
        _manager = GameObject.Find("EVRSuitManager").GetComponent<EVRSuitManager>();
        _limbMap = GameObject.Find("[EnfluxVRHumanoid]").GetComponent<EVRHumanoidLimbMap>();
	}

    public void startInitTimer()
    {
        StartCoroutine(initTimer());
    }

    public void stopAnimation()
    {
        _manager.disableAnimate();
        _limbMap.resetInitialize();
    }

    private IEnumerator initTimer()
    {        
        //delay must be at least 5 seconds
        if (delay < 5)
        {
            delay = 5;
        }

        int count = (delay + 1) * 2;        

        while (count > -1)
        {
            yield return new WaitForSeconds(0.5f);

            if(count % 2 == 0)
            {
                timerText.text = (count/2).ToString();
            }

            if (count  == 7)
            {
                _manager.enableAnimate(record);
            }
            
            count--;            
        }

        timerText.text = "Ready!";
        _limbMap.initialize();
    }
}
