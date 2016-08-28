using UnityEngine;
using System.Collections;

public class Screenshotter : MonoBehaviour {

    public Camera CaptureCamera;

    public void takeScreenShot(string filename)
    {
        int width = Screen.width;
        int height = Screen.height;

        //yield return new WaitForEndOfFrame();

        RenderTexture rt = new RenderTexture(width, height, 24);

        CaptureCamera.targetTexture = rt;

        Texture2D cap = new Texture2D(width, height, TextureFormat.RGB24, false);

        CaptureCamera.Render();

        RenderTexture.active = rt;

        cap.ReadPixels(new Rect(0, 0, width, height), 0, 0);

        CaptureCamera.targetTexture = null;

        RenderTexture.active = null;

        Destroy(cap);
        

        byte[] bytes = cap.EncodeToPNG();
        System.IO.File.WriteAllBytes(filename + ".png", bytes);
        Debug.Log("Done with image: " + filename);
        return;
    }
    public IEnumerator takeScreenShotAsync(string filename)
    {
        takeScreenShot(filename);
        yield return null;
    }

}
