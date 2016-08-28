using UnityEngine;
using System.Collections;

public class ScreenshotCam2 : MonoBehaviour {

    private Screenshotter takeAshot;
    public GameObject screenshotObject;
    public Camera thisCam;

	// Use this for initialization
	void Start () {
        takeAshot = screenshotObject.GetComponent<Screenshotter>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("s"))
        {
            StartCoroutine(takeAshot.takeScreenShotAsync("testcam2"));
        }
	}
}
