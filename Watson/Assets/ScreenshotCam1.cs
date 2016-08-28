using UnityEngine;
using System.Collections;

public class ScreenshotCam1 : MonoBehaviour {

    private Screenshotter takeAshot;
    public GameObject screenshotObject;
    public Camera thisCam;
    public int Count = 0;
    public string Folder = "Images";
    // Use this for initialization
    void Start () {
        takeAshot = screenshotObject.GetComponent<Screenshotter>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("t"))
        {
            StartCoroutine(takeAshot.takeScreenShotAsync(Folder + "/testcam" + Count++));
        }
    }
}
