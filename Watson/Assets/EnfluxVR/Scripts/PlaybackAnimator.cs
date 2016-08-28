using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;

public class PlaybackAnimator : MonoBehaviour {

    JointRotations jointRotations = new JointRotations();

    private OrientationAngles orientAngles;
    private ArrayList rpyVals = new ArrayList();
    private float[] upper = new float[20];
    private float[] lower = new float[20];
    private float[] initCore = new float[] { 0, 0, 0 };
    private Quaternion hmd = Quaternion.identity;
    private bool initUpper = false;

    //lower    
    public Transform waist;
    public Transform leftThigh;
    public Transform leftShin;
    public Transform rightThigh;
    public Transform rightShin;
    //upper
    public Transform core;
    public Transform leftUpper;
    public Transform leftFore;
    public Transform rightUpper;
    public Transform rightFore;   

    private Quaternion chainUpper = new Quaternion();
    private Quaternion chainLower = new Quaternion();
    private Quaternion initCorePose = new Quaternion();

    // Use this for initialization
    void Start()
    {
        orientAngles = GameObject.Find("PlaybackAngles")
            .GetComponent<OrientationAngles>();
    }

    public void readInData(string filename)
    {
        var reader = new StreamReader(File.OpenRead("Assets/PoseRecordings/" + filename));

        while (!reader.EndOfStream)
        {
            string line = reader.ReadLine();
            rpyVals.Add(line);
        }

        string[] parts;

        foreach (string vals in rpyVals)
        {
            parts = vals.Split(',');
            float[] f = new float[] {
                Single.Parse(parts[0]), Single.Parse(parts[1]), Single.Parse(parts[2]), Single.Parse(parts[3]),
                Single.Parse(parts[4]),Single.Parse(parts[5]),Single.Parse(parts[6]), Single.Parse(parts[7]),
                Single.Parse(parts[8]),Single.Parse(parts[9]),Single.Parse(parts[10]),Single.Parse(parts[11]),
                Single.Parse(parts[12]),Single.Parse(parts[13]),Single.Parse(parts[14]),Single.Parse(parts[15]),
                Single.Parse(parts[16]),Single.Parse(parts[17]),Single.Parse(parts[18]),Single.Parse(parts[19]),
                Single.Parse(parts[20]),Single.Parse(parts[21]),Single.Parse(parts[22]),Single.Parse(parts[23]),
                Single.Parse(parts[24]),Single.Parse(parts[25]),Single.Parse(parts[26]),Single.Parse(parts[27]),
                Single.Parse(parts[28]),Single.Parse(parts[29]),Single.Parse(parts[30]),Single.Parse(parts[31]),
                Single.Parse(parts[32]),Single.Parse(parts[33]),Single.Parse(parts[34]),Single.Parse(parts[35]),
                Single.Parse(parts[36]),Single.Parse(parts[37]),Single.Parse(parts[38]),Single.Parse(parts[39])};           
            orientAngles.addAngles(f);
        }

        Debug.Log("Finished reading in data");
    }

    public void runPlayback()
    {
        readInData("TheMattLimaYogaClip.csv");
        StartCoroutine(setupAngles());
    }

    public void stopPlayback()
    {
        StopAllCoroutines();
    }

    IEnumerator setupAngles()
    {
        while (orientAngles.Count() > 0)
        {
            yield return null;
            float[] angles = orientAngles.getAngles();

            Buffer.BlockCopy(angles, 0, upper, 0, 20 * sizeof(float));
            Buffer.BlockCopy(angles, 20 * sizeof(float), lower, 0, 20 * sizeof(float));

            animateUpper(upper);
            animateLower(lower);
        }
    }

    private void animateUpper(float[] angles)
    {
        if(!initUpper)
        {
            initCore = new float[] { angles[1], angles[2], angles[3] };

            initCorePose = jointRotations.rotateCore(initCore,
                new float[] { 0, 0, 0 }, hmd);
        }
        
        //core node 1
        float[] coreAngles = new float[] { angles[1], angles[2], angles[3] };
        chainUpper = jointRotations.rotateCore(coreAngles, initCore, hmd);

        core.localRotation = chainUpper;

        float[] luAngles = new float[] { angles[5], angles[6], angles[7] };
        chainUpper = jointRotations.rotateLeftArm(luAngles, core.localRotation,
            hmd);

        leftUpper.localRotation = chainUpper;

        float[] lfAngles = new float[] { angles[9], angles[10], angles[11] };
        chainUpper = jointRotations.rotateLeftForearm(lfAngles, core.localRotation,
            leftUpper.localRotation, hmd);

        leftFore.localRotation = chainUpper;

        float[] ruAngles = new float[] { angles[13], angles[14], angles[15] };
        chainUpper = jointRotations.rotateRightArm(ruAngles, core.localRotation,
            hmd);

        rightUpper.localRotation = chainUpper;

        float[] rfAngles = new float[] { angles[17], angles[18], angles[19] };
        chainUpper = jointRotations.rotateRightForearm(rfAngles, core.localRotation,
            rightUpper.localRotation, hmd);

        rightFore.localRotation = chainUpper;
    }

    private void animateLower(float[] angles)
    {
        float[] waistAngles = new float[] { angles[1], angles[2], angles[3] };
        chainLower = jointRotations.rotateWaist(waistAngles, new float[] { 0, 0, 0 }, hmd);

        waist.localRotation = chainLower;

        float[] ltAngles = new float[] { angles[5], angles[6], angles[7] };
        chainLower = jointRotations.rotateLeftLeg(ltAngles, waist.localRotation,
            hmd);

        leftThigh.localRotation = chainLower;

        float[] lsAngles = new float[] { angles[9], angles[10], angles[11] };
        chainLower = jointRotations.rotateLeftShin(lsAngles, waist.localRotation,
            leftThigh.localRotation, hmd);

        leftShin.localRotation = chainLower;

        float[] rtAngles = new float[] { angles[13], angles[14], angles[15] };
        chainLower = jointRotations.rotateRightLeg(rtAngles, waist.localRotation,
            hmd);

        rightThigh.localRotation = chainLower;

        float[] rsAngles = new float[] { angles[17], angles[18], angles[19] };
        chainLower = jointRotations.rotateRightShin(rsAngles, waist.localRotation,
            rightThigh.localRotation, hmd);

        rightShin.localRotation = chainLower;
    }
}
