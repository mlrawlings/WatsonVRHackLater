using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Threading;
using System.IO;
using System.Text;

public class GameLogic : MonoBehaviour {

    public Text status;
    public Text score;

    private int Score;

    int poseIndex = 0;
    bool captured = false;
    public AudioSource audioSource;

    public GameObject Menu;
    public GameObject GameUI;

    PoseStruct.Pose[] poses;

    string Folder = "tmp";

    //System.Action<bool> ClassifierFinished;

    public float Delay = 0.1f;

    // Use this for initialization
    void Start () {
        Score = 0;
        //Debug Only    
        //float t = GetComponent<AudioSource>().time = 38;
        Directory.CreateDirectory(Folder);
        Folder = Folder + "/Request";
        poses = PoseStruct.poses;
    }

    void OnEnable()
    {
        //ClassifierFinished += UpdateScore;
        //DEBUG
        //audioSource.time = 35;
        poseIndex = 0;
    }

    void OnDisable()
    {
        //ClassifierFinished -= UpdateScore;
    }

    public void StartGame()
    {
        audioSource.Play();

        Menu.SetActive(false);
        GameUI.SetActive(true);
        Score = 0;
        score.text = "Score: " + Score;
    }

    private void endGame()
    {
        Menu.SetActive(true);
        GameUI.SetActive(false);
    }

    // Update is called once per frame
    void Update () {

        if (poseIndex >= poses.Length)
        {
            if (!audioSource.isPlaying)
            {
                endGame();
            }
            return;
        }

        float t = audioSource.time;

        PoseStruct.Pose pose = poses[poseIndex];
        float timeStart = pose.timeStart;
        float timeEnd = pose.timeEnd;

        while (t > timeEnd)
        {
            captured = false;

            poseIndex++;
            if(poseIndex < poses.Length && audioSource.isPlaying)
            {
                pose = poses[poseIndex];
                timeStart = pose.timeStart;
                timeEnd = pose.timeEnd;
            }
            else
            {
                break;
            }

            if(poseIndex % 4 == 0)
            {
                status.text = "WAIT";
            }
        }

        if (timeStart < t && t < timeEnd)
        {
            if(!captured)
            {
                captured = true;
                status.text = " In Pose: " + pose.pose;
                score.text = "Score: " + Score;
                StartCoroutine(TakePhotoAndClassify(Folder + poseIndex, pose.pose));
            }
        }
    }

    IEnumerator TakePhotoAndClassify(string filename, string expected)
    {
        bool hasMatched = false;
        string[] classifierIDs = { "poses_421181554" };
        GetComponent<Screenshotter>().takeScreenShot(filename);
        GetComponent<GetClassifiers>().CheckMatch(filename + ".png", "pose" + expected.ToUpper(), classifierIDs, (wasMatch) => {
            if (wasMatch && !hasMatched)
            {
                hasMatched = true;
                increaseScore(10);
            }
        });
        yield return new WaitForSeconds(Delay);
        GetComponent<Screenshotter>().takeScreenShot(filename);
        GetComponent<GetClassifiers>().CheckMatch(filename + ".png", "pose" + expected.ToUpper(), classifierIDs, (wasMatch) => {
            if(wasMatch && !hasMatched)
            {
                hasMatched = true;
                increaseScore(10);
            }
        });
        yield return new WaitForSeconds(Delay);
        GetComponent<Screenshotter>().takeScreenShot(filename);
        GetComponent<GetClassifiers>().CheckMatch(filename + ".png", "pose" + expected.ToUpper(), classifierIDs, (wasMatch) => {
            if (wasMatch && !hasMatched)
            {
                hasMatched = true;
                increaseScore(10);
            }
        });
        yield return null;
    }

    public void increaseScore(int amount)
    {
        Score += amount;
        score.text = "Score: " + Score;
    }

    public int getScore()
    {
        return Score;
    }
}
