//========= Copyright 2016, Enflux Inc. All rights reserved. ===========
//
// Purpose: Scanning for Bluegiga BLED112 dongle
//
//======================================================================

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScanResultsUpdater : MonoBehaviour {

    private state status;
    private IScanUpdate updateView;
    private Dictionary<string, BleDevice> scannedDevices = new Dictionary<string, BleDevice>();
        
    private enum state
    {
        state_updating,
        state_notupdating
    };

    // Use this for initialization
    void Start () {
        status = state.state_notupdating;
        scannedDevices = ThreadDispatch.instance.GetScanItems();
    }
	
	// Update is called once per frame
	void Update () {
       
    }

    public void StartScanning()
    {
        StartCoroutine(processDevices());
    }

    public void StopScanning()
    {
        StopAllCoroutines();
    }

    private IEnumerator processDevices()
    {
        while (true)
        {
            yield return null;
            if (scannedDevices != null)
            {
                foreach (var pair in scannedDevices)
                {
                    updateView.postUpdate(pair.Value);
                }
            }
            scannedDevices = ThreadDispatch.instance.GetScanItems();
        }
    }

    public void setUpdateView(IScanUpdate view)
    {
        updateView = view;
    }

    public interface IScanUpdate
    {
        void postUpdate(BleDevice device);
    }
}
